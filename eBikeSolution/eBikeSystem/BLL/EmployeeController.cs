
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities;
using eBikeSystem.DAL;
using eBike.Data.POCOs;
using Microsoft.AspNet.Identity;
#endregion
namespace eBikeSystem.BLL
{
    public class EmployeeController 
    {
        public Employee Employee_Get(int employeeid)
        {
            using (var context = new eBikeContext())
            {
                return context.Employees.Find(employeeid);
            }
        }
    }
}
