using CommonLibrary.Models;
using Dapper;
using MediatR;
using System;
using TechExamAPI.Interface;

namespace TechExamAPI.Application.Customer.Querry
{

    public class ValidateFieldQuerry : IRequest<Dictionary<string, string>>
    {
        public string input;
        public string valType;
        public string fieldName;
        public int id;
        public ValidateFieldQuerry(string _input, string _valType, string _fieldName, int _id) 
        {
            input = _input;
            valType = _valType;
            fieldName = _fieldName;
            id = _id;
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
            param.Add("@CustomerID", request.id);
            param.Add("@Input", request.input);
            param.Add("@ValType", request.valType);
            var exists = await _repo.GetData<bool>("ValidateInput", param);
            if (exists)
            {
                errors[request.fieldName] = $"{request.valType} already exists.";
            }

            return errors;
        }
    }
}
