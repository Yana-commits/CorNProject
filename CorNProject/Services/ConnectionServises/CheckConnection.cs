using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CorNProject.Services.ConnectionServises
{
    public class CheckConnection
    {
        public static bool CheckForInternetConnection(ConfigService config)
        {
            var ping = new Ping();
            byte[] buffer = new byte[32];
            int timeout = 1000;
            var options = new PingOptions();

            try
            {
                var reply = ping.Send(config.Addresses.Connection, timeout, buffer, options);
                return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}
