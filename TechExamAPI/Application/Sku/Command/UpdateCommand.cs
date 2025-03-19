using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Sku.Command
{
    public class UpdateCommand : IRequest<SKUGVM>
    {
        public SKUGVM data;

        public UpdateCommand(SKUGVM data)
        {
            this.data = data;
        }   
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, SKUGVM>
    {
        public readonly IRepositoryService _repo;

        public UpdateCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<SKUGVM> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SkuID", request.data.SkuID);
            parameters.Add("@SkuName", request.data.SkuName);
            parameters.Add("@SkuCode", request.data.SkuCode);
            parameters.Add("@UnitPrice", request.data.UnitPrice);
            var res = await _repo.GetData<SKUGVM>("UpdateSKU", parameters);
            return res;
        }
    }

}
