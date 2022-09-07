using AutoMapper;

namespace NZwalks.API.Profiles
{
    public class WalksProfile: Profile
    {
        public WalksProfile()
        {
            CreateMap<NZWalks.API.Models.Domain.Walk, Models.DTO.WalkDTO>()
                .ReverseMap();

            CreateMap<NZWalks.API.Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyDTO>()
                .ReverseMap();
        }
    }
}
