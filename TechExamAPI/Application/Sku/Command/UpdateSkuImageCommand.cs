using CommonLibrary.Models;
using Dapper;
using MediatR;
using System.Data;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Sku.Command
{
    public class UpdateSkuImageCommand : IRequest
    {
        public SKUGVM data;

        public UpdateSkuImageCommand(SKUGVM data)
        {
            this.data = data;
        }   
    }

    public class UpdateSkuImageCommandHandler : IRequestHandler<UpdateSkuImageCommand>
    {
        public readonly IRepositoryService _repo;

        public UpdateSkuImageCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task Handle(UpdateSkuImageCommand request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SkuID", request.data.SkuID, DbType.Int32);
            parameters.Add("@FileName", request.data.ImageFile.FileName, DbType.String);

            await _repo.Execute("UpdateSKUImage", parameters);
        }
    }

}
