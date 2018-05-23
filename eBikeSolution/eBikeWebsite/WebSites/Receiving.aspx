<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Receiving.aspx.cs" Inherits="WebSites_Receiving" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
        .displayemployeename{
            float: right;
            margin-top: 20px;
            font-size: 18px;
        }
        td{
            padding: 10px;            
        }
        th{
            text-align: center;          
        }
    </style>
     
    <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="displayemployeename"></asp:Label>
     <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Receiving</h1>
        </div>
    </div>
   
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="OutstandingOrdersGV" CssClass="table table-bordered table-striped table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="OutstandingOrdersODS">
                <Columns>
                    <asp:TemplateField HeaderText="PurchaseOrderId" SortExpression="PurchaseOrderId" Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("PurchaseOrderId") %>' ID="purchaseOrderID"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order" SortExpression="PurchaseOrderNumber">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("PurchaseOrderNumber") %>' ID="Label4"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Date" SortExpression="OrderDate">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("OrderDate", "{0:MMM/dd/yyyy}") %>' ID="Label3"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendor" SortExpression="VendorName">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Vendor") %>' ID="Label2"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact" SortExpression="VendorPhone">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Phone") %>' ID="Label5"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="View Order" CommandName="" OnClick="ViewOrder_Click" CausesValidation="false" ID="viewOrder"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
       
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs">
                <li><a href="#receiving" data-toggle="tab">Receiving</a></li>
                <li><a href="#unorderedParts" data-toggle="tab">Unordered Parts</a></li>
            </ul>
        
            <div class="tab-content">
                <div class="tab-pane fade" id="receiving" role="tabpanel">
                    <asp:UpdatePanel ID="UpdatePanelReceiving" runat="server">                        
                      <ContentTemplate>
                          <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
                          <asp:Label ID="Label6" runat="server" Text="Label"><b>PO: </b></asp:Label>
                          <asp:Label ID="lblPONumber" runat="server"></asp:Label>
                          <asp:Label ID="Label7" runat="server" Text="Label"><b>Vendor: </b></asp:Label>    
                          <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                          <asp:Label ID="Label8" runat="server" Text="Label"><b>Contact: </b></asp:Label>
                          <asp:Label ID="lblVendorPhone" runat="server"></asp:Label>
                                         
                          <asp:GridView ID="PODetailsGV" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False">
                              <Columns>
                                  <asp:TemplateField HeaderText="PurchaseOrderID" SortExpression="PurchaseOrderID" Visible="False">                                  
                                      <ItemTemplate>
                                          <asp:Label runat="server" Text='<%# Bind("PurchaseOrderID") %>' ID="PurchaseOrderID"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <%-- will need these for update of the Receive order details table--%>
                                  <asp:TemplateField HeaderText="PurchaseOrderDetailID" SortExpression="PurchaseOrderDetailID" Visible="False">                                 
                                      <ItemTemplate>
                                          <asp:Label runat="server" Text='<%# Bind("PurchaseOrderDetailID") %>' ID="PurchaseOrderDetailID"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Part#" SortExpression="PartID">                                     
                                      <ItemTemplate>
                                          <asp:Label runat="server" Text='<%# Bind("PartID") %>' ID="PartID"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Description" SortExpression="Description">                                 
                                      <ItemTemplate>
                                          <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Description"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Ordered" SortExpression="QuantityOnOrder">                                  
                                      <ItemTemplate>
                                          <asp:Label runat="server" Text='<%# Bind("QuantityOnOrder") %>' ID="Label4"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Outstanding" SortExpression="QuantityOutstanding">                                   
                                      <ItemTemplate>
                                          <asp:Label runat="server" Text='<%# Bind("QuantityOutstanding") %>' ID="QuantityOutstanding"></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Receiving" SortExpression="Receiving">                                   
                                      <ItemTemplate>
                                          <asp:TextBox ID="txtReceiving" runat="server" Width="30px" ></asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Returning" SortExpression="Returning">                                   
                                      <ItemTemplate>
                                          <asp:TextBox ID="txtReturning" runat="server" Width="30px"></asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Reason" SortExpression="Reason">                                   
                                      <ItemTemplate>
                                         <asp:TextBox ID="txtReason" runat="server"></asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                          <asp:Button ID="btnReceive" runat="server" Text="Receive" CssClass="btn btn-success" OnClick="Receive_Click" />
                          <asp:Button ID="btnForceCloser" runat="server" Text="Force Closer" CssClass="btn btn-danger" OnClick="ForceCloser_Click" />
                          <asp:Label ID="lblReasonFC" runat="server" Text="Label"><b>Reason: </b></asp:Label>
                          <asp:TextBox ID="txtReasonFC" runat="server"></asp:TextBox>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                </div> <%--end of pane--%> 
                
                <div class="tab-pane fade" id="unorderedParts">
                    <asp:UpdatePanel ID="UpdatePanelUnorderedParts" runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="UnorderedVendorPartsList" runat="server" DataSourceID="UnorderedVendorePartsODS" InsertItemPosition="FirstItem" DataKeyNames="CartID">
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #FFFFFF; color: #284775;">
                                        <td>
                                            <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" CssClass="btn btn-danger" /></td>
                                       <%-- <td>
                                            <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" Visible="False"/></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("PurchaseOrderNumber") %>' runat="server" ID="PurchaseOrderNumberLabel" Visible="False" /></td>--%>
                                        <td>
                                            <asp:Label Text='<%# Eval("VendorPartNumber") %>' runat="server" ID="VendorPartNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        
                                        <td>
                                            <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                    </tr>
                                </AlternatingItemTemplate>                          
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
                                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" CssClass="btn btn-primary" /></td>                                                                               
                                        <%--<td>
                                            <asp:TextBox Text='<%# Bind("CartID") %>' runat="server" ID="CartIDTextBox" Visibile="False"/></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("PurchaseOrderNumber") %>' runat="server" ID="PurchaseOrderNumberTextBox" Visibile="False" /></td>--%>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("VendorPartNumber") %>' runat="server" ID="txtVendorPartNumber" /></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="txtDescription" /></td>
                                        
                                        <td>
                                            <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="txtQuantity" /></td>
                                    </tr>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #E0FFFF; color: #333333;">
                                       <td>
                                            <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" CssClass="btn btn-danger" CommandArgument='<%# Bind("CartID") %>'/></td>
                                        <%--<td>
                                            <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" Visibile="False"/></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("PurchaseOrderNumber") %>' runat="server" ID="PurchaseOrderNumberLabel" Visibile="False"/></td>--%>
                                        <td>
                                            <asp:Label Text='<%# Eval("VendorPartNumber") %>' runat="server" ID="VendorPartNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        

                                        <td>
                                            <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                        <%--<th runat="server"></th>--%>
                                                        <th runat="server"></th>
                                                        <th runat="server">Vendore Part#</th>
                                                        <th runat="server">Description</th>
                                                        <th runat="server">Qty</th>
                                                    </tr>
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>                         
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div> <%--end of pane--%>          

            </div> <%--end of tab-content--%>
        </div> <%--end of col-md-12--%>
    </div> <%--end of row--%>


    <%--ODS for displaying the Outstanding orders--%>
    <asp:ObjectDataSource ID="OutstandingOrdersODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetOutstandingPO" 
        TypeName="eBikeSystem.BLL.ReceivingController">
    </asp:ObjectDataSource>
     <%--ODS for displaying the Unordered Vendor parts--%>
    <asp:ObjectDataSource ID="UnorderedVendorePartsODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUnorderedVendorParts"
        TypeName="eBikeSystem.BLL.ReceivingController"
        DataObjectTypeName="eBike.Data.Entities.UnorderedPurchaseItemCart"
        InsertMethod="Add_UnorderedVendorPart" 
        DeleteMethod="Remove_UnorderedVendorPart">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblPONumber" PropertyName="Text" DefaultValue="0" Name="poNumber" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
