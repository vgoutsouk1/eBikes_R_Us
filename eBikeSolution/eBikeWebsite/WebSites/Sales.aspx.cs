using eBike.Data.Entities.Security;
using eBikeSystem.BLL;
using eBikeSystem.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSites_Sales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // are you logged on?
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                // now that you are logged on, are you in the approved role for this page?
                if (!User.IsInRole(SecurityRoles.RegisteredUsers) && !User.IsInRole(SecurityRoles.Sales) && !User.IsInRole(SecurityRoles.WebsiteAdmins))
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }

            if (User.IsInRole(SecurityRoles.Staff))
            {
                var sysmgr = new UserManager();

                string employeename = sysmgr.Get_EmployeeFullName(User.Identity.Name);

                EmployeeNameLabel.Text = "Current user: " + employeename;
            }
        }
    }
 
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void PartsbyCategoryList_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (User.IsInRole(SecurityRoles.RegisteredUsers))
        {
            ListViewDataItem row = e.Item as ListViewDataItem;

            string username = User.Identity.Name;

            int partid = int.Parse(e.CommandArgument.ToString());

            int quantity = int.Parse((row.FindControl("QuantityOrdered") as TextBox).Text.ToString());

            if (quantity < 1)
            {
                MessageUserControl.ShowInfo("Warning", "Quantity must be at least 1");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SalesController sysmgr = new SalesController();
                    sysmgr.Add_ItemToCart(username, partid, quantity);

                }, "Success", "Item added to cart");
            }
            
        }
        else
        {
            MessageUserControl.ShowInfo("Warning", "Sorry, only online customers can shop. Please register an account or login as a customer.");
        }
    }
}