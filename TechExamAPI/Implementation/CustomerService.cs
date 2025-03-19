using CommonLibrary.Models;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TechExamAPI.Application.Customer.Command;
using TechExamAPI.Application.Customer.Querry;
using TechExamAPI.Interface;

namespace TechExamAPI.Implementation
{
    public class CustomerService : ICustomerService
    {
        public IMediator _mediatr;
        public CustomerService(IMediator mediator)
        {
            _mediatr = mediator;
        }

        public async Task<CustomerGVM> GetCustomer(int customerId)
        {
            try
            {
                var res = await _mediatr.Send(new GetDataQuerry(customerId));
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
                var res = await _mediatr.Send(new GetAllDataQuerry());
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
            return await _mediatr.Send(new ValidateFieldQuerry(input, valType, fieldName, customerId));
        }

        public async Task<CustomerGVM> CreateCustomer([FromBody] CustomerGVM data)
        {
            try
            {
                var res = await _mediatr.Send(new CreateCommand(data));

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
                var res = await _mediatr.Send(new UpdateCommand(data));

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
            var res = await _mediatr.Send(new DeleteCommand(customerId));
            return res;
        }
    }
}
