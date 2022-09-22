using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Collections.Generic;
using System.Data;

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
        //Adaugam informatiile in DTO de unde aceasta informatie va fi dusa in NZWalksDbContext(database) unde va fi salvata si apoi va fi returnata in Repository
        //In final returnam informatiile in GetMembersData daca informatiile sunt correcte && not null.
        [HttpPost]
        public async Task<IActionResult> AddMembersDataAsync(Models.DTO.AssignValuesRequest assignValuesRequest)
        {
            var assign = new NZwalks.API.Models.Domain.DataMembers()
            {
                NameOfDepositor = assignValuesRequest.NameOfDepositor,
                Id = new Guid(),
                AccBalance = assignValuesRequest.AccBalance,
            };

            //Dam informatiile la Repository //
            assign = await membersDataRepository.AddAsync(assign);


            //Dam informatiile inapoi la DTO

            var assignDTO = new Models.DTO.AssignValuesRequest
            {
                NameOfDepositor = assign.NameOfDepositor,
                Id = assign.Id,
                AccBalance = assign.AccBalance

            };

            return CreatedAtAction(nameof(GetMembersDataAsync), new { assignDTO.Id }, assignDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateMembersDataAsync([FromRoute] Guid id, [FromBody]Models.DTO.UpdateDataMemberRequest updateDataMemberRequest)
        {
            //Convert Domain Model to DTO
            var memberData = new Models.Domain.DataMembers
            {
                NameOfDepositor = updateDataMemberRequest.NameOfDepositor,
                AccBalance = updateDataMemberRequest.AccBalance,
            };

           memberData = await membersDataRepository.UpdateAsync(id, memberData);

            //if null return not Found
            if (memberData == null)
            {
                return NotFound();
            }

            //convert back to Domain Model
            var memberDataDTO = new Models.DTO.AssignValuesRequest
            {
                NameOfDepositor = memberData.NameOfDepositor,
                AccBalance = memberData.AccBalance,
            };
            

            //Return ok Response
            return Ok(memberDataDTO);
        }

        [HttpGet]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> GetMembersDataAsync()
        {
            var memberData = await membersDataRepository.GetAllAsync();
            var memberDataDTO = mapper.Map<List<DataMembers>, List<Models.DTO.AssignValuesRequest>>(memberData);
            return Ok(memberDataDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> GetMembersDataAsync(Guid id, MembersDataRepository membersDataRepository)
        {
            var memberData = await membersDataRepository.GetAllAsync(id);
            var memberDataDTO = mapper.Map<Models.DTO.AssignValuesRequest>(memberData);

            if (memberData == null)
            {
                return NotFound(memberData);
            }

            return Ok(memberDataDTO);
        }

    }
}
