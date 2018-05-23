using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class PurchaseOrderDetailsPOCO
    {
        public int PurchaseOrderID { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public int PartID { get; set; }
        public string Description { get; set; }
        public int QuantityOnOrder { get; set; }
        public int QuantityOutstanding { get; set; } //MIGHT be changed 
    }
}
