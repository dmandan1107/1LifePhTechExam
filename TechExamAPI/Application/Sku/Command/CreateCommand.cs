using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Sku.Command
{
    public class CreateCommand : IRequest<SKUGVM>
    {
        public SKUGVM data;

        public CreateCommand(SKUGVM data)
        {
            this.data = data;
        }   
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand, SKUGVM>
    {
        public readonly IRepositoryService _repo;

        public CreateCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<SKUGVM> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SkuName", request.data.SkuName);
            parameters.Add("@SkuCode", request.data.SkuCode);
            parameters.Add("@UnitPrice", request.data.UnitPrice);
            parameters.Add("@IsActive", true);
            var res = await _repo.GetData<SKUGVM>("CreateSKU", parameters);
            return res;
        }
    }

}
