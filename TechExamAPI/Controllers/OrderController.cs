using CommonLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TechExamAPI.Interface;

namespace TechExamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _svc;
        public ICustomerService _cs;
        public ISkuService _sku;
        public OrderController(IOrderService svc, ICustomerService cs, ISkuService sku)
        {
            _svc = svc;
            _cs = cs;
            _sku = sku;
        }

        [HttpGet("getAllCustomer")]
        public async Task<List<CustomerGVM>> GetAllCustomer(string search = "")
        {            
            var res = await _cs.GetAllCustomer(search);

            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(a => a.FullName.ToUpper().Contains(search.ToUpper()) && a.IsActive).ToList();
                return res.Take(10).ToList();
            }

            return res.Take(10).ToList();
        }

        [HttpGet("getAllSKU")]
        public async Task<List<SKUGVM>> GetAllSKU(string search = "")
        {
            var res = await _sku.GetAllSKU();

            if (!string.IsNullOrEmpty(search))
            {
                res = res.Where(a => a.SkuName.ToUpper().Contains(search.ToUpper()) && a.IsActive).ToList();
                return res.Take(10).ToList();
            }

            return res.Take(10).ToList();
        }

        [HttpGet("getOrderDetails")]
        public async Task<vwPurchaseOrdersGVM> GetOrderDetails(int purchaseOrderID)
        {
            var res = await _svc.GetOrder(purchaseOrderID);

            return res;
        }

        [HttpGet("getAllOrder")]
        public async Task<List<vwPurchaseOrdersGVM>> GetAllOrder()
        {
            var res = await _svc.GetAllOrder();           

            return res;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] vwPurchaseOrdersGVM data)
        {
            var res = await _svc.CreateNewOrder(data);

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] vwPurchaseOrdersGVM data)
        {
            var res = await _svc.UpdateOrder(data);

            return Ok();
        }

    }
}
