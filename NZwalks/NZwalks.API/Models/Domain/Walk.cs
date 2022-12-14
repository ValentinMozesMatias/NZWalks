namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation properties

        public DomainRegion Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

        //Now install Entity Framework Core Nugget Packages
    }
}
