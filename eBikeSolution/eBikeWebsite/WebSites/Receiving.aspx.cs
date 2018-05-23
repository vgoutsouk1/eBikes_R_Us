using eBike.Data.Entities.Security;
using eBikeSystem.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eBikeSystem.BLL;
using eBike.Data.DTOs;
using eBike.Data.Entities;
using eBike.Data.POCOs;


public partial class WebSites_Receiving : System.Web.UI.Page
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
                if (!User.IsInRole(SecurityRoles.Receiving) && !User.IsInRole(SecurityRoles.WebsiteAdmins))
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
        //Message.Text = "";
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void ViewOrder_Click(object sender, EventArgs e)
    {
        int  poID = 0;
        //int poNumber = 0;

        GridViewRow item = (GridViewRow)((LinkButton)sender).NamingContainer; // gets info from one row.
        poID = int.Parse(((Label)item.FindControl("PurchaseOrderId")).Text);

        ReceivingController sysmng = new ReceivingController();
        VendorPurchaseOrderDetailsDTO vendorPODetails = sysmng.GetPODetails(poID);

        lblPONumber.Text = vendorPODetails.PurchaseOrderNumber.ToString();
        lblVendorName.Text = vendorPODetails.VendorName;
        lblVendorPhone.Text = vendorPODetails.VendorPhone;
        //DataSource for the GridView
        PODetailsGV.DataSource = vendorPODetails.PODetails;
        PODetailsGV.DataBind();        
    }

    protected void ForceCloser_Click(object sender, EventArgs e)
    {
       
            if (string.IsNullOrEmpty(txtReasonFC.Text))
            {
                MessageUserControl.ShowInfo("Warning", "Please provide reason for closing the purchase order.");
            }
            else
            {
                ReceivingController sysmng = new ReceivingController();

                PurchaseOrder poData = new PurchaseOrder();
                //get value to update PO table
                poData.PurchaseOrderID = int.Parse(((Label)PODetailsGV.Rows[0].FindControl("PurchaseOrderID")).Text);
                poData.Notes = txtReasonFC.Text;
                poData.Closed = true;

                MessageUserControl.TryRun(() =>
                {
                    List<PurchaseOrderDetailsPOCO> poDetailsList = new List<PurchaseOrderDetailsPOCO>();
                    foreach (GridViewRow row in PODetailsGV.Rows)
                    {
                        PurchaseOrderDetailsPOCO data = new PurchaseOrderDetailsPOCO();
                        data.PurchaseOrderID = int.Parse(((Label)row.FindControl("PurchaseOrderID")).Text);
                        data.PartID = int.Parse(((Label)row.FindControl("PartID")).Text);
                        data.QuantityOutstanding = int.Parse(((Label)row.FindControl("QuantityOutstanding")).Text);
                            //add single item to the list
                            poDetailsList.Add(data);
                    }
                    sysmng.ForceCloser_Update(poData, poDetailsList);
                    RefreshPage();
                }, "Success","Order was closed successfully.");
                           
            }
        
    }

    private void RefreshPage()
    {
        PODetailsGV.DataSource = null;
        PODetailsGV.DataBind();
        lblPONumber.Text = "";
        lblVendorName.Text = "";
        lblVendorPhone.Text = "";
    }


    protected void Receive_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(()=>
        {
            ReceivingController sysmng = new ReceivingController();

            List<NewReceiveOrderPOCO> receiveNewOrders = new List<NewReceiveOrderPOCO>();

            foreach (GridViewRow row in PODetailsGV.Rows)
            {
                NewReceiveOrderPOCO newOrder = new NewReceiveOrderPOCO();
                newOrder.PurchaseOrderID = int.Parse(((Label)row.FindControl("PurchaseOrderID")).Text);
                newOrder.PurchaseOrderDetailID = int.Parse(((Label)row.FindControl("PurchaseOrderDetailID")).Text);
                newOrder.PartID = int.Parse(((Label)row.FindControl("PartID")).Text);
                newOrder.PartDescription = (((Label)row.FindControl("Description")).Text);
                newOrder.Outstanding = int.Parse(((Label)row.FindControl("QuantityOutstanding")).Text);
                newOrder.QuantityReceived = (((TextBox)row.FindControl("txtReceiving")).Text) == "" ? 0 : int.Parse(((TextBox)row.FindControl("txtReceiving")).Text);
                newOrder.QuantityReturned = (((TextBox)row.FindControl("txtReturning")).Text) == "" ? 0 : int.Parse(((TextBox)row.FindControl("txtReturning")).Text);
                newOrder.Notes = (((TextBox)row.FindControl("txtReason")).Text);

                receiveNewOrders.Add(newOrder);
            }
            sysmng.Add_ReceivedOrders(receiveNewOrders);
            PODetailsGV.DataBind();
        }, "Confirmation", "Order was successfully received.");
    }
}