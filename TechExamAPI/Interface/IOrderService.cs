using CommonLibrary.Models;

namespace TechExamAPI.Interface
{
    public interface IOrderService
    {
        Task<vwPurchaseOrdersGVM> GetOrder(int id);
        Task<List<vwPurchaseOrdersGVM>> GetAllOrder();
        Task<vwPurchaseOrdersGVM> CreateNewOrder(vwPurchaseOrdersGVM data);
        Task<vwPurchaseOrdersGVM> UpdateOrder(vwPurchaseOrdersGVM data);
    }
}
