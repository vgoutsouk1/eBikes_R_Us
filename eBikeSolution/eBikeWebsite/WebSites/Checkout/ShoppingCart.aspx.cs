using eBike.Data.Entities;
using eBike.Data.Entities.Security;
using eBikeSystem.BLL;
using eBikeSystem.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSites_Checkout_ShoppingCart : System.Web.UI.Page
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
                else
                {
                    UserLabel.Text = User.Identity.Name;
                }
            }

            if (User.IsInRole(SecurityRoles.Staff))
            {
                var sysmgr = new UserManager();

                string employeename = sysmgr.Get_EmployeeFullName(User.Identity.Name);

                EmployeeNameLabel.Text = "Current user: " + employeename;
            }

            
        }
        if (User.IsInRole(SecurityRoles.RegisteredUsers))
        {
            var sysmgr = new SalesController();

            List<Part> backordered = sysmgr.Check_ForBackorders(User.Identity.Name);

            if (backordered.Count > 0)
            {
                string backorderalert = "The following items are low in stock and will be backordered: ";

                foreach (Part part in backordered)
                {
                    backorderalert += part.Description + " ";
                }

                backorderalertlabel.Visible = true;
                backorderalertlabel.Text = "Warning! " + backorderalert;
            }
            else
            {
                backorderalertlabel.Visible = false;
            }
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void ShoppingCartList_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ListViewDataItem row = e.Item as ListViewDataItem;

        string username = User.Identity.Name;

        int partid = int.Parse(e.CommandArgument.ToString());

        SalesController sysmgr = new SalesController();

        if (e.CommandName == "Change")
        {
            int quantity = int.Parse((row.FindControl("QuantityTextBox") as TextBox).Text.ToString());

            if (quantity < 1)
            {
                MessageUserControl.ShowInfo("Warning", "Quantity must be at least 1");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {

                    sysmgr.Update_CartItem(username, partid, quantity);

                }, "Success", "Item quantity updated");
            }

            
        }
        else
        {
            MessageUserControl.TryRun(() =>
            {

                sysmgr.Remove_CartItem(username, partid);

            }, "Success", "Item removed");
        }

        ShoppingCartList.DataBind();

        TotalsGridView.DataBind();

        Page_Load(this, e);
    }
}