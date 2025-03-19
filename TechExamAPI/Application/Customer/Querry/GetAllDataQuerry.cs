using CommonLibrary.Models;
using Dapper;
using MediatR;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Customer.Querry
{

    public class GetAllDataQuerry : IRequest<List<CustomerGVM>>
    {

    }

    public class GetAllDataQuerryHandler : IRequestHandler<GetAllDataQuerry, List<CustomerGVM>>
    {
        public readonly IRepositoryService _repo;

        public GetAllDataQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerGVM>> Handle(GetAllDataQuerry request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetAllData<CustomerGVM>("GetAllCustomer");
            return res;
        }
    }
}
