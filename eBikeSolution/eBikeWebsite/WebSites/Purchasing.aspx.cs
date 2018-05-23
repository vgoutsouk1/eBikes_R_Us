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

public partial class WebSites_Purchasing : System.Web.UI.Page
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
                if (!User.IsInRole(SecurityRoles.Purchasing) && !User.IsInRole(SecurityRoles.WebsiteAdmins))
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }

            // this is to show the currently logged in users name on the page
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

    public void hideAll()
    {
        CurrentPOListView.Visible = false;
        CurrentInventoryListView.Visible = false;
        VendorDetailGridView.Visible = false;
        CurrentPOLabel.Visible = false;
        CurrentInventoryLabel.Visible = false;
        UpdateButton.Visible = false;
        PlaceButton.Visible = false;
        DeleteButton.Visible = false;
        ClearButton.Visible = false;
        TotalsGridView.Visible = false;
    }
    public void showAll()
    {
        CurrentPOListView.Visible = true;
        CurrentInventoryListView.Visible = true;
        VendorDetailGridView.Visible = true;
        CurrentPOLabel.Visible = true;
        CurrentInventoryLabel.Visible = true;
        UpdateButton.Visible = true;
        PlaceButton.Visible = true;
        DeleteButton.Visible = true;
        ClearButton.Visible = true;
        TotalsGridView.Visible = true;
    }

    protected void GetCreatePO_Click(object sender, EventArgs e)
    {
        if (VendorDDL.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("You must first select a vendor from the list.");
        }
        else
        {
            // only show the GridViews, Listviews and buttons on click
            showAll();

            // set the current active order ODS to the listview
            CurrentPOListView.DataSourceID = "CurrentPOODS";
            CurrentPOListView.DataBind();

            if (CurrentPOListView.Items.Count == 0) // then create the suggested putchase order
            {
                // set the suggested order ODS to the listview and bind it
                CurrentPOListView.DataSourceID = "SuggestedPOODS";
                CurrentPOListView.DataBind();

                PurchaseOrder purchaseorder = new PurchaseOrder();

                // get the vendorID from the DDL and set it into purchaseorder
                purchaseorder.VendorID = int.Parse(VendorDDL.SelectedValue);

                var usersysmgr = new UserManager();

                int employeeId = usersysmgr.Get_EmployeeId(User.Identity.Name);

                // create new list to store the purchase order details
                List<PurchaseOrderDetail> purchaseorderdetails = new List<PurchaseOrderDetail>();

                // go through the needed rows of the listview to get the data needed for suggested order and add it to the purchaseorderdetails list
                foreach (ListViewItem item in CurrentPOListView.Items)
                {
                    PurchaseOrderDetail purchaseorderdetail = new PurchaseOrderDetail();

                    purchaseorderdetail.PartID = int.Parse((item.FindControl("PartIDLabel2") as Label).Text.ToString());
                    purchaseorderdetail.Quantity = int.Parse((item.FindControl("QuantityTextBox2") as TextBox).Text.ToString());
                    purchaseorderdetail.PurchasePrice = decimal.Parse((item.FindControl("PurchasePriceTextBox2") as TextBox).Text.ToString());

                    purchaseorderdetails.Add(purchaseorderdetail);
                }


                // pass the data to the controller to create the suggested order
                var sysmgr = new PurchasingController();
                MessageUserControl.TryRun(() =>
                {
                    sysmgr.NewSuggestedOrder(purchaseorder, purchaseorderdetails, employeeId);

                }, "Success", "Suggested purchase order created.");

                TotalsGridView.DataBind();

                PurchaseOrder purchaseordertotals = new PurchaseOrder();

                // pass the subtotal, taxamount to purchaseorder here
                purchaseordertotals.SubTotal = Math.Round(decimal.Parse(TotalsGridView.DataKeys[0].Values[0].ToString()),2);
                purchaseordertotals.TaxAmount = Math.Round(decimal.Parse(TotalsGridView.DataKeys[0].Values[1].ToString()),2);

                // pass the totals to the controller to update the suggested order
                var newsysmgr = new PurchasingController();
                MessageUserControl.TryRun(() =>
                {
                    newsysmgr.UpdateTotalsSuggestedOrder(purchaseorder, purchaseordertotals);

                }, "Success", "Suggested purchase order totals updated.");

                // Bind the new current inventory for the suggested order
                CurrentInventoryListView.DataSourceID = "CurrentInventoryODS";
                CurrentInventoryListView.DataBind();

                // this is for if the vendor has no parts available to be automatically put on the suggested purchase order
                if (CurrentPOListView.Items.Count == 0)
                {
                    MessageUserControl.ShowInfo("Information", "No active purchase order found. A suggested purchase order has been created, however, no parts from this vendor are eligible for the suggested purchase order.");
                }
                else // else, the suggested parts will be shown on the suggested order
                {
                    MessageUserControl.ShowInfo("Information", "No active purchase order found. A suggested purchase order has been created.");
                }
            }
            else
            {
                // Bind the new current inventory for the current active order
                CurrentInventoryListView.DataSourceID = "CurrentInventoryODS";
                CurrentInventoryListView.DataBind();

                MessageUserControl.ShowInfo("Information", "Current active order found.");
            }
        }
    }

    // Remove item
    protected void CurrentPOListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ListViewDataItem row = e.Item as ListViewDataItem;
        PurchaseOrderPartsPOCO removeItem = new PurchaseOrderPartsPOCO();
        removeItem.PartID = int.Parse((row.FindControl("PartIDLabel2") as Label).Text);
        removeItem.Description = (row.FindControl("DescriptionLabel2") as Label).Text;
        removeItem.QuantityOnHand = int.Parse((row.FindControl("QuantityOnHandLabel2") as Label).Text);
        removeItem.QuantityOnOrder = int.Parse((row.FindControl("QuantityOnOrderLabel2") as Label).Text);
        removeItem.ReorderLevel = int.Parse((row.FindControl("ReorderLevelLabel2") as Label).Text);
        removeItem.PurchasePrice = decimal.Parse((row.FindControl("PurchasePriceTextBox2") as TextBox).Text);
        removeItem.Quantity = 0;

        List<PurchaseOrderPartsPOCO> currentInventoryList = new List<PurchaseOrderPartsPOCO>();
        foreach (ListViewItem item in CurrentInventoryListView.Items)
        {
            PurchaseOrderPartsPOCO inventoryItem = new PurchaseOrderPartsPOCO();
            inventoryItem.PartID = int.Parse((item.FindControl("PartIDLabel3") as Label).Text.ToString());
            inventoryItem.Description = (item.FindControl("DescriptionLabel3") as Label).Text;
            inventoryItem.QuantityOnHand = int.Parse((item.FindControl("QuantityOnHandLabel3") as Label).Text.ToString());
            inventoryItem.QuantityOnOrder = int.Parse((item.FindControl("QuantityOnOrderLabel3") as Label).Text.ToString());
            inventoryItem.ReorderLevel = int.Parse((item.FindControl("ReorderLevelLabel3") as Label).Text.ToString());
            inventoryItem.PurchasePrice = decimal.Parse((item.FindControl("PurchasePriceLabel3") as Label).Text.ToString());
            inventoryItem.Quantity = 0;
            currentInventoryList.Add(inventoryItem);
        }

        List<PurchaseOrderPartsPOCO> currentOrderList = new List<PurchaseOrderPartsPOCO>();
        foreach (ListViewItem item in CurrentPOListView.Items)
        {
            PurchaseOrderPartsPOCO orderItem = new PurchaseOrderPartsPOCO();
            orderItem.PartID = int.Parse((item.FindControl("PartIDLabel2") as Label).Text.ToString());
            orderItem.Description = (item.FindControl("DescriptionLabel2") as Label).Text.ToString();
            orderItem.QuantityOnHand = int.Parse((item.FindControl("QuantityOnHandLabel2") as Label).Text.ToString());
            orderItem.QuantityOnOrder = int.Parse((item.FindControl("QuantityOnOrderLabel2") as Label).Text.ToString());
            orderItem.ReorderLevel = int.Parse((item.FindControl("ReorderLevelLabel2") as Label).Text.ToString());
            orderItem.PurchasePrice = decimal.Parse((item.FindControl("PurchasePriceTextBox2") as TextBox).Text.ToString());
            orderItem.Quantity = int.Parse((item.FindControl("QuantityTextBox2") as TextBox).Text);
            currentOrderList.Add(orderItem);
        }

        int index = row.DataItemIndex;

        List<PurchaseOrderPartsPOCO> dataRemoved = currentOrderList;
        List<PurchaseOrderPartsPOCO> dataAdded = currentInventoryList;

        dataRemoved.RemoveAt(index);
        CurrentPOListView.DataSource = dataRemoved;
        CurrentPOListView.DataSourceID = String.Empty;
        CurrentPOListView.DataBind();

        dataAdded.Add(removeItem);
        CurrentInventoryListView.DataSource = dataAdded;
        CurrentInventoryListView.DataSourceID = String.Empty;
        CurrentInventoryListView.DataBind();
    }

    // Add item
    protected void CurrentInventoryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ListViewDataItem row = e.Item as ListViewDataItem;
        PurchaseOrderPartsPOCO addItem = new PurchaseOrderPartsPOCO();
        addItem.PartID = int.Parse((row.FindControl("PartIDLabel3") as Label).Text);
        addItem.Description = (row.FindControl("DescriptionLabel3") as Label).Text;
        addItem.QuantityOnHand = int.Parse((row.FindControl("QuantityOnHandLabel3") as Label).Text);
        addItem.QuantityOnOrder = int.Parse((row.FindControl("QuantityOnOrderLabel3") as Label).Text);
        addItem.ReorderLevel = int.Parse((row.FindControl("ReorderLevelLabel3") as Label).Text);
        addItem.PurchasePrice = decimal.Parse((row.FindControl("PurchasePriceLabel3") as Label).Text);
        addItem.Quantity = 0;

        List<PurchaseOrderPartsPOCO> currentInventoryList = new List<PurchaseOrderPartsPOCO>();
        foreach (ListViewItem item in CurrentInventoryListView.Items)
        {
            PurchaseOrderPartsPOCO inventoryItem = new PurchaseOrderPartsPOCO();
            inventoryItem.PartID = int.Parse((item.FindControl("PartIDLabel3") as Label).Text.ToString());
            inventoryItem.Description = (item.FindControl("DescriptionLabel3") as Label).Text;
            inventoryItem.QuantityOnHand = int.Parse((item.FindControl("QuantityOnHandLabel3") as Label).Text.ToString());
            inventoryItem.QuantityOnOrder = int.Parse((item.FindControl("QuantityOnOrderLabel3") as Label).Text.ToString());
            inventoryItem.ReorderLevel = int.Parse((item.FindControl("ReorderLevelLabel3") as Label).Text.ToString());
            inventoryItem.PurchasePrice = decimal.Parse((item.FindControl("PurchasePriceLabel3") as Label).Text.ToString());
            inventoryItem.Quantity = 0;
            currentInventoryList.Add(inventoryItem);
        }

        List<PurchaseOrderPartsPOCO> currentOrderList = new List<PurchaseOrderPartsPOCO>();
        foreach (ListViewItem item in CurrentPOListView.Items)
        {
            PurchaseOrderPartsPOCO orderItem = new PurchaseOrderPartsPOCO();
            orderItem.PartID = int.Parse((item.FindControl("PartIDLabel2") as Label).Text.ToString());
            orderItem.Description = (item.FindControl("DescriptionLabel2") as Label).Text.ToString();
            orderItem.QuantityOnHand = int.Parse((item.FindControl("QuantityOnHandLabel2") as Label).Text.ToString());
            orderItem.QuantityOnOrder = int.Parse((item.FindControl("QuantityOnOrderLabel2") as Label).Text.ToString());
            orderItem.ReorderLevel = int.Parse((item.FindControl("ReorderLevelLabel2") as Label).Text.ToString());
            orderItem.PurchasePrice = decimal.Parse((item.FindControl("PurchasePriceTextBox2") as TextBox).Text.ToString());
            orderItem.Quantity = int.Parse((item.FindControl("QuantityTextBox2") as TextBox).Text);
            currentOrderList.Add(orderItem);
        }

        int index = row.DataItemIndex;

        List<PurchaseOrderPartsPOCO> dataRemoved = currentInventoryList;
        List<PurchaseOrderPartsPOCO> dataAdded = currentOrderList;

        dataRemoved.RemoveAt(index);
        CurrentInventoryListView.DataSource = dataRemoved;
        CurrentInventoryListView.DataSourceID = String.Empty;
        CurrentInventoryListView.DataBind();

        dataAdded.Add(addItem);
        CurrentPOListView.DataSource = dataAdded;
        CurrentPOListView.DataSourceID = String.Empty;
        CurrentPOListView.DataBind();
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        PurchaseOrder purchaseorder = new PurchaseOrder();

        // get the vendorID from the DDL and set it into purchaseorder
        purchaseorder.VendorID = int.Parse(VendorDDL.SelectedValue);

        List<PurchaseOrderDetail> purchaseorderdetails = new List<PurchaseOrderDetail>();

        foreach (ListViewItem item in CurrentPOListView.Items)
        {

            PurchaseOrderDetail purchaseorderdetail = new PurchaseOrderDetail();

            purchaseorderdetail.PurchaseOrderDetailID = int.Parse((item.FindControl("PurchaseOrderDetailIDLabel2") as Label).Text.ToString());
            purchaseorderdetail.PartID = int.Parse((item.FindControl("PartIDLabel2") as Label).Text.ToString());
            purchaseorderdetail.Quantity = int.Parse((item.FindControl("QuantityTextBox2") as TextBox).Text.ToString());
            purchaseorderdetail.PurchasePrice = decimal.Parse((item.FindControl("PurchasePriceTextBox2") as TextBox).Text.ToString());

            purchaseorderdetails.Add(purchaseorderdetail);
        }

       var sysmgr = new PurchasingController();

        MessageUserControl.TryRun(() =>
        {
            sysmgr.Update_PurchaseOrder(purchaseorder, purchaseorderdetails);

            // rebind the current PO
            CurrentPOListView.DataSourceID = "CurrentPOODS";
            CurrentPOListView.DataBind();

            // rebind the totals for the current PO
            TotalsGridView.DataBind();

            // rebind the current inventory list
            CurrentInventoryListView.DataSourceID = "CurrentInventoryODS";
            CurrentInventoryListView.DataBind();

        }, "Success", "Purchase Order item(s) quantity and purchase price updated");

    }

    protected void PlaceButton_Click(object sender, EventArgs e)
    {
        PurchaseOrder purchaseorder = new PurchaseOrder();

        // get the vendorID from the DDL and set it into purchaseorder
        purchaseorder.VendorID = int.Parse(VendorDDL.SelectedValue);

        List<PurchaseOrderDetail> purchaseorderdetails = new List<PurchaseOrderDetail>();

        foreach (ListViewItem item in CurrentPOListView.Items)
        {
            PurchaseOrderDetail purchaseorderdetail = new PurchaseOrderDetail();

            purchaseorderdetail.PartID = int.Parse((item.FindControl("PartIDLabel2") as Label).Text.ToString());
            purchaseorderdetail.Quantity = int.Parse((item.FindControl("QuantityTextBox2") as TextBox).Text.ToString());

            purchaseorderdetails.Add(purchaseorderdetail);
        }

        var sysmgr = new PurchasingController();

        MessageUserControl.TryRun(() =>
        {
            sysmgr.Place_PurchaseOrder(purchaseorder, purchaseorderdetails);

        }, "Success", "Purchase Order has been placed.");

        hideAll();
        VendorDDL.SelectedValue = "0";
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            int purchaseOrderID = 0;

            foreach (ListViewItem item in CurrentPOListView.Items)
            {
                purchaseOrderID = int.Parse((item.FindControl("PurchaseOrderIDLabel2") as Label).Text.ToString());
            }

            PurchasingController sysmgr = new PurchasingController();
            sysmgr.PurchaseOrder_Delete(purchaseOrderID);

            hideAll();

        }, "Removed", "Purchase order has been removed.");
    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        hideAll();
        VendorDDL.SelectedValue = "0";
        MessageUserControl.ShowInfo("Information", "Webpage cleared.");
    }
}