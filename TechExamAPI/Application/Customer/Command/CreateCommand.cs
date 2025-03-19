using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Customer.Command
{
    public class CreateCommand : IRequest<CustomerGVM>
    {
        public CustomerGVM data;

        public CreateCommand(CustomerGVM data)
        {
            this.data = data;
        }   
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand, CustomerGVM>
    {
        public readonly IRepositoryService _repo;

        public CreateCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<CustomerGVM> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", request.data.FirstName);
            parameters.Add("@LastName", request.data.LastName);
            parameters.Add("@FullName", request.data.FullName);
            parameters.Add("@MobileNumber", request.data.MobileNumber);
            parameters.Add("@City", request.data.City);
            parameters.Add("@IsActive", true);
            var res = await _repo.GetData<CustomerGVM>("CreateCustomer", parameters);
            return res;
        }
    }

}
