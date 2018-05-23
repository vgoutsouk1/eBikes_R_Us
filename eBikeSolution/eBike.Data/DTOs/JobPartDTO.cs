using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.DTOs
{
    public class JobPartDTO
    {
        public string Description { get; set; }
        public int PartID { get; set; }
        public int Quantity { get; set; }
    }
}
