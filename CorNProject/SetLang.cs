using CorNProject.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorNProject
{
    public static class SetLang
    {
        private const string defaulltEng = "en-US";
        private const string defaulltRu = "ru-RU";

        public static string currentInfo;

        public static void ToSetLang()
        {
            var cultureInfo = CultureInfo.CurrentCulture.Name;

            if (cultureInfo == defaulltEng || cultureInfo == defaulltRu)
            {
                currentInfo = cultureInfo;
            }
            else
            {
                currentInfo = defaulltRu;  
            }

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentInfo);

            Properties.Settings.Default.languageCode = currentInfo;
            Properties.Settings.Default.Save();
        }
    }
}
