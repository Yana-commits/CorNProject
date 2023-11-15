
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

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

            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentInfo);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentInfo);

            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary resdict = new ResourceDictionary()
            {
                Source = new Uri($"/Dictionary-{currentInfo}.xaml", UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(resdict);

            Properties.Settings.Default.languageCode = currentInfo;
            Properties.Settings.Default.Save();
        }
    }
}
