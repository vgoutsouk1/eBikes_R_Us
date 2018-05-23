using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class ShoppingCartListPOCO
    {
        public int PartID { get; set; }
	    public string Description { get; set; }
	    public int Quantity { get; set; }
	    public decimal UnitPrice { get; set; }
	    public decimal TotalPrice { get; set; }

    }
}
