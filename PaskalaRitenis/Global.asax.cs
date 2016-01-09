using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
<<<<<<< HEAD
using System.Web.Http;
=======
>>>>>>> d1012c88a4b9935531d59ab8b3a62083eb4cc039
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PaskalaRitenis
{
<<<<<<< HEAD
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

=======
>>>>>>> d1012c88a4b9935531d59ab8b3a62083eb4cc039
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
<<<<<<< HEAD

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}
=======
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
>>>>>>> d1012c88a4b9935531d59ab8b3a62083eb4cc039
