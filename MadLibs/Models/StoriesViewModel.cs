namespace MadLibs.Models
{
    public class StoriesViewModel
    {
        public Story Story { get; set; }
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public List<Placeholder> Placeholders { get; set; }
        public List<UserResponse> Responses { get; set; }
    }
}
