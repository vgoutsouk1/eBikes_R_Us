using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class PartsListPOCO
    {
        public int PartID { get; set; }
	    public string Description { get; set; }
	    public int InStock { get; set; }
	    public decimal Price { get; set; }
    }
}
