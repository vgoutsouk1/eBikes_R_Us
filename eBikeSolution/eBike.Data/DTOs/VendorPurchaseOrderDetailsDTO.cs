using eBike.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.DTOs
{
 public class VendorPurchaseOrderDetailsDTO
    {
        public int? PurchaseOrderNumber { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
        public List<PurchaseOrderDetailsPOCO> PODetails { get; set; }
    }
}
