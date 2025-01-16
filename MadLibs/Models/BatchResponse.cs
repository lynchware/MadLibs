namespace MadLibs.Models
{
    public class BatchResponse
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public List<UserResponse> Responses { get; set; } = new();
    }

}
