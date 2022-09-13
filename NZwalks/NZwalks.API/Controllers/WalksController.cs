using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {

            //Fetch Data from database
            var walksDomain = await walkRepository.GetAllAsync();

            //Convert Domain Walks to DTO WalksDTO
            var walksDTO = mapper.Map<List<Models.DTO.WalkDTO>>(walksDomain);

            //Return
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get walk object from DataBase
            var walkDomain = await walkRepository.GetAsync(id);
            //Convert Domain to DTO

            var walksDTO = mapper.Map<Models.DTO.WalkDTO>(walkDomain);

            //return
            return Ok(walksDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {

            //Validate request // implemented with FluentValidator

            if (!await ValidateAddWalkAsync(addWalkRequest) == true)
            {
                return BadRequest(ModelState);
            }

            //Convert request to Domain Object
            var walkPostDomain = new NZWalks.API.Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

             //Pass Domain Object details to Repository
             walkPostDomain = await walkRepository.AddAsync(walkPostDomain);

            //Convert Domain Object back to DTO

            var walkPostDTO = new Models.DTO.WalkDTO
            {
                Id = walkPostDomain.Id,
                Name = walkPostDomain.Name,
                Length = walkPostDomain.Length,
                RegionId = walkPostDomain.RegionId,
                WalkDifficultyId = walkPostDomain.WalkDifficultyId
            };

            //Send DTO Response back to client

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkPostDTO.Id }, walkPostDTO);
         }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute]Guid id, 
            [FromBody]Models.DTO.UpdateWalkRequest updateWalkRequest)
        
        {
            //Validate request 
            if(! await ValidateUpdateWalkAsync(updateWalkRequest) == true)
            {
                return BadRequest(ModelState);
            }

            //Convert DTO to Domain Object
            var walkDomain = new NZWalks.API.Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId

            };

            //Update Walk using repository Get Domain object in response (or null)

            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            //if Null then not found
            if (walkDomain == null)
            {
                return NotFound();
            }
            else
            {
                var walkDTO = new Models.DTO.WalkDTO()
                {
                    Id = walkDomain.Id,
                    Name = walkDomain.Name,
                    Length = walkDomain.Length,
                    WalkDifficultyId = walkDomain.WalkDifficultyId,
                    RegionId = walkDomain.RegionId

                };

                //Return ok response
                return Ok(walkDTO);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {

            //Get walk from database and assign a delete await once received the id
            var walk = await walkRepository.DeleteAsync(id);

            //if search result is null then return not Found
            if (walk == null)
            {
                return NotFound();
            }

            //convert response to DTO if we received the walk
            var walkDTO = new Models.DTO.WalkDTO
            {
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };


            //create ok response
            return Ok();
        }

        #region Private Methods

        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} cannot be empty.");

            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name), $"{nameof(addWalkRequest.Name)}, cannot be empty.");
            //}

            //if (addWalkRequest.Length <= 0 )
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length), $"{nameof(addWalkRequest.Length)}, should be greater than zero");
            //}

            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"{nameof(addWalkRequest.RegionId)}, is invalid Id.");
            }

            var walkDifficultyId = await walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficultyId == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"{nameof(addWalkRequest.WalkDifficultyId)}, is invalid ID.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }

        private async Task<bool> ValidateUpdateWalkAsync(NZwalks.API.Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //if(updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} cannot be empty");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} value cannot be empty or null");
            //}

            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)} value cannot be zero");
            //}

            var updateRegion = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (updateRegion == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{nameof(updateWalkRequest.RegionId)} Invalid Id");
            }

            var updateWalkDifficulty = await walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (updateWalkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)} Invalid Id");
            }
            return true;
        }

        #endregion
    }
}
