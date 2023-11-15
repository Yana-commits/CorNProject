using CorNProject.Services;
using CorNProject.Services.ConnectionServises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CorNProject
{
    /// <summary>
    /// Логика взаимодействия для SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        private static SplashWindow instance;

        public static SplashWindow Instance()
        {
            if (instance == null)
                instance = new SplashWindow();

            return instance;
        }
        public void Init()
        {
            InitializeComponent();
            if (this != null)
            {
                this.Show();
            }
        }
        public void CloseSplash()
        {
            if ( this != null)
            {
                this.Close();
                if (this is IDisposable)
                    (this as IDisposable).Dispose();
            }
        }
    }
}
