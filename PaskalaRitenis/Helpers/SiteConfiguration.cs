using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PaskalaRitenis
{
    public static class SiteConfiguration
    {
        public static bool EnableRegistration()
        {
            var result = ConfigurationManager.AppSettings["RegistrationEnabled"].ToString();

            if (result == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}