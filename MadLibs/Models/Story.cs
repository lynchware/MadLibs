namespace MadLibs.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
        public int ThemeId { get; set; }
    }
}
