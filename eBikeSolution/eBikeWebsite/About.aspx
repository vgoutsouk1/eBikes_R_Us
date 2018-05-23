<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1><u>Who we are</u></h1> <br />
    

    <h4><img src="img/mike.jpg" alt="mike's logo" height="100" width="80"> &nbsp;&nbsp;Mike Bateman - Sales </h4>
    <hr>

    <h4> <img src="img/igor.jpg" alt="igor's logo" height="100" width="80"> &nbsp;&nbsp;Igor Marchenko - Receiving </h4>
    <hr />

    <h4><img src="img/jamie.jpg" alt="jamie's logo" height="100" width="80"> &nbsp;&nbsp;Jamie Stewart - Purchasing</h4>
    <hr />
            
       
    <h4><img src="img/vadim.jpg" alt="vadim's logo" height="100" width="80"> &nbsp;&nbsp;Vadim Goutsouk - Jobing</h4>
      <br />
        
    <h1><u>Planned Security Roles</u></h1>
    <ul>
        <li>Website managament team - Password: $omething1</li>
        <li>Staff - Password: $omething01</li>
        <li>Regular users - Password: Up to the user</li>
    </ul>


    <h1><u>Connection String</u></h1>
    <p>
     name="eBikeDB"
     connectionString="Data Source=.;Initial Catalog=eBikes;Integrated Security=true"
     providerName="System.Data.SqlClient"
    </p>
     <h3><u>List of known bugs - Version 1.0.0 - Beta</u></h3>
     
    
</asp:Content>
