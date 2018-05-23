using eBike.Data.Entities.Security;
using eBikeSystem.BLL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSites_Jobing : System.Web.UI.Page
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
                if (!User.IsInRole(SecurityRoles.Jobing) && !User.IsInRole(SecurityRoles.WebsiteAdmins))
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }

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

    protected void NewJob_Click(object sender, EventArgs e)
    {
        var newjob = "0";
        Response.Redirect(String.Format("CurrentJob.aspx?job={0}",newjob), false);
    }


    protected void ViewJob_Click(object sender, EventArgs e)
    {
        GridViewRow agvrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int jobId = int.Parse(((Label)agvrow.FindControl("JobIDLabel")).Text);
        string customerName = ((Label)agvrow.FindControl("NameLabel")).Text;
        string contactNumber = ((Label)agvrow.FindControl("ContactPhoneLabel")).Text;
        Response.Redirect(String.Format("CurrentJob.aspx?id={0}&name={1}&contact={2}", jobId, customerName, contactNumber), false);
       
    }
}