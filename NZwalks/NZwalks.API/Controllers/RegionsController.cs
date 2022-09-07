using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Repositories;
using System.Runtime.InteropServices;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }



        //we create a new method which is decorated with HTTPGet attribute
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //return DTO regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    //I don't understand wher region DTO comes from or this regionDTO is 
            //    //just set as a variable in order to be assigned later to regionsDTO.Add
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //           Id = region.Id,
            //           Code = region.Code,
            //           Name = region.Name,
            //           Area = region.Area,
            //           Lat = region.Lat,
            //           Long = region.Long,
            //           Population = region.Population,
            //    }; 

            //    regionsDTO.Add(regionDTO);
            //});
            //return Ok???

            //nu inteleg ce a facut aici. a facut un MAP de lista de Models.DTO.Region de rgions? si l-a aruncat in regions DTO?
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]

        //ASP.Net allows us to use collon and mention an id that 
        //takes for example only guid values only;
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            //De ce cu litera mica si nu cu litera mare "id"?
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        //[ActionName(nameof(GetAllRegionsAsync))]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Convert Request to Domain Model
            var region = new NZWalks.API.Models.Domain.DomainRegion()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };

            //Pass details to Repository
            region = await regionRepository.AddAsync(region);

            //Convert back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRegionAcync(Guid id)
        {
            //get region from Database
            var region = await regionRepository.DeleteAsync(id);

            //if null then return response
            if (region == null)
            {
                return NotFound();
            }

            //Convert response to DTO if received region
            var regionDTO = new Models.DTO.Region
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //Return Ok response
            return Ok();
        }
        //Create the action and Decoration of the Update/Put method

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to Domain Model
            var region = new NZWalks.API.Models.Domain.DomainRegion()
            { 
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            //Update region using repository
            await regionRepository.UpdateAsync(id, region);

            //If Null then not found
            if (region == null)
            {
                return NotFound();
            }

            //Convert Domain back to DTO
            Models.DTO.Region regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //Return Ok response

            return Ok(regionDTO);
        }
    }
}


