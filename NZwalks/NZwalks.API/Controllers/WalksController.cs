using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
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
    }
}
