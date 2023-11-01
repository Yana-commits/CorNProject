using CorNProject.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorNProject.Services
{
    public class ConfigService
    {
        private readonly string _filePath = "config.json";
        private readonly Addresses _addresses;
        private readonly string _licenseKey;
        private readonly DefaultCulture _defaultCulture;

        public ConfigService()
        {
            var config = GetSettings();
            _licenseKey = config.LicenseKey;
            _defaultCulture = config.DefaultCulture;
            _addresses = config.Addresses;
        }

        public Addresses Addresses => _addresses;
        public string LicenseKey => _licenseKey;
        public DefaultCulture DefaultCulture => _defaultCulture;

        private string ReadAllText(string path) => File.ReadAllText(path);
        private CornProjectSettings GetSettings()
        {
            var json = ReadAllText(_filePath);
            CornProjectSettings mmm = JsonConvert.DeserializeObject<CornProjectSettings>(json);
            return mmm;
        }

    }
}
