using CommonLibrary.Models;
using Dapper;
using MediatR;
using System;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Sku.Querry
{

    public class ValidateFieldQuerry : IRequest<Dictionary<string, string>>
    {
        public string input;
        public string valType;
        public string fieldName;
        public int skuID;
        public ValidateFieldQuerry(string _input, string _valType, string _fieldName, int _skuID) 
        {
            input = _input;
            valType = _valType;
            fieldName = _fieldName;
            skuID = _skuID;
        }
    }

    public class ValidateFieldQuerryHandler : IRequestHandler<ValidateFieldQuerry, Dictionary<string, string>>
    {
        public readonly IRepositoryService _repo;

        public ValidateFieldQuerryHandler(IRepositoryService repo)
        {
            _repo = repo;
        }

        public async Task<Dictionary<string, string>> Handle(ValidateFieldQuerry request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string>();
            var param = new DynamicParameters();
            param.Add("@@SkuID", request.skuID);
            param.Add("@Input", request.input);
            param.Add("@ValType", request.valType);
            var exists = await _repo.GetData<bool>("ValidateSKUInput", param);
            if (exists)
            {
                errors[request.fieldName] = $"{request.valType} already exists.";
            }

            return errors;
        }
    }
}
