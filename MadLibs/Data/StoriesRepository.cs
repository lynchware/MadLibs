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

            var results = queryResult.GroupBy(q => q.ThemeId)
                .Select(g =>
                {
                    var theme = g.First();
                    theme.Id = g.First().Id;
                    theme.Title = g.First().Title;
                    theme.Template = g.First().Template;
                    theme.ImagePath = g.First().ImagePath;
                    theme.IsExpanded = g.First().IsExpanded;
                    theme.Responses = g.SelectMany(q => q.Responses).ToList();
                    theme.Placeholders = g.SelectMany(q => q.Placeholders).ToList();

                    return theme;
                });

            return results.ToList();
        }

    }
}

