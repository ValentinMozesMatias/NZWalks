namespace NZWalks.API.Models.Domain
{
    public class DomainRegion
    {


        //Navigation Property
        public IEnumerable<Walk> Walks { get; set; }
        public Guid Id { get; internal set; }
        public string Code { get; internal set; }
        public string Name { get; internal set; }
        public double Area { get; internal set; }
        public double Lat { get; internal set; }
        public double Long { get; internal set; }
        public long Population { get; internal set; }
    }
}
