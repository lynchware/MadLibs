﻿using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using MadLibs.Models;
using Microsoft.Extensions.Configuration;

namespace MadLibs.Data
{
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MadLibsConnectionString")
                ?? throw new ArgumentNullException(nameof(_configuration), "Connection string not found.");
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

        public async Task<int> GetValueAsync(string sql, object parameters)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<int>(
                sql,
                parameters
            );
        }
        //example using Dynamic Parameters
        public async Task<int> AddAsync(string storedProcedureName, object parameters)
        {
            using var connection = CreateConnection();
            var dynamicParameters = new DynamicParameters(parameters);
            dynamicParameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                await connection.ExecuteAsync(storedProcedureName, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return dynamicParameters.Get<int>("NewId");
        }

        public async Task<string> AddAsyncWithValidation(string storedProcedureName, object parameters)
        {
            using var connection = CreateConnection();
            var dynamicParameters = new DynamicParameters(parameters);
            dynamicParameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParameters.Add("Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            try
            {
                await connection.ExecuteAsync(storedProcedureName, dynamicParameters, commandType: CommandType.StoredProcedure);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            if(dynamicParameters.Get<int>("NewId") == 0)
            {
                return dynamicParameters.Get<string>("Message");
            }
            else
                 return dynamicParameters.Get<string>("NewId");
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
            try
            {
                return await connection.ExecuteAsync(
                    storedProcedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task SaveBatch(string storedProcedureName, DataTable data)
        {
            using(IDbConnection connection = CreateConnection())
            {
                connection.Open();
                var p = new
                {
                    placeholders = data.AsTableValuedParameter("BasicUDT")
                };
                using(var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(storedProcedureName, p, transaction, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                    }                
                }
            }
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

    }
}
