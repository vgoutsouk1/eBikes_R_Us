<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .displayemployeename{
            float: right;
            margin-top: 20px;
            font-size: 18px;
        }
    </style>

        <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="displayemployeename"></asp:Label>

        
        <h1>Team JVMI </h1>
        <img src="img/teamlogo.png" alt="team logo" height="200" width="200">
        
    

    <div>

        
        <h2>Team member names and responsibilities</h2>
        <hr />
        <ul>
            <li>Mike Bateman - Sales </li>
            <li>Igor Marchenko - Receiving </li>
            <li>Jamie Stewart - Purchasing </li>
            <li>Vadim Goutsouk - Jobing</li>
            
            
            
        </ul>
            
        
        <h2><u>Shared components distribution</u></h2>
        <p>
            Overall structure - Mike, Vadim.<br />
            Entity framework - Jamie, Igor, Vadim.<br />
            Shared Components - </p><ul>
                                    <li>Pages: Igor, Vadim</li>
                                    <li>MessageUserControl: Jamie</li>
                                    <li>Group logo: Mike</li>
                                </ul>
                  
        

        <h2>List of known bugs</h2>
        <hr>
    </div>

  
</asp:Content>


