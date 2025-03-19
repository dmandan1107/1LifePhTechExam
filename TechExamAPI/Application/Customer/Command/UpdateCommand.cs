using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Customer.Command
{
    public class UpdateCommand : IRequest<CustomerGVM>
    {
        public CustomerGVM data;

        public UpdateCommand(CustomerGVM data)
        {
            this.data = data;
        }   
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, CustomerGVM>
    {
        public readonly IRepositoryService _repo;

        public UpdateCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<CustomerGVM> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@customerId", request.data.customerId);
            parameters.Add("@FirstName", request.data.FirstName);
            parameters.Add("@LastName", request.data.LastName);
            parameters.Add("@FullName", request.data.FullName);
            parameters.Add("@MobileNumber", request.data.MobileNumber);
            parameters.Add("@City", request.data.City);
            var res = await _repo.GetData<CustomerGVM>("UpdateCustomer", parameters);
            return res;
        }
    }

}
