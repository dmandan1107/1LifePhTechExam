using CommonLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace TechExamAPI.Implementation
{
    public class SkuService : ISkuService
    {
        public IRepositoryService _repo;
        public SkuService(IRepositoryService repo) 
        {
            _repo = repo;
        }

        public async Task<SKUGVM> GetSKU(int SKUId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SKUID", SKUId);
                var res = await _repo.GetData<SKUGVM>("GetSKU", parameters);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        public async Task<List<SKUGVM>> GetAllSKU()
        {
            var res = (await _repo.GetAllData<SKUGVM>("GetAllSKU")).ToList();
            return res;
        }

        public async Task<Dictionary<string, string>> ValidateField(string input, string valType, string fieldName, int skuID = 0)
        {
            var errors = new Dictionary<string, string>();
            var param = new DynamicParameters();
            param.Add("@@SkuID", skuID);
            param.Add("@Input", input);
            param.Add("@ValType", valType);
            var exists = await _repo.GetData<bool>("ValidateSKUInput", param);
            if (exists)
            {
                errors[fieldName] = $"{valType} already exists.";
            }

            return errors;
        }

        public async Task<SKUGVM> CreateSKU(SKUGVM data)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SkuName", data.SkuName);
                parameters.Add("@SkuCode", data.SkuCode);
                parameters.Add("@UnitPrice", data.UnitPrice);
                parameters.Add("@IsActive", true);
                var res = await _repo.GetData<SKUGVM>("CreateSKU", parameters);             

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

        }

        public async Task UpdateSKUImage(SKUGVM data)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SkuID", data.SkuID, DbType.Int32);
                parameters.Add("@FileName", data.ImageFile.FileName, DbType.String);

                await _repo.Execute("UpdateSKUImage", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

        }

        public async Task<SKUGVM> UpdateSKU(SKUGVM data)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SkuID", data.SkuID);
                parameters.Add("@SkuName", data.SkuName);
                parameters.Add("@SkuCode", data.SkuCode);
                parameters.Add("@UnitPrice", data.UnitPrice);
                var res = await _repo.GetData<SKUGVM>("UpdateSKU", parameters);

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }
        }

        public async Task<SKUGVM> DeleteSKU(int SKUId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SKUId", SKUId);
                return await _repo.GetData<SKUGVM>("DeleteSKU", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

            
        }
    }
}
