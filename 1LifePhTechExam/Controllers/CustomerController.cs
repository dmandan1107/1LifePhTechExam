using CommonLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace _1LifePhTechExam.Controllers
{
    public class CustomerController : Controller
    {
        private readonly string _connectionString;

        public CustomerController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultDataContext");
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<CustomerGVM> GetCustomer(int customerId)
        {
            try
            {
                using var db = new SqlConnection(_connectionString);
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", customerId);
                var res = await db.QueryFirstOrDefaultAsync<CustomerGVM>("GetCustomer", parameters, commandType: CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }


        public async Task<List<CustomerGVM>> GetAllCustomer(string search = "")
        {
            using var db = new SqlConnection(_connectionString);
            var res = (await db.QueryAsync<CustomerGVM>("GetAllCustomer", null, commandType: CommandType.StoredProcedure)).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(a => a.FullName.ToUpper().Contains(search.ToUpper())).ToList();
                return res;
            }

            return res;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody]CustomerGVM data)
        {
            try
            {
                using var db = new SqlConnection(_connectionString);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var errors = new Dictionary<string, string>();

                async Task ValidateField(string input, string valType, string fieldName)
                {
                    var param = new DynamicParameters();
                    param.Add("@Input", input);
                    param.Add("@ValType", valType);
                    var exists = await db.QueryFirstOrDefaultAsync<bool>("ValidateInput", param, commandType: CommandType.StoredProcedure);
                    if (exists)
                    {
                        errors[fieldName] = $"{valType} already exists.";
                    }
                }

                await ValidateField(data.FirstName, "First Name", "firstName");
                await ValidateField(data.LastName, "Last Name", "lastName");
                await ValidateField(data.MobileNumber, "Mobile Number", "mobileNumber");

                if (errors.Any())
                {
                    return BadRequest(new { errors });
                }

                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", data.FirstName);
                parameters.Add("@LastName", data.LastName);
                parameters.Add("@FullName", data.FullName);
                parameters.Add("@MobileNumber", data.MobileNumber);
                parameters.Add("@City", data.City);
                parameters.Add("@IsActive", data.IsActive);
                var res = await db.QueryFirstOrDefaultAsync<CustomerGVM>("CreateCustomer", parameters, commandType: CommandType.StoredProcedure);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }          

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerGVM data)
        {
            try
            {
                using var db = new SqlConnection(_connectionString);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var errors = new Dictionary<string, string>();

                async Task ValidateField(string input, string valType, string fieldName)
                {
                    var param = new DynamicParameters();
                    param.Add("@Input", input);
                    param.Add("@ValType", valType);
                    param.Add("@CustomerID", data.customerId);
                    var exists = await db.QueryFirstOrDefaultAsync<bool>("ValidateInput", param, commandType: CommandType.StoredProcedure);
                    if (exists)
                    {
                        errors[fieldName] = $"{valType} already exists.";
                    }
                }

                await ValidateField(data.FirstName, "First Name", "firstName");
                await ValidateField(data.LastName, "Last Name", "lastName");
                await ValidateField(data.MobileNumber, "Mobile Number", "mobileNumber");

                if (errors.Any())
                {
                    return BadRequest(new { errors });
                }

                var parameters = new DynamicParameters();
                parameters.Add("@customerId", data.customerId);
                parameters.Add("@FirstName", data.FirstName);
                parameters.Add("@LastName", data.LastName);
                parameters.Add("@FullName", data.FullName);
                parameters.Add("@MobileNumber", data.MobileNumber);
                parameters.Add("@City", data.City);
                parameters.Add("@IsActive", data.IsActive);
                var res = await db.QueryFirstOrDefaultAsync<CustomerGVM>("UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

            
        }

        [HttpDelete]
        public async Task<CustomerGVM> DeleteCustomer(int customerId)
        {
            using var db = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@customerId", customerId);
            return await db.QueryFirstOrDefaultAsync<CustomerGVM>("", parameters, commandType: CommandType.StoredProcedure);
        }



    }
}
