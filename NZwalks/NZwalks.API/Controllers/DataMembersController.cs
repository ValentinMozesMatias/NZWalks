using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Domain;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataMembersController : Controller
    {
        private readonly IMembersDataRepository membersDataRepository;
        private readonly Mapper mapper;

        public DataMembersController(IMembersDataRepository membersDataRepository, IMapper mapper)
        {
            this.membersDataRepository = membersDataRepository;
            this.mapper = (Mapper)mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembersData()
        {
            //var memberData = membersDataRepository.GetAllAsync();

            var memberData = new List<DataMembers>()
            {
                new DataMembers
                {
                    NameOfDepositor = "Valentin Sebastian",
                    AccNumber = 234,
                    AccBalance = 5000,
                },

                new DataMembers
                {
                    NameOfDepositor = "Norbert Sebastian",
                    AccNumber = 543,
                    AccBalance = 15000,
                },

            };

            return Ok(memberData);
               
        }

        //[HttpGet]
        //[Route("{AccNumber: int}")]
        //public async Task<IActionResult>GetDataMemberAsync(int accNumber)
        //{
        //    var memberData = await membersDataRepository.GetAsync(accNumber);
        //    var memberDataDTO = mapper.Map<Models.DTO.AssignValuesRequest>(memberData);

        //    if (memberData == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(memberDataDTO);
        //}
    }
}
