using System;
using System.Collections.Generic;

namespace CommonLibrary.Models
{
    public class PurchaseOrdersGVM
    {
        public int? purchaseOrderID { get; set; }
        public int? customerID { get; set; }
        public DateTime dateOfDelivery { get; set; }
        public string status { get; set; }
        public double amountDue { get; set; }
        public bool isActive { get; set; }
    }

    public class vwPurchaseOrdersGVM : PurchaseOrdersGVM
    {
        public string customerName { get; set; }
        public string purchaseItemJson { get; set; } = "";
        public List<PurchaseItemGVM> purchaseItems { get; set; } = new List<PurchaseItemGVM>();
    }

    public class PurchaseItemGVM
    {
        public int? purchaseItem {  get; set; }
        public int? purchaseOrderID { get; set; }
        public int skuId { get; set; }
        public string skuName { get; set; }
        public int quantity { get; set; }
        public double unitPrice { get; set; }
        public double totalPrice { get; set; }
        public bool isActive { set; get; } = true;
    }
}
