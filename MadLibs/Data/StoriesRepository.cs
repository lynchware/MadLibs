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

            var queryResult = await connection.QueryAsync<StoriesViewModel, Placeholder, UserResponse, StoriesViewModel>(
                sql,
                (viewModel, placeholder, response) =>
                {
                    if(placeholder != null)
                        viewModel.Placeholders.Add(placeholder);
                    if(response != null)
                        viewModel.Responses.Add(response);
                    return viewModel;
                },
                commandType: CommandType.StoredProcedure
            );

            var results = queryResult.GroupBy(q => q.Id)
                .Select(g =>
                {
                    var story = g.First();
                    story.Id = g.First().Id;
                    story.Title = g.First().Title;
                    story.Template = g.First().Template;
                    story.ImagePath = g.First().ImagePath;
                    story.IsExpanded = g.First().IsExpanded;
                    story.Responses = g.SelectMany(q => q.Responses).ToList();
                    story.Placeholders = g.SelectMany(q => q.Placeholders).ToList();

                    return story;
                });

            return results.ToList();
        }

    }
}

