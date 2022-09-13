using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Repositories;
using NZWalks.API.Models.Domain;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class WalkDifficultiesController : Controller
    {

        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;


        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;


        }



        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            return Ok(await walkDifficultyRepository.GetAllAsync());

            var walkDifficultiesDomain = walkDifficultyRepository.GetAllAsync();

            var walkDifficultiesDTO = mapper.Map<Models.DTO.WalkDifficultyDTO>(walkDifficultiesDomain);

            return Ok(walkDifficultiesDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkdifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficultyDTO>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {

            //Validating incoming requests
            if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

        ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest);

            var newWalkDifficulty = new NZWalks.API.Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            newWalkDifficulty = await walkDifficultyRepository.AddAsync(newWalkDifficulty);

            var newWalkdifficultyDTO = new Models.DTO.WalkDifficultyDTO
            {

                Id = newWalkDifficulty.Id,
                Code = newWalkDifficulty.Code
            };

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = newWalkdifficultyDTO.Id }, newWalkdifficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, 
            [FromBody]Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {

            //Validate UpdateWalkDifficulties
            if (!ValidateUpdateWalkDifficultiesAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            //Convert DTO to Domain object
            var walkDifficultyDomain = new NZWalks.API.Models.Domain.WalkDifficulty 
                {
                Code = updateWalkDifficultyRequest.Code
                };

            //Call repository to update
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            // Convert Domain back to DTO

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficultyDTO>(walkDifficultyDomain);

            //Return the information to DTB

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var deleteWalkDifficulty = await walkDifficultyRepository.DeleteAsync(id);

            if (deleteWalkDifficulty == null)
            {
                return NotFound();
            }

            var newWalkDifficultyListDTO = new NZwalks.API.Models.DTO.WalkDifficultyDTO()
            {
                Code = deleteWalkDifficulty.Code,
            };

            return Ok();
            
        }

        // nu inteleg de ce aici s-a pus in # in fata cuvantului
        #region

        private bool ValidateAddWalkDifficultyAsync(NZwalks.API.Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest), $"{nameof(addWalkDifficultyRequest)}, is required.");
                return false;
            }
            

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code), $"{nameof(addWalkDifficultyRequest.Code)} it can't be null.");
   
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultiesAsync(NZwalks.API.Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest), $"{nameof(updateWalkDifficultyRequest)} is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code), $"{nameof(updateWalkDifficultyRequest.Code)} cannot be null or empty. ");
            return false;
            }
            return true;
        }
        #endregion
    }
}
