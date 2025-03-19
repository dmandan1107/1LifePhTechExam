using CommonLibrary.Models;
using Dapper;
using System.Text.Json;
using TechExamAPI.Interface;
using MediatR;
using TechExamAPI.Application.Order.Querry;
using TechExamAPI.Application.Order.Command;
namespace TechExamAPI.Implementation
{
    public class OrderService : IOrderService
    {
        public readonly IMediator _mediatr;
        JsonSerializerOptions options;

        public OrderService(IMediator mediator)
        {
            _mediatr = mediator;
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<vwPurchaseOrdersGVM> GetOrder(int id)
        {
            try
            {;
                var res = await _mediatr.Send(new GetDataQuerry(id));
                res.purchaseItems = string.IsNullOrEmpty(res.purchaseItemJson) ?
                                        new List<PurchaseItemGVM>() :
                                        JsonSerializer.Deserialize<List<PurchaseItemGVM>>(res.purchaseItemJson, options);
                res.purchaseItemJson = "";
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<vwPurchaseOrdersGVM>> GetAllOrder()
        {
            try
            {
                var res = await _mediatr.Send(new GetAllDataQuerry());
                res.ForEach(v => v.purchaseItems = 
                            string.IsNullOrEmpty(v.purchaseItemJson) ? 
                            new List<PurchaseItemGVM>() : 
                            JsonSerializer.Deserialize<List<PurchaseItemGVM>>(v.purchaseItemJson, options));
                res.ForEach(v => v.purchaseItemJson = "");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        public async Task<vwPurchaseOrdersGVM> CreateNewOrder(vwPurchaseOrdersGVM data)
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

        public async Task<vwPurchaseOrdersGVM> UpdateOrder(vwPurchaseOrdersGVM data)
        {
            try
            {
                var res = await _mediatr.Send(new UpdateCommand(data));

                return res;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to update the data");
            }
        }
    }
}
