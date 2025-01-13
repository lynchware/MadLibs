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

            // Fetch the data and map it directly
            var result = await connection.QueryAsync<StoriesViewModel, Placeholder, UserResponse, StoriesViewModel>(
                sql,
                (viewModel, placeholder, response) =>
                {
                    if(placeholder != null)
                        viewModel.Placeholders.Add(placeholder);

                    if(response != null)
                        viewModel.Responses.Add(response);

                    return viewModel;
                },
                splitOn: "PlaceholderId,ResponseId",
                commandType: CommandType.StoredProcedure
            );

            return result.Distinct().ToList();
        }
    }
}

