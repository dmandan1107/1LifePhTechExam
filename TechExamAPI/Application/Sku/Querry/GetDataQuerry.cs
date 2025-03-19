using CommonLibrary.Models;
using Dapper;
using MediatR;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Sku.Querry
{

    public class GetDataQuerry : IRequest<SKUGVM>
    {
        public int id { get; set; }
        public GetDataQuerry(int _id) { id = _id; }
    }

    public class GetDataQuerryHandler : IRequestHandler<GetDataQuerry, SKUGVM>
    {
        public readonly IRepositoryService _repo;

        public GetDataQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<SKUGVM> Handle(GetDataQuerry request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SKUID", request.id);
            var res = await _repo.GetData<SKUGVM>("GetSKU", parameters);
            return res;
        }
    }
}
