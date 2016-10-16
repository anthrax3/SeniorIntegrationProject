using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using COS492_SIP.DAL;

namespace COS492_SIP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //System.Data.Entity.Database.SetInitializer<MyDbContext>(new MyDbInitializer());
            //var db = new MyDbContext();
            //db.Database.Initialize(true);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
