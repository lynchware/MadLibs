namespace MadLibs.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public int PlaceholderId { get; set; }
        public string Word { get; set; }
    }
}
