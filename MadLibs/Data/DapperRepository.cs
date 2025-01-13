using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using MadLibs.Models;

namespace MadLibs.Data
{
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedureName, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<TEntity>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<TEntity?> GetAsync(string storedProcedureName, object parameters)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<TEntity>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> AddAsync(string storedProcedureName, object parameters)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> UpdateAsync(string storedProcedureName, object parameters)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> DeleteAsync(string storedProcedureName, object parameters)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> QueryMultipleAsync<T1, T2, T3, T4>(
            string storedProcedureName,
            object? parameters = null)
        {
            using var connection = CreateConnection();
            using var result = await connection.QueryMultipleAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var set1 = await result.ReadAsync<T1>();
            var set2 = await result.ReadAsync<T2>();
            var set3 = await result.ReadAsync<T3>();
            var set4 = await result.ReadAsync<T4>();

            return (set1, set2, set3, set4);
        }

        public async Task<List<StoriesViewModel>> GetStoriesViewModelsAsync()
        {
            var sql = "GetStoriesViewModels"; 

            using var connection = CreateConnection();

            // Fetch the data and map it directly
            var result = await connection.QueryAsync<StoriesViewModel, Placeholder, UserResponse, StoriesViewModel>(
                sql, (viewModel, placeholder, response) =>
                {
                    // Add placeholders and responses directly to the view model
                    if(placeholder != null)
                        viewModel.Placeholders.Add(placeholder);

                    if(response != null)
                        viewModel.Responses.Add(response);

                    return viewModel;
                },
                splitOn: "PlaceholderId,ResponseId", // Split on these fields in the result set
                commandType: CommandType.StoredProcedure
            );

            return result.Distinct().ToList(); 
        }

    }
}
