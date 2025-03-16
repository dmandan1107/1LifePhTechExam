using Microsoft.AspNetCore.Http;

namespace CommonLibrary.Models
{
    public class SKUGVM
    {
        public int SkuID { get; set; }
        public string SkuName { get; set; }
        public string SkuCode { get; set; }
        public double UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
