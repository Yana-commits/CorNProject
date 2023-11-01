using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorNProject.Config
{
    public class CornProjectSettings
    {
        public Addresses Addresses { get; set; }
        public string LicenseKey { get; set; }
        public DefaultCulture DefaultCulture { get; set; }
    }
}
