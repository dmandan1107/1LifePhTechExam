using CommonLibrary.Models;

namespace TechExamAPI.Interface
{
    public interface ISkuService
    {
        Task<SKUGVM> GetSKU(int SKUId);
        Task<List<SKUGVM>> GetAllSKU();
        Task<SKUGVM> CreateSKU(SKUGVM data);
        Task UpdateSKUImage(SKUGVM data);
        Task<SKUGVM> UpdateSKU(SKUGVM data);
        Task<SKUGVM> DeleteSKU(int SKUId);
    }
}
