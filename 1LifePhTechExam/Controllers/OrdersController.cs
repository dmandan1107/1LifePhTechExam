using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using CommonLibrary.Models;

namespace _1LifePhTechExam.Controllers
{
    public class OrdersController : Controller
    {
        private readonly string _connectionString;
        public OrdersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultDataContext");
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<List<CustomerGVM>> GetAllCustomer(string search = "")
        {
            using var db = new SqlConnection(_connectionString);
            var res = (await db.QueryAsync<CustomerGVM>("GetAllCustomer", null, commandType: CommandType.StoredProcedure)).ToList();

            if (!string.IsNullOrEmpty(search))
            { 
                res = res.Where(a => a.FullName.ToUpper().Contains(search.ToUpper()) && a.IsActive).ToList();
                return res;
            }

            return res;
        }

        public async Task<List<SKUGVM>> GetAllSKU(string search = "")
        {
            using var db = new SqlConnection(_connectionString);
            var res = (await db.QueryAsync<SKUGVM>("GetAllSKU", null, commandType: CommandType.StoredProcedure)).ToList();

            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(a => a.SkuName.ToUpper().Contains(search.ToUpper()) && a.IsActive).ToList();
                return res;
            }

            return res;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] vwPurchaseOrdersGVM data)
        {
            return Ok();
        }
    }
}
