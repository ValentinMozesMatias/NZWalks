namespace NZwalks.API.Models
{
    public class RepositoryResponse
    {
        public RepositoryResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Data { get; set; }
    }
}
