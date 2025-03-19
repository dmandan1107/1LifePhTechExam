using CommonLibrary.Models;
using Dapper;
using MediatR;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Sku.Querry
{

    public class GetAllDataQuerry : IRequest<List<SKUGVM>>
    {

    }

    public class GetAllDataQuerryHandler : IRequestHandler<GetAllDataQuerry, List<SKUGVM>>
    {
        public readonly IRepositoryService _repo;

        public GetAllDataQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<List<SKUGVM>> Handle(GetAllDataQuerry request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetAllData<SKUGVM>("GetAllSKU", null);
            return res;
        }
    }
}
