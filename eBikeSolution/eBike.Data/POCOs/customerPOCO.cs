using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class customerPOCO
    {
        public int CustomerID { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        private string name;

        public string Name
        {
            get { return LastName + ", " + FirstName; }

        }

    }
}

