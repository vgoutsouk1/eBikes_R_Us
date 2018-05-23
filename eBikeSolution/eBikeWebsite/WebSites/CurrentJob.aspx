<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CurrentJob.aspx.cs" Inherits="WebSites_CurrentJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="jumbotron">
        <h1>Current Job</h1>
    </div>
    <div class="row col-md-12">
         <asp:DropDownList ID="CustomerDDL" 
             runat="server" DataSourceID="customerDDLODS"
              DataTextField="Name" DataValueField="CustomerID"
              OnSelectedIndexChanged="CustomerDDL_SelectedIndexChanged" 
             AutoPostBack="True" AppendDataBoundItems="true">
              <asp:ListItem Value="0" Enabled="true" Selected="True">Select...</asp:ListItem>

            </asp:DropDownList> 
        <div class="form-group" style="padding-bottom: 2rem; padding-top: 2rem;">

            <asp:Label CssClass="col-xs-1" runat="server" Text="User: " AssociatedControlID="UserFullName"></asp:Label>

            <asp:Label CssClass="col-sm-1" ID="UserFullName" runat="server"></asp:Label>
           

            <asp:Label CssClass="col-sm-1" runat="server" Text="Job: " AssociatedControlID="JobID"></asp:Label>

            <asp:Label CssClass="col-sm-1" ID="JobID" runat="server"></asp:Label>

            <asp:Label CssClass="col-sm-1" runat="server" Text="Customer: " AssociatedControlID="CustomerName"></asp:Label>

            <asp:Label CssClass="col-sm-3" ID="CustomerName" runat="server"></asp:Label>

            <asp:Label CssClass="col-sm-1" runat="server" Text="Contact: " AssociatedControlID="ContactNumber"></asp:Label>

            <asp:Label CssClass="col-sm-2" ID="ContactNumber" runat="server"></asp:Label>

           

        </div>
          
    </div>
    <div class="row col-md-12">
        <div class="form-group" style="padding-bottom: 2rem; padding-top: 2rem;">
           
             <asp:Label CssClass="col-sm-1" runat="server" Text="Presets: " AssociatedControlID="PresetDDL"></asp:Label>
            <asp:DropDownList CssClass="col-sm-2" ID="PresetDDL" AppendDataBoundItems="true"
                runat="server" DataSourceID="PresetDDLODS" DataTextField="Description"
                DataValueField="StandardJobID" OnSelectedIndexChanged="PresetDDL_SelectedIndexChanged">
                <asp:ListItem Value="0" Enabled="true" Selected="True">Select...</asp:ListItem>
            </asp:DropDownList>
            <asp:Button CssClass="col-sm-1" ID="PresetButton" runat="server" Text="Select" OnClick="PresetButton_Click"  />

            <asp:Label CssClass="col-sm-1" runat="server" Text="Coupon: " AssociatedControlID="CouponDDL"></asp:Label>
            <asp:DropDownList CssClass="col-sm-2" ID="CouponDDL" AppendDataBoundItems="true" runat="server" DataSourceID="CouponDDLODS" 
                DataTextField="CouponIDValue" DataValueField="CouponID">
                <asp:ListItem Value="0" Enabled="true" Selected="True">Select...</asp:ListItem>
            </asp:DropDownList>
            <asp:Button CssClass="col-sm-1" ID="AddServiceButton" runat="server" Text="Add Service" OnClick="AddServiceButton_Click" />

        </div>
    </div>
    <%-- OnDataBinding="PresetButton_Click" --%>
    <div class="row col-md-12">
        <div class="form-group" style="padding-bottom: 2rem; padding-top: 2rem;">
            <asp:Label CssClass="col-sm-1" AssociatedControlID="Description" runat="server" Text="Description"></asp:Label>
            <asp:TextBox CssClass="col-sm-1" ID="Description" runat="server" Text="" Width="250px" ></asp:TextBox>
            <asp:Label CssClass="col-sm-1" AssociatedControlID="Hours" runat="server" Text="Hours: "></asp:Label>
            <asp:TextBox CssClass="col-sm-1" ID="Hours" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row col-md-12">
        <div class="form-group" style="padding-bottom: 2rem; padding-top: 2rem;">
            <asp:Label CssClass="col-sm-1" AssociatedControlID="Comments" runat="server" Text="Comments: "></asp:Label>
            <asp:TextBox CssClass="col-sm-1" ID="Comments" runat="server" Height="100" Width="800px"></asp:TextBox>
        </div>
    </div>

    <div class="row col-md-12">
        <div class="form-group" style="padding-bottom: 2rem; padding-top: 2rem;">
            <asp:Button ID="Manage" runat="server" Text="Manage Services" OnClick="Manage_Click" />
        </div>
    </div>

    <div class="row col-md-12">
        <asp:GridView ID="JobServiceGridView" runat="server" AllowPaging="True"
            CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False"
             > 
            <Columns>


                 <asp:TemplateField Visible="True">
                    <ItemTemplate>
                        <asp:LinkButton ID="RemoveLinkButton" runat="server">Remove</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="JobDetailID" Visible="true" >
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobDetailID") %>' ID="JobDetailIDLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="JobDetailID" HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="DescriptionLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="JobDetailID" HeaderText="Hours">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobHours") %>' ID="JobHoursLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="JobDetailID" HeaderText="Coupon">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("CouponID") %>' ID="CouponIDLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="JobDetailID" HeaderText="Comments">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Comments") %>' ID="CommentsLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              



            </Columns>  
        </asp:GridView>
        </div>
        
    
     <div class="row col-md-12">
         <asp:GridView ID="ManageServicesGridView" runat="server" AllowPaging="True"
             CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False"
            >
            <Columns>


                <asp:TemplateField SortExpression="JobDetailID" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobDetailID") %>' ID="JobDetailIDLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="JobDetailID" HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="DescriptionLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField SortExpression="JobDetailID" HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("StatusCode") %>' ID="StatusLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField Visible="True">
                    <ItemTemplate>
                        <asp:LinkButton ID="SelectLinkButton" runat="server">Select</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField Visible="True">
                    <ItemTemplate>
                        <asp:LinkButton ID="DoneLinkButton" runat="server">Done</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="RemoveLinkButton" runat="server">Remove</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="ViewPartsLinkButton" runat="server" OnClick="ViewPartsLinkButton_Click">View Parts</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>


        </asp:GridView>
     </div>

    <div>
        <%--<asp:ListView ID="ServicePartsListView" runat="server" DataSourceID="ServicePartsListViewODS">


            <AlternatingItemTemplate>
                <tr style="background-color: #FFF8DC;">
                    <td>
                        <asp:Label Text='<%# Eval("PartID") %>' runat="server" ID="PartIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchasePrice") %>' runat="server" ID="PurchasePriceLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SellingPrice") %>' runat="server" ID="SellingPriceLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("QuantityOnHand") %>' runat="server" ID="QuantityOnHandLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReorderLevel") %>' runat="server" ID="ReorderLevelLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("QuantityOnOrder") %>' runat="server" ID="QuantityOnOrderLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("CategoryID") %>' runat="server" ID="CategoryIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Refundable") %>' runat="server" ID="RefundableLabel" /></td>
                    <td>
                        <asp:CheckBox Checked='<%# Eval("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" Enabled="false" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorID") %>' runat="server" ID="VendorIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Category") %>' runat="server" ID="CategoryLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("JobDetailParts") %>' runat="server" ID="JobDetailPartsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Vendor") %>' runat="server" ID="VendorLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchaseOrderDetails") %>' runat="server" ID="PurchaseOrderDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SaleDetails") %>' runat="server" ID="SaleDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SaleRefundDetails") %>' runat="server" ID="SaleRefundDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ShoppingCartItems") %>' runat="server" ID="ShoppingCartItemsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("StandardJobParts") %>' runat="server" ID="StandardJobPartsLabel" /></td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #008A8C; color: #FFFFFF;">
                    <td>
                        <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PartID") %>' runat="server" ID="PartIDTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PurchasePrice") %>' runat="server" ID="PurchasePriceTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SellingPrice") %>' runat="server" ID="SellingPriceTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("QuantityOnHand") %>' runat="server" ID="QuantityOnHandTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ReorderLevel") %>' runat="server" ID="ReorderLevelTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("QuantityOnOrder") %>' runat="server" ID="QuantityOnOrderTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("CategoryID") %>' runat="server" ID="CategoryIDTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Refundable") %>' runat="server" ID="RefundableTextBox" /></td>
                    <td>
                        <asp:CheckBox Checked='<%# Bind("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("VendorID") %>' runat="server" ID="VendorIDTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Category") %>' runat="server" ID="CategoryTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("JobDetailParts") %>' runat="server" ID="JobDetailPartsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Vendor") %>' runat="server" ID="VendorTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PurchaseOrderDetails") %>' runat="server" ID="PurchaseOrderDetailsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SaleDetails") %>' runat="server" ID="SaleDetailsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SaleRefundDetails") %>' runat="server" ID="SaleRefundDetailsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ShoppingCartItems") %>' runat="server" ID="ShoppingCartItemsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("StandardJobParts") %>' runat="server" ID="StandardJobPartsTextBox" /></td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PartID") %>' runat="server" ID="PartIDTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PurchasePrice") %>' runat="server" ID="PurchasePriceTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SellingPrice") %>' runat="server" ID="SellingPriceTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("QuantityOnHand") %>' runat="server" ID="QuantityOnHandTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ReorderLevel") %>' runat="server" ID="ReorderLevelTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("QuantityOnOrder") %>' runat="server" ID="QuantityOnOrderTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("CategoryID") %>' runat="server" ID="CategoryIDTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Refundable") %>' runat="server" ID="RefundableTextBox" /></td>
                    <td>
                        <asp:CheckBox Checked='<%# Bind("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("VendorID") %>' runat="server" ID="VendorIDTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Category") %>' runat="server" ID="CategoryTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("JobDetailParts") %>' runat="server" ID="JobDetailPartsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Vendor") %>' runat="server" ID="VendorTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PurchaseOrderDetails") %>' runat="server" ID="PurchaseOrderDetailsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SaleDetails") %>' runat="server" ID="SaleDetailsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("SaleRefundDetails") %>' runat="server" ID="SaleRefundDetailsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("ShoppingCartItems") %>' runat="server" ID="ShoppingCartItemsTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("StandardJobParts") %>' runat="server" ID="StandardJobPartsTextBox" /></td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="background-color: #DCDCDC; color: #000000;">
                    <td>
                        <asp:Label Text='<%# Eval("PartID") %>' runat="server" ID="PartIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchasePrice") %>' runat="server" ID="PurchasePriceLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SellingPrice") %>' runat="server" ID="SellingPriceLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("QuantityOnHand") %>' runat="server" ID="QuantityOnHandLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReorderLevel") %>' runat="server" ID="ReorderLevelLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("QuantityOnOrder") %>' runat="server" ID="QuantityOnOrderLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("CategoryID") %>' runat="server" ID="CategoryIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Refundable") %>' runat="server" ID="RefundableLabel" /></td>
                    <td>
                        <asp:CheckBox Checked='<%# Eval("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" Enabled="false" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorID") %>' runat="server" ID="VendorIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Category") %>' runat="server" ID="CategoryLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("JobDetailParts") %>' runat="server" ID="JobDetailPartsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Vendor") %>' runat="server" ID="VendorLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchaseOrderDetails") %>' runat="server" ID="PurchaseOrderDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SaleDetails") %>' runat="server" ID="SaleDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SaleRefundDetails") %>' runat="server" ID="SaleRefundDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ShoppingCartItems") %>' runat="server" ID="ShoppingCartItemsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("StandardJobParts") %>' runat="server" ID="StandardJobPartsLabel" /></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                    <th runat="server">PartID</th>
                                    <th runat="server">Description</th>
                                    <th runat="server">PurchasePrice</th>
                                    <th runat="server">SellingPrice</th>
                                    <th runat="server">QuantityOnHand</th>
                                    <th runat="server">ReorderLevel</th>
                                    <th runat="server">QuantityOnOrder</th>
                                    <th runat="server">CategoryID</th>
                                    <th runat="server">Refundable</th>
                                    <th runat="server">Discontinued</th>
                                    <th runat="server">VendorID</th>
                                    <th runat="server">Category</th>
                                    <th runat="server">JobDetailParts</th>
                                    <th runat="server">Vendor</th>
                                    <th runat="server">PurchaseOrderDetails</th>
                                    <th runat="server">SaleDetails</th>
                                    <th runat="server">SaleRefundDetails</th>
                                    <th runat="server">ShoppingCartItems</th>
                                    <th runat="server">StandardJobParts</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                            <asp:DataPager runat="server" ID="DataPager1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    <asp:NumericPagerField></asp:NumericPagerField>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                    <td>
                        <asp:Label Text='<%# Eval("PartID") %>' runat="server" ID="PartIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchasePrice") %>' runat="server" ID="PurchasePriceLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SellingPrice") %>' runat="server" ID="SellingPriceLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("QuantityOnHand") %>' runat="server" ID="QuantityOnHandLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ReorderLevel") %>' runat="server" ID="ReorderLevelLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("QuantityOnOrder") %>' runat="server" ID="QuantityOnOrderLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("CategoryID") %>' runat="server" ID="CategoryIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Refundable") %>' runat="server" ID="RefundableLabel" /></td>
                    <td>
                        <asp:CheckBox Checked='<%# Eval("Discontinued") %>' runat="server" ID="DiscontinuedCheckBox" Enabled="false" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorID") %>' runat="server" ID="VendorIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Category") %>' runat="server" ID="CategoryLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("JobDetailParts") %>' runat="server" ID="JobDetailPartsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Vendor") %>' runat="server" ID="VendorLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchaseOrderDetails") %>' runat="server" ID="PurchaseOrderDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SaleDetails") %>' runat="server" ID="SaleDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("SaleRefundDetails") %>' runat="server" ID="SaleRefundDetailsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("ShoppingCartItems") %>' runat="server" ID="ShoppingCartItemsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("StandardJobParts") %>' runat="server" ID="StandardJobPartsLabel" /></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>--%> 

    </div>

    <div>
        <asp:GridView ID="ServicePartsGridView" runat="server"
            CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False">
            <Columns>

                   <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="EditLinkButton" runat="server">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField SortExpression="JobDetailID" HeaderText="Part ID">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("PartID") %>' ID="PartIDLabel"></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField SortExpression="JobDetailID" HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="DescriptionLabel"></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>


                 <asp:TemplateField SortExpression="JobDetailID" HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Quantity") %>' ID="QuantityLabel"></asp:Label>
                    </ItemTemplate>
                 </asp:TemplateField>

                   <asp:TemplateField >
                    <ItemTemplate>
                        <asp:LinkButton ID="RemoveLinkButton" runat="server">Remove</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
              

        </asp:GridView>
    </div>

    <%-- ods area below --%>
    <asp:ObjectDataSource ID="CurrentJobGridViewODS" runat="server"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="PresetDDLODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="presets" TypeName="eBikeSystem.BLL.JobController"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CouponDDLODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="coupon" TypeName="eBikeSystem.BLL.JobController"></asp:ObjectDataSource>

      <asp:ObjectDataSource ID="customerDDLODS" runat="server" 
       OldValuesParameterFormatString="original_{0}" 
       SelectMethod="customerList" 
       TypeName="eBikeSystem.BLL.JobController">
     </asp:ObjectDataSource>
</asp:Content>

