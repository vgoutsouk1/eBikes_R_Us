<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PurchaseDetails.aspx.cs" Inherits="WebSites_Checkout_PurchaseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style>
        .displayemployeename{
            float: right;
            margin-top: 20px;
            font-size: 18px;
        }
        .lead{
            float: right;
        }
        .continuebutton{
            float: right;
            margin-top: 20px;
        }
        .backbutton{
            float: left;
            margin-top: 20px;
        }
        input{
            max-width: 100%;
        }
    </style>
    <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="displayemployeename"></asp:Label>
    <div class="row">
        <div class="col-md-7">
            <div class="page-header">
                <h1>Purchase Details <span aria-hidden="true" class="glyphicon glyphicon-pencil"></span></h1>
                <p class="lead">Enter your information for billing and shipping.</p>
                <asp:LinkButton ID="BackToShopping" runat="server"
                             CssClass="btn btn-primary" PostBackUrl="~/WebSites/Sales.aspx" >
                            <span aria-hidden="true" class="glyphicon glyphicon-menu-left"></span> Back to Shopping 
                </asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-5">
            <h3>Billing Details</h3>
            <fieldset>
                <div class="form-group">             
                    <asp:Label ID="BillingNameLabel" runat="server" Text="Name:"></asp:Label>
                    <asp:TextBox ID="BillingName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">             
                    <asp:Label ID="BillingEmailLabel" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="BillingEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">             
                    <asp:Label ID="BillingAddressLabel" runat="server" Text="Address:"></asp:Label>
                    <asp:TextBox ID="BillingAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">             
                    <asp:Label ID="BillingPhoneLabel" runat="server" Text="Phone:" ></asp:Label>
                    <asp:TextBox ID="BillingPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </fieldset>
        </div>

        <div class="col-md-5">
            <h3>Shipping Details</h3>
            <fieldset>
                <div class="form-group">             
                    <asp:Label ID="ShippingNameLabel" runat="server" Text="Name:"></asp:Label>
                    <asp:TextBox ID="ShippingName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">             
                    <asp:Label ID="ShippingEmailLabel" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="ShippingEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">             
                    <asp:Label ID="ShippingAddressLabel" runat="server" Text="Address:"></asp:Label>
                    <asp:TextBox ID="ShippingAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">             
                    <asp:Label ID="ShippingPhoneLabel" runat="server" Text="Phone:" ></asp:Label>
                    <asp:TextBox ID="ShippingPhone" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="row">
        <div class="col-md-10">
            <div class="continuebutton">
                <asp:LinkButton ID="NextButton" runat="server"
                                 CssClass="btn btn-primary" PostBackUrl="~/WebSites/Checkout/PlaceOrder.aspx" >
                                Continue <span aria-hidden="true" class="glyphicon glyphicon-menu-right"></span>
                </asp:LinkButton>
            </div>
            <div class="backbutton">
                <asp:LinkButton ID="BackButton" runat="server"
                                 CssClass="btn btn-primary" PostBackUrl="~/WebSites/Checkout/ShoppingCart.aspx" >
                                <span aria-hidden="true" class="glyphicon glyphicon-menu-left"></span> Back
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

