using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class OutstandingOrdersPOCO
    {
        public int PurchaseOrderID { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Vendor { get; set; }
        public string Phone { get; set; }

    }
}
