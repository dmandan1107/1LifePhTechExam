using CommonLibrary.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TechExamAPI.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using MediatR;
using TechExamAPI.Application.Sku.Querry;
using TechExamAPI.Application.Sku.Command;

namespace TechExamAPI.Implementation
{
    public class SkuService : ISkuService
    {
        public readonly IMediator _mediatr;
        public SkuService(IMediator mediator)
        {
            _mediatr = mediator;
        }

        public async Task<SKUGVM> GetSKU(int SKUId)
        {
            try
            {
                var res = await _mediatr.Send(new GetDataQuerry(SKUId));
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
            var res = await _mediatr.Send(new GetAllDataQuerry());
            return res;
        }

        public async Task<Dictionary<string, string>> ValidateField(string input, string valType, string fieldName, int skuID = 0)
        {
            return await _mediatr.Send(new ValidateFieldQuerry(input, valType, fieldName, skuID));
        }

        public async Task<SKUGVM> CreateSKU(SKUGVM data)
        {
            try
            {
                var res = await _mediatr.Send(new CreateCommand(data));
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
                await _mediatr.Send(new UpdateSkuImageCommand(data));
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
                var res = await _mediatr.Send(new UpdateCommand(data));
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
                var res = await _mediatr.Send(new DeleteCommand(SKUId));
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to save the data");
            }

            
        }
    }
}
