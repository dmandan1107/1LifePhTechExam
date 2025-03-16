using CommonLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace TechExamAPI.Interface
{
    public interface ICustomerService
    {
        Task<CustomerGVM> GetCustomer(int customerId);
        Task<List<CustomerGVM>> GetAllCustomer(string search = "");
        Task<Dictionary<string, string>> ValidateField(string input, string valType, string fieldName, int customerId = 0);
        Task<CustomerGVM> CreateCustomer([FromBody] CustomerGVM data);
        Task<CustomerGVM> UpdateCustomer([FromBody] CustomerGVM data);
        Task<CustomerGVM> DeleteCustomer(int customerId);
    }
}
