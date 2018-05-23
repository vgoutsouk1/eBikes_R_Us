using eBike.Data.Entities;
using eBike.Data.Entities.Security;
using eBike.Data.POCOs;
using eBikeSystem.BLL;
using eBikeSystem.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSites_Checkout_PlaceOrder : System.Web.UI.Page
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

            }
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void CouponRefreshBtn_Click(object sender, EventArgs e)
    {
        CouponListDD.SelectedIndex = CouponListDD.SelectedIndex;
        FinalTotalODS.DataBind();
    }

    protected void PlaceOrderBtn_Click(object sender, EventArgs e)
    {
        string username = User.Identity.Name;

        int couponid = int.Parse(CouponListDD.SelectedValue);

        FinalTotalPOCO totals = new FinalTotalPOCO();

        totals.SubTotal = decimal.Parse(TotalsGridView.DataKeys[0].Values[0].ToString());
        totals.Discount = decimal.Parse(TotalsGridView.DataKeys[0].Values[1].ToString());
        totals.GST = decimal.Parse(TotalsGridView.DataKeys[0].Values[2].ToString());
        totals.Total = decimal.Parse(TotalsGridView.DataKeys[0].Values[3].ToString());

        string paymethod = PaymentMethodRB.SelectedValue;

        MessageUserControl.TryRun(() =>
        {
            SalesController sysmgr = new SalesController();
            List<Part> backordered = sysmgr.Place_Order(username, couponid, totals, paymethod);

            if (backordered.Count > 0)
            {
                backorderalertlabel.Text = "";
                string backorderalert = "The following items have been backordered: ";

                foreach (Part part in backordered)
                {
                    backorderalert += part.Description + " ";
                }

                backorderalertlabel.Visible = true;
                backorderalertlabel.Text = backorderalert;
            }

            ShoppingCartList.DataBind();
            TotalsGridView.DataBind();

        }, "Success", "Your order has been processed.");

        
    }
}