using CommonLibrary.Models;
using Dapper;
using MediatR;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Order.Querry
{

    public class GetAllDataQuerry : IRequest<List<vwPurchaseOrdersGVM>>
    {

    }

    public class GetAllOrderQuerryHandler : IRequestHandler<GetAllDataQuerry, List<vwPurchaseOrdersGVM>>
    {
        public readonly IRepositoryService _repo;

        public GetAllOrderQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<List<vwPurchaseOrdersGVM>> Handle(GetAllDataQuerry request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetAllData<vwPurchaseOrdersGVM>("GetAllPurchaseOrder", null);
            return res;
        }
    }
}
