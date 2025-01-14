using Dapper;
using MadLibs.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MadLibs.Data
{
    public class StoriesRepository : IStoriesRepository
    {
        private readonly string _connectionString;

        public StoriesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<List<StoriesViewModel>> GetStoriesViewModelsAsync()
        {
            var sql = "GetStoriesViewModels"; // Stored procedure name

            using var connection = CreateConnection();

            // Dictionary to ensure only one StoriesViewModel per Story Id
            var storyDictionary = new Dictionary<int, StoriesViewModel>();

            var queryResult = await connection.QueryAsync<StoriesViewModel, Placeholder, UserResponse, StoriesViewModel>(
                sql,
                (viewModel, placeholder, response) =>
                {
                    // Find or create the StoriesViewModel
                    if(!storyDictionary.TryGetValue(viewModel.Id, out var existingViewModel))
                    {
                        existingViewModel = viewModel;
                        storyDictionary.Add(existingViewModel.Id, existingViewModel);
                    }

                    // Add placeholders
                    if(placeholder != null && !existingViewModel.Placeholders.Any(p => p.Id == placeholder.Id))
                    {
                        existingViewModel.Placeholders.Add(placeholder);
                    }

                    // Add responses
                    if(response != null && !existingViewModel.Responses.Any(r => r.Id == response.Id))
                    {
                        existingViewModel.Responses.Add(response);
                    }

                    return existingViewModel;
                },
                commandType: CommandType.StoredProcedure
            );

            // Return distinct StoriesViewModel objects
            return storyDictionary.Values.ToList();
        }

    }
}

