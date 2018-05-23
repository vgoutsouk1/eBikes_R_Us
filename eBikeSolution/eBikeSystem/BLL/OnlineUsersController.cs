using eBike.Data.Entities;
using eBikeSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBikeSystem.BLL
{
    public class OnlineUsersController
    {

        public void Add_OnlineCustomer (string username)
        {
            using (var context = new eBikeContext())
            {
                var customer = new OnlineCustomer();

                customer.UserName = username;
                customer.CreatedOn = DateTime.Now;

                
                context.OnlineCustomers.Add(customer);

                context.SaveChanges();
            }
        }

        public void Remove_OnlineCustomer(string username)
        {
            using (var context = new eBikeContext())
            {
                var customer = (from x in context.OnlineCustomers
                               where (x.UserName).Equals(username)
                               select x).SingleOrDefault();

                if (customer != null)
                {
                    context.OnlineCustomers.Remove(customer);
                    context.SaveChanges();
                }
            }
        }
    }
}
