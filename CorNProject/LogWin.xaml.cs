using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CorNProject.Data;

namespace CorNProject
{
    /// <summary>
    /// Логика взаимодействия для LogWin.xaml
    /// </summary>
    public partial class LogWin : Window
    {
        private ObservableCollection<LogToShow> _gridItems;
        public ObservableCollection<LogToShow> GridItems 
        {
            get => _gridItems; 
            set => _gridItems = value; 
        }

        public LogWin(List<MyLogger> _loggs)
        {
          
            GridItems = new ObservableCollection<LogToShow>();
           
            foreach (var item in _loggs)
            {
                var rep = "  замен";

                if ((item.Number == 1)||(item.Number > 20 && item.Number % 10 == 1))
                    rep = "  замена";

                else if((item.Number>1 && item.Number<5)||(item.Number>20 && item.Number%10 >1 && item.Number%10 <5))
                    rep = "  замены";

                var repMessage = "произведено  "+ item.Number.ToString() + rep;
                GridItems.Add(new LogToShow(){File = item.FileName, ReplaceMessage = repMessage });
            }

            DataContext = this;
           
            InitializeComponent();
            LoggsData.IsReadOnly = true;
        } 
    }
}
