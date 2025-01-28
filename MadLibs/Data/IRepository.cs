using System.Data;

namespace MadLibs.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string storedProcedureName, object? parameters = null);
        Task<TEntity?> GetAsync(string storedProcedureName, object parameters);
        Task<int> AddAsync(string storedProcedureName, object parameters);
        Task<int> GetValueAsync(string sql, object parameters);
        Task<int> UpdateAsync(string storedProcedureName, object parameters);
        Task<int> DeleteAsync(string storedProcedureName, object parameters);
        Task SaveBatch(string storedProcedureName, DataTable data);
        Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> QueryMultipleAsync<T1, T2, T3, T4>(
            string storedProcedureName,
            object? parameters = null);
    }
}
