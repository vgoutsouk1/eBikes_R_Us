using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.Entities.Security
{
    public static class SecurityRoles
    {
        public const string WebsiteAdmins = "WebsiteAdmins";
        public const string RegisteredUsers = "RegisteredUsers";
        public const string Staff = "Staff";
        public const string Purchasing = "Purchasing";
        public const string Receiving = "Receiving";
        public const string Sales = "Sales";
        public const string Jobing = "Jobing";

        public static List<string> DefaultSecurityRoles
        {
            get
            {
                List<string> value = new List<string>();
                value.Add(WebsiteAdmins);
                value.Add(RegisteredUsers);
                value.Add(Staff);
                value.Add(Purchasing);
                value.Add(Receiving);
                value.Add(Sales);
                value.Add(Jobing);
                return value;
            }
        }
    }
}
