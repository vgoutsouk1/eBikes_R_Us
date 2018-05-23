<%@ Application Language="C#" %>
<%@ Import Namespace="eBikeWebsite" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="eBikeSystem.BLL.Security" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        var RoleManager = new RoleManager();
        RoleManager.AddDefaultRoles();

        var UserManager = new UserManager();
        UserManager.AddWebMaster();
        UserManager.AddEmployees();
    }

</script>
