using CommonLibrary.Models;
using Dapper;
using MediatR;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Order.Querry
{

    public class GetDataQuerry : IRequest<vwPurchaseOrdersGVM>
    {
        public int id { get; set; }
        public GetDataQuerry(int _id) { id = _id; }
    }

    public class GetOrderQuerryHandler : IRequestHandler<GetDataQuerry, vwPurchaseOrdersGVM>
    {
        public readonly IRepositoryService _repo;

        public GetOrderQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<vwPurchaseOrdersGVM> Handle(GetDataQuerry request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PurchaseOrderID", request.id);
            var res = await _repo.GetData<vwPurchaseOrdersGVM>("GetPurchaseOrder", parameters);
            return res;
        }
    }
}
