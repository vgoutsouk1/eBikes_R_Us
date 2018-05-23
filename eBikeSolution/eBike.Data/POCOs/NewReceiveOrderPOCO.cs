using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class NewReceiveOrderPOCO
    {
        public int PurchaseOrderID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public int PartID { get; set; }
        public string PartDescription { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityReturned { get; set; }
        public int Outstanding { get; set; }
        public string Notes { get; set; }
    }
}
