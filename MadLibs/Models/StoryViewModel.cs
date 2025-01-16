namespace MadLibs.Models
{
    public class StoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public string ImagePath { get; set; }
        public bool IsExpanded { get; set; } = false;
        public List<Placeholder> Placeholders { get; set; } = new();
        public List<UserResponse> Responses { get; set; } = new();
    }
}
