using MadLibs.Models;

namespace MadLibs.Data
{
    public interface IStoriesRepository
    {
        Task<List<StoriesViewModel>> GetStoriesViewModelsAsync();
    }
}