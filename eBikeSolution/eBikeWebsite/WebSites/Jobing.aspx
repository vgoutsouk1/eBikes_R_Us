<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Jobing.aspx.cs" Inherits="WebSites_Jobing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .displayemployeename {
            float: right;
            margin-top: 20px;
            font-size: 18px;
        }



    </style>
    <asp:Label ID="EmployeeNameLabel" runat="server" CssClass="displayemployeename"></asp:Label>
    <div>
        <h1>Jobing Page</h1>
    </div>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <div class="row">
         
        <div class="col-md-5">
            
                <h2><asp:Label ID="CurrentJobs" runat="server" Text="Current Jobs"></asp:Label></h2>

            <asp:GridView ID="CurrentJobsList" 
                CssClass="table table-bordered table-striped table-hover"
                 runat="server" AutoGenerateColumns="False" DataSourceID="CurrentJobsListODS">
            <Columns>
                <asp:TemplateField HeaderText="Job Id" SortExpression="JobId">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobID") %>' ID="JobIDLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Job Date In" SortExpression="JobDateIn">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobDateIn", "{0:MMM/dd/yyyy}") %>' ID="JobDateInLabel" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date" SortExpression="JobDateStarted">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobDateStarted", "{0:MMM/dd/yyyy}") %>' ID="JobDateStartedLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date Done" SortExpression="JobDateDone">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("JobDateDone", "{0:MMM/dd/yyyy}") %>' ID="JobDateDoneLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Name") %>' ID="NameLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="Contact" SortExpression="ContactPhone">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("ContactPhone") %>' ID="ContactPhoneLabel"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text="View" CommandName="" OnClick="ViewJob_Click" CausesValidation="false" ID="viewJob"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
           
        </div>
        <div class="col-md-3">
             <asp:Button ID="NewJob" runat="server" Text="New Job" OnClick="NewJob_Click" Visible ="true"/>
        </div>
     

    </div>
    <%--ODS AREA BELOW--%>
    <asp:ObjectDataSource ID="CurrentJobsListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="JobList" 
        TypeName="eBikeSystem.BLL.JobController"></asp:ObjectDataSource>



</asp:Content>

