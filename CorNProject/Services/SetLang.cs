using CorNProject.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorNProject.Services
{
    public static class SetLang
    {
        public static string currentInfo;

        public static void ToSetLang()
        {
            var config = new ConfigService();

            var cultureInfo = CultureInfo.CurrentCulture.Name;

            if (cultureInfo == config.DefaultCulture.DefaulltEng || cultureInfo == config.DefaultCulture.DefaulltRu)
            {
                currentInfo = cultureInfo;
            }
            else
            {
                currentInfo = config.DefaultCulture.DefaulltEng;
            }

           Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentInfo);

            Properties.Settings.Default.languageCode = currentInfo;
            Properties.Settings.Default.Save();
        }
    }
}
