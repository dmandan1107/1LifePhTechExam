using TechExamAPI.Interface;
using CommonLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TechExamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService _svc;
        public CustomerController(ICustomerService svc)
        { 
            _svc = svc;
        }

        [HttpGet("getCustomer")]
        public async Task<CustomerGVM> GetCustomer(int customerId)
        {
            try
            {
                var res = await _svc.GetCustomer(customerId);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        [HttpGet("getAllCustomer")]
        public async Task<List<CustomerGVM>> GetAllCustomer(string search = "")
        {
            try
            {
                var res = await _svc.GetAllCustomer(search);
                if (!string.IsNullOrEmpty(search))
                {
                    res = res.Where(a => a.FullName.ToUpper().Contains(search.ToUpper())).ToList();
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
            

            
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerGVM data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var errors = new Dictionary<string, string>();

                var val1 = await _svc.ValidateField(data.FirstName, "First Name", "firstName");
                if (val1.FirstOrDefault().Key != null)
                    errors.Add(val1.FirstOrDefault().Key, val1.FirstOrDefault().Value);

                var val2 = await _svc.ValidateField(data.LastName, "Last Name", "lastName");
                if (val2.FirstOrDefault().Key != null)
                    errors.Add(val2.FirstOrDefault().Key, val2.FirstOrDefault().Value);

                var val3 = await _svc.ValidateField(data.MobileNumber, "Mobile Number", "mobileNumber");
                if (val3.FirstOrDefault().Key != null)
                    errors.Add(val3.FirstOrDefault().Key, val3.FirstOrDefault().Value);

                if (errors.Any())
                {
                    return BadRequest(new { errors });
                }

                var res = await _svc.CreateCustomer(data);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

        }

        [HttpPut("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerGVM data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var errors = new Dictionary<string, string>();

                var val1 = await _svc.ValidateField(data.FirstName, "First Name", "firstName");
                if (val1.FirstOrDefault().Key != null)
                    errors.Add(val1.FirstOrDefault().Key, val1.FirstOrDefault().Value);

                var val2 = await _svc.ValidateField(data.LastName, "Last Name", "lastName");
                if (val2.FirstOrDefault().Key != null)
                    errors.Add(val2.FirstOrDefault().Key, val2.FirstOrDefault().Value);

                var val3 = await _svc.ValidateField(data.MobileNumber, "Mobile Number", "mobileNumber");
                if (val3.FirstOrDefault().Key != null)
                    errors.Add(val3.FirstOrDefault().Key, val3.FirstOrDefault().Value);

                if (errors.Any())
                {
                    return BadRequest(new { errors });
                }

                var res = await _svc.UpdateCustomer(data);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }


        }

        [HttpDelete("deleteCustomer")]
        public async Task<CustomerGVM> DeleteCustomer(int customerId)
        {
            return await _svc.DeleteCustomer(customerId);
        }
    }
}
