using AutoMapper;

namespace NZwalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            //Aici am adaugat namescape-ul ca sa functioneze maparea
            CreateMap<NZWalks.API.Models.Domain.DomainRegion, Models.DTO.Region>()
            .ReverseMap();
        }
    }
}
