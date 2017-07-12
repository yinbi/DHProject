using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHGame.Config
{
    public sealed class WebConfig
    {
        public static string DHWebSiteDBConnStr
        {
            get { return ConfigurationManager.ConnectionStrings["DHWebSiteDBConnStr"].ConnectionString; }
        }
    }
}
