using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator.Properties;


namespace Simulator.Shared
{
    class Utils
    {
        public string getBaseURL()
        {
            string baseURL = null;
            Settings.Default.Reload();

            if(Settings.Default["port"].ToString().Length == 0)
            {
                baseURL = Settings.Default["ip"].ToString();
            }
            else
            {
                baseURL = Settings.Default["ip"].ToString() + ":" + Settings.Default["port"].ToString();
            }
            return baseURL;
        }
    }
}
