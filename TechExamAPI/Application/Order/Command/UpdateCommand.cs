using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Order.Command
{
    public class UpdateCommand : IRequest<vwPurchaseOrdersGVM>
    {
        public vwPurchaseOrdersGVM data;

        public UpdateCommand(vwPurchaseOrdersGVM data)
        {
            this.data = data;
        }   
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, vwPurchaseOrdersGVM>
    {
        public readonly IRepositoryService _repo;

        public UpdateCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<vwPurchaseOrdersGVM> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var param = new DynamicParameters();
            param.Add("@purchaseOrderID", request.data.purchaseOrderID);
            param.Add("@customerID", request.data.customerID);
            param.Add("@status", request.data.status);
            param.Add("@amountDue", request.data.amountDue);
            param.Add("@isActive", true);
            param.Add("@purchaseItemJson", JsonSerializer.Serialize(request.data.purchaseItems));
            var res = await _repo.GetData<vwPurchaseOrdersGVM>("UpdatePurchaseOrder", param);
            return res;

        }
    }

}
