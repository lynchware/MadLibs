using MadLibs.Models;

namespace MadLibs.Data
{
    public interface IStoriesRepository
    {
        Task MethodTesting();
        Task<List<StoryViewModel>> GetStoriesViewModelsAsync();
        Task<List<StoryViewModel>> GetStoriesViewModelsAlt();
    }
}