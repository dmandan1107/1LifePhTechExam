using CommonLibrary.Models;
using Dapper;
using MediatR;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Customer.Querry
{

    public class GetDataQuerry : IRequest<CustomerGVM>
    {
        public int id { get; set; }
        public GetDataQuerry(int _id) { id = _id; }
    }

    public class GetDataQuerryHandler : IRequestHandler<GetDataQuerry, CustomerGVM>
    {
        public readonly IRepositoryService _repo;

        public GetDataQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<CustomerGVM> Handle(GetDataQuerry request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", request.id);
            var res = await _repo.GetData<CustomerGVM>("GetCustomer", parameters);
            return res;
        }
    }
}
