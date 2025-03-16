using CommonLibrary.Models;
using Dapper;
using System.Text.Json;
using TechExamAPI.Interface;

namespace TechExamAPI.Implementation
{
    public class OrderService : IOrderService
    {
        public IRepositoryService _repo;
        JsonSerializerOptions options;

        public OrderService(IRepositoryService repo)
        {
            _repo = repo;
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }        

        public async Task<vwPurchaseOrdersGVM> GetOrder(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PurchaseOrderID", id);
                var res = await _repo.GetData<vwPurchaseOrdersGVM>("GetPurchaseOrder", parameters);
                res.purchaseItems = string.IsNullOrEmpty(res.purchaseItemJson) ?
                                        new List<PurchaseItemGVM>() : 
                                        JsonSerializer.Deserialize<List<PurchaseItemGVM>>(res.purchaseItemJson, options);
                res.purchaseItemJson = "";
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Failed to fetch the data");
            }
        }

        public async Task<List<vwPurchaseOrdersGVM>> GetAllOrder()
        {
            try
            {
                var res = await _repo.GetAllData<vwPurchaseOrdersGVM>("GetAllPurchaseOrder");
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
                var param = new DynamicParameters();
                param.Add("@customerID", data.customerID);
                param.Add("@DateOfDelivery", DateTime.Now);
                param.Add("@status", data.status);
                param.Add("@amountDue", data.amountDue);
                param.Add("@isActive", true);
                param.Add("@purchaseItemJson", JsonSerializer.Serialize(data.purchaseItems));
                var res = await _repo.GetData<vwPurchaseOrdersGVM>("CreateNewPurchaseOrder", param);

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
                var param = new DynamicParameters();
                param.Add("@purchaseOrderID", data.purchaseOrderID);
                param.Add("@customerID", data.customerID);
                param.Add("@DateOfDelivery", DateTime.Now);
                param.Add("@status", data.status);
                param.Add("@amountDue", data.amountDue);
                param.Add("@isActive", true);
                param.Add("@purchaseItemJson", JsonSerializer.Serialize(data.purchaseItems));
                var res = await _repo.GetData<vwPurchaseOrdersGVM>("UpdatePurchaseOrder", param);

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
