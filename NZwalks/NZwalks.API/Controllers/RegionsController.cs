using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Repositories;
using NZWalks.API.Models.Domain;

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
        public async Task<IActionResult> GetAllRegions()
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
    }
}
