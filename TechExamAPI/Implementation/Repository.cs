using Dapper;
using System.Data.SqlClient;
using System.Data;
using TechExamAPI.Interface;

namespace TechExamAPI.Implementation
{
    public class RepositoryService : IRepositoryService
    {
        private readonly string _connectionString;

        public RepositoryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultDataContext");
        }

        public async Task<T> GetData<T>(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var res = await db.QueryFirstOrDefaultAsync<T>(sp, parameters, commandType: commandType);
                    return res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        //public async Task<TModel> Get<TModel>(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure) where TModel : class
        //{
        //    try
        //    {
        //        using (var db = new SqlConnection(_connectionString))
        //        {
        //            var res = await db.QueryFirstOrDefaultAsync<TModel>("sp", parameters, commandType: commandType);
        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new Exception("Failed to fetch the data");
        //    }
        //}

        public async Task<List<TModel>> GetAllData<TModel>(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure) where TModel : class
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var res = await db.QueryAsync<TModel>(sp, parameters, commandType: commandType);
                    return res.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        public async Task Execute(string sp, DynamicParameters parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var res = await db.ExecuteAsync(sp, parameters, commandType: commandType);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }
    }
}
