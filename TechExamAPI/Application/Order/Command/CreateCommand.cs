using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Order.Command
{
    public class CreateCommand : IRequest<vwPurchaseOrdersGVM>
    {
        public vwPurchaseOrdersGVM data;

        public CreateCommand(vwPurchaseOrdersGVM data)
        {
            this.data = data;
        }   
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand, vwPurchaseOrdersGVM>
    {
        public readonly IRepositoryService _repo;

        public CreateCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<vwPurchaseOrdersGVM> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var param = new DynamicParameters();
            param.Add("@customerID", request.data.customerID);
            param.Add("@DateOfDelivery", request.data.dateOfDelivery);
            param.Add("@status", request.data.status);
            param.Add("@amountDue", request.data.amountDue);
            param.Add("@isActive", true);
            param.Add("@purchaseItemJson", JsonSerializer.Serialize(request.data.purchaseItems));
            var res = await _repo.GetData<vwPurchaseOrdersGVM>("CreateNewPurchaseOrder", param);
            return res;
        }
    }

}
