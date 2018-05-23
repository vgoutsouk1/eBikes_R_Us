using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eBikeSystem.BLL;
using eBike.Data.Entities;

public partial class WebSites_CurrentJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var newjob = Request.QueryString["job"];
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (newjob == "0") 
        {
            UserFullName.Text = User.Identity.Name.ToString();
       
            

        }
        else
        {
            CustomerDDL.Visible = false;

            UserFullName.Text = User.Identity.Name.ToString();
            JobID.Text = Request.QueryString["id"];
            CustomerName.Text = Request.QueryString["name"];
            ContactNumber.Text = Request.QueryString["contact"];

            JobController sysmgr = new JobController();
            int jobId = int.Parse(JobID.Text);

            employeeDDL.DataSource = sysmgr.Get_Employees();
            employeeDDL.DataBind();


            JobServiceGridView.DataSource = sysmgr.CurrentJobDetail(jobId);
            JobServiceGridView.DataBind();


        }
    }

    //protected void PresetButton_Click(object sender, EventArgs e)
    //{
    //    JobController sysmgr = new JobController();
    //    int presetid = int.Parse(PresetDDL.SelectedValue);

    //    Description.Text = sysmgr.PresetDescriptionHours(presetid);

    //}

    protected void AddServiceButton_Click(object sender, EventArgs e)
    {
        try
        {
            Job item = new Job();

            item.CustomerID = int.Parse(CustomerDDL.SelectedValue);
            item.JobDateIn = DateTime.Now;
            item.JobDateStarted = DateTime.Now;
            item.EmployeeID = int.Parse("3");
            item.ShopRate = int.Parse("55");
            item.StatusCode = "I";
            item.VehicleIdentification = "vinaladlfmsdfm1234";
           
           


            JobController sysmgr = new JobController();
            int newjob = sysmgr.Job_Add(item);


            JobServiceGridView.DataBind();

        }
        catch (Exception ex)
        {
           
        }
    }



    protected void PresetDDL_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void CustomerDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        CustomerName.Text = CustomerDDL.SelectedItem.Text;
    }

   protected void ViewPartsLinkButton_Click(object sender, EventArgs e)
   {
        GridViewRow agvrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int jobserviceid = int.Parse(((Label)agvrow.FindControl("JobDetailIDLabel")).Text);

        JobController sysmgr = new JobController();
        ServicePartsGridView.DataSource = sysmgr.ServiceParts(jobserviceid);
        ServicePartsGridView.DataBind();

        JobServiceGridView.Visible = true;
        ServicePartsGridView.Visible = true;
    }

    protected void Manage_Click(object sender, EventArgs e)
    {
        int jobId = int.Parse(JobID.Text);
        JobController sysmgr = new JobController();
        ManageServicesGridView.DataSource = sysmgr.JobDetailManage(jobId);
        ManageServicesGridView.DataBind();


        JobServiceGridView.Visible = false;


        ManageServicesGridView.Visible = true;
    }
}
