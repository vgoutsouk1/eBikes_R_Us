<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="WebSites_Sales" %>

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
    table{
        width: 100%;
    }
</style>
    <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="displayemployeename"></asp:Label>
    <div class="row">
        <div class="col-md-12">
            <h1 class="page-header">Parts Catalog <span class="glyphicon glyphicon-wrench"></span> Available Online and In-Store</h1>
        </div>
    </div>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div class="row">
        <div class="col-md-3">
            <div class="well">
                <div class="form-group">
                <h3><asp:Label ID="CategoryListLabel" runat="server" Text="Browse by Category"></asp:Label></h3>
                    <asp:DropDownList ID="CategoryList" runat="server" DataSourceID="CategoryListODS" DataTextField="Description" DataValueField="CategoryID" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="true">
                        <asp:ListItem Text="Select a Category..." Value=0></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="well">
                <h3>Checkout</h3>
                <asp:LinkButton ID="ViewCart" runat="server"
                             CssClass="btn btn-primary" PostBackUrl="~/WebSites/Checkout/ShoppingCart.aspx" >
                            Shopping Cart <span aria-hidden="true" class="glyphicon glyphicon-shopping-cart"></span>
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-md-9">
            <div class="well">
                <div class="form-group">
                <h2><asp:Label ID="PartsListLabel" runat="server" Text="Products"></asp:Label></h2>
                    <asp:ListView ID="PartsbyCategoryList" runat="server" DataSourceID="PartsbyCategoryODS" OnItemCommand="PartsbyCategoryList_ItemCommand">
                        <AlternatingItemTemplate>
                            <tr style="background-color: #FFFFFF; color: #284775;">
                                <td style="width: 150px;">
                                    <asp:Button runat="server"
                                        ID="AddToCartBtn"
                                        CommandArgument='<%# Eval("PartID") %>'
                                        Text="Add"
                                        CssClass="btn btn-primary" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="QuantityOrdered" MaxLength="3" Width="50px" TextMode="Number" Text="1">
                                    </asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                <td style="width: 100px; text-align: right;">
                                    <asp:Label Text='<%# Eval("InStock") %>' runat="server" ID="InStockLabel" /></td>
                                <td style="width: 100px; text-align: right;">
                                    <asp:Label Text='<%# Eval("Price", "{0:C}") %>' runat="server" ID="PriceLabel" /></td>
                            </tr>
                        </AlternatingItemTemplate>
                        
                        <EmptyDataTemplate>
                            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                <tr>
                                    <td>No category selected.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                       
                        <ItemTemplate>
                            <tr style="background-color: #E0FFFF; color: #333333;">
                                <td style="width: 150px;">
                                    <asp:Button runat="server"
                                        ID="AddToCartBtn"
                                        CommandArgument='<%# Eval("PartID") %>'
                                        Text="Add"
                                        CssClass="btn btn-primary" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="QuantityOrdered" MaxLength="3" Width="50px" TextMode="Number" Text="1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                <td style="width: 100px; text-align: right;">
                                    <asp:Label Text='<%# Eval("InStock") %>' runat="server" ID="InStockLabel" /></td>
                                <td style="width: 100px; text-align: right;">
                                    <asp:Label Text='<%# Eval("Price", "{0:C}") %>' runat="server" ID="PriceLabel" /></td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                <th runat="server"></th>
                                                <th runat="server"></th>
                                                <th runat="server">Part Name</th>
                                                <th runat="server">In Stock</th>
                                                <th runat="server">Price</th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder"></tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center; background-color: #F5F5F5; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000">
                                        <asp:DataPager runat="server" ID="DataPager2">
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
                            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                                <td>
                                    <asp:Label Text='<%# Eval("PartID") %>' runat="server" ID="PartIDLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("InStock") %>' runat="server" ID="InStockLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Price") %>' runat="server" ID="PriceLabel" /></td>
                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="CategoryListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="CategoryList" TypeName="eBikeSystem.BLL.SalesController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="PartsbyCategoryODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Parts_byCategoryList" TypeName="eBikeSystem.BLL.SalesController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryList" PropertyName="SelectedValue" Name="categoryid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
