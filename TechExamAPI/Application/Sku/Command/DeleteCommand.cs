using CommonLibrary.Models;
using Dapper;
using MediatR;
using System;
using System.Data;
using System.Text.Json;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechExamAPI.Application.Sku.Command
{
    public class DeleteCommand : IRequest<SKUGVM>
    {
        public int id;

        public DeleteCommand(int _id)
        {
            this.id = _id;
        }   
    }

    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, SKUGVM>
    {
        public readonly IRepositoryService _repo;

        public DeleteCommandHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<SKUGVM> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SKUId", request.id);
            var res = await _repo.GetData<SKUGVM>("DeleteSKU", parameters);
            return res;
        }
    }

}
