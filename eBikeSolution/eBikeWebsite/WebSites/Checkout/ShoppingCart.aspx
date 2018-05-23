<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="WebSites_Checkout_ShoppingCart" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <style>
        .displayemployeename{
            float: right;
            margin-top: 20px;
            font-size: 18px;
        }
        table{
            width: 100%;
        }
        td{
            padding: 10px;
        }
        th{
            text-align: center;
        }
        input{
            text-align: right;
        }
        .totals{
            text-align: right;
            padding: 10px;
            font-size: 18px;
        }
        .continuebutton{
            float: right;
            margin-top: 20px;
        }
        .boalert{
            font-size: 16px;
            padding: 10px;
            display: block;
        }
    </style>
    <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="displayemployeename"></asp:Label>
    <asp:Label ID="UserLabel" runat="server" Visible="false"></asp:Label>
    <div class="row">
        <div class="col-md-10">
            <div class="page-header">
                <h1>Your Shopping Cart <span aria-hidden="true" class="glyphicon glyphicon-shopping-cart"></span></h1>
                <asp:LinkButton ID="BackToShopping" runat="server"
                             CssClass="btn btn-primary" PostBackUrl="~/WebSites/Sales.aspx" >
                            <span aria-hidden="true" class="glyphicon glyphicon-menu-left"></span> Back to Shopping 
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <asp:ListView ID="ShoppingCartList" runat="server" DataSourceID="ShoppingCartListODS" OnItemCommand="ShoppingCartList_ItemCommand">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775;">
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                        <td style="width: 100px; text-align: center;">
                            <asp:TextBox ID="QuantityTextBox" Text='<%# Eval("Quantity") %>' runat="server" Width="50px"></asp:TextBox></td>
                            
                        <td style="width: 100px; text-align: right;">
                            <asp:Label Text='<%# Eval("UnitPrice", "{0:C}") %>' runat="server" ID="UnitPriceLabel" /></td>
                        <td style="width: 100px; text-align: right;">
                            <asp:Label Text='<%# Eval("TotalPrice", "{0:C}") %>' runat="server" ID="TotalPriceLabel" /></td>
                         <td style="width: 100px; text-align: center;">
                                    <asp:Button runat="server"
                                        ID="UpdateCartBtn"
                                        CommandArgument='<%# Eval("PartID") %>'
                                        CommandName="Change"
                                        Text="Update"
                                        CssClass="btn btn-primary" />
                        </td>
                         <td style="width: 100px; text-align: center;">
                                    <asp:Button runat="server"
                                        ID="RemoveItemButton"
                                        CommandArgument='<%# Eval("PartID") %>'
                                        CommandName="Remove"
                                        Text="Remove"
                                        CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
               
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>Shopping cart is empty.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333;">
                        
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                        <td style="width: 100px; text-align: center;">
                            <asp:TextBox ID="QuantityTextBox" Text='<%# Eval("Quantity") %>' runat="server" Width="50px"></asp:TextBox></td>
                        <td style="width: 100px; text-align: right;">
                            <asp:Label Text='<%# Eval("UnitPrice", "{0:C}") %>' runat="server" ID="UnitPriceLabel" /></td>
                        <td style="width: 100px; text-align: right;">
                            <asp:Label Text='<%# Eval("TotalPrice", "{0:C}") %>' runat="server" ID="TotalPriceLabel" /></td>
                        <td style="width: 100px; text-align: center;">
                                    <asp:Button runat="server"
                                        ID="UpdateCartBtn"
                                        CommandArgument='<%# Eval("PartID") %>'
                                        CommandName="Change"
                                        Text="Update"
                                        CssClass="btn btn-primary" />
                        </td>                      
                        <td style="width: 100px; text-align: center;">
                                    <asp:Button runat="server"
                                        ID="RemoveItemButton"
                                        CommandArgument='<%# Eval("PartID") %>'
                                        CommandName="Remove"
                                        Text="Remove"
                                        CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-style: none; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr runat="server" style="background-color: #FFFFFF; color: #333333;">
                                        
                                        <th runat="server">Part Name</th>
                                        <th runat="server">Quantity</th>
                                        <th runat="server">Price(ea.)</th>
                                        <th runat="server">Total</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #FFFFFF; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                        <td>
                            <asp:Label Text='<%# Eval("PartID") %>' runat="server" ID="PartIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("TotalPrice") %>' runat="server" ID="TotalPriceLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
        </div>
    </div>
     <div class="row">
        <asp:Label runat="server" Visible="false" ID="backorderalertlabel" CssClass="alert alert-warning boalert"></asp:Label>
    </div>
    <div class="row">
        <div class="col-md-7"></div>
        <div class="col-md-3">
            <div class="totals">             
                <asp:GridView ID="TotalsGridView" runat="server" AutoGenerateColumns="False" DataSourceID="TotalsODS">
                    <Columns>
                        <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" SortExpression="SubTotal" DataFormatString="{0:C}"></asp:BoundField>
                        <asp:BoundField DataField="GST" HeaderText="GST" SortExpression="GST" DataFormatString="{0:C}"></asp:BoundField>
                        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" DataFormatString="{0:C}"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="continuebutton">
                <asp:LinkButton ID="NextButton" runat="server"
                                 CssClass="btn btn-primary" PostBackUrl="~/WebSites/Checkout/PurchaseDetails.aspx" >
                                Continue <span aria-hidden="true" class="glyphicon glyphicon-menu-right"></span>
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="ShoppingCartListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ShoppingCartList" TypeName="eBikeSystem.BLL.SalesController"  OnSelected="CheckForException">
        <SelectParameters>
            <asp:ControlParameter ControlID="UserLabel" PropertyName="Text" DefaultValue="" Name="username" Type="String"></asp:ControlParameter>

        </SelectParameters>

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TotalsODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ShoppingCart_Totals" TypeName="eBikeSystem.BLL.SalesController">
        <SelectParameters>
            <asp:ControlParameter ControlID="UserLabel" PropertyName="Text" Name="username" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

