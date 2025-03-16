using Dapper;
using System.Data;

namespace TechExamAPI.Interface
{
    public interface IRepositoryService
    {
        Task<T> GetData<T>(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure);
        //Task<TModel> Get<TModel>(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure) where TModel : class;

        Task<List<TModel>> GetAllData<TModel>(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure) where TModel : class;

        Task Execute(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure);
    }
}
