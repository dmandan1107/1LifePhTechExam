using CommonLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TechExamAPI.Interface;

namespace TechExamAPI.Implementation
{
    public class CustomerService : ICustomerService
    {
        public IRepositoryService _repo;
        public CustomerService(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<CustomerGVM> GetCustomer(int customerId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CustomerID", customerId);
                var res = await _repo.GetData<CustomerGVM>("GetCustomer", parameters);
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
            try
            {
                var res = await _repo.GetAllData<CustomerGVM>("GetAllCustomer");
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

        public async Task<Dictionary<string, string>> ValidateField(string input, string valType, string fieldName, int customerId = 0)
        {
            var errors = new Dictionary<string, string>();
            var param = new DynamicParameters();
            param.Add("@CustomerID", customerId);
            param.Add("@Input", input);
            param.Add("@ValType", valType);
            var exists = await _repo.GetData<bool>("ValidateInput", param);
            if (exists)
            {
                errors[fieldName] = $"{valType} already exists.";
            }

            return errors;
        }

        public async Task<CustomerGVM> CreateCustomer([FromBody] CustomerGVM data)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", data.FirstName);
                parameters.Add("@LastName", data.LastName);
                parameters.Add("@FullName", data.FullName);
                parameters.Add("@MobileNumber", data.MobileNumber);
                parameters.Add("@City", data.City);
                parameters.Add("@IsActive", true);
                var res = await _repo.GetData<CustomerGVM>("CreateCustomer", parameters);

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

        }

        public async Task<CustomerGVM> UpdateCustomer([FromBody] CustomerGVM data)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@customerId", data.customerId);
                parameters.Add("@FirstName", data.FirstName);
                parameters.Add("@LastName", data.LastName);
                parameters.Add("@FullName", data.FullName);
                parameters.Add("@MobileNumber", data.MobileNumber);
                parameters.Add("@City", data.City);
                var res = await _repo.GetData<CustomerGVM>("UpdateCustomer", parameters);

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }


        }

        public async Task<CustomerGVM> DeleteCustomer(int customerId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@customerId", customerId);
            return await _repo.GetData<CustomerGVM>("DeleteCustomer", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
