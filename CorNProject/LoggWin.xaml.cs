using CorNProject.Data;
using CorNProject.Properties.Langs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace CorNProject
{
    /// <summary>
    /// Логика взаимодействия для LoggWin.xaml
    /// </summary>
    public partial class LoggWin : Window
    {
        private ObservableCollection<LogToShow> _gridItems;
        public ObservableCollection<LogToShow> GridItems
        {
            get => _gridItems;
            set => _gridItems = value;
        }

        private string actPlural = Lang.replacements;
        private string actOnly = Lang.replacement;
        private string actPlralRus = Lang.replacements1;
        private string act = Lang.produced;

        public LoggWin(List<MyLogger> _loggs, bool onlyFind)
        {
            if (onlyFind)
            {
                actOnly = Lang.match;
                actPlural = Lang.matches;
                actPlralRus = Lang.matches1;
                act = Lang.found;
            }

            GridItems = new ObservableCollection<LogToShow>();

            foreach (var item in _loggs)
            {
                var rep = "  " + actPlural;

                if ((item.Number == 1) || (item.Number > 20 && item.Number % 10 == 1))
                    rep = "  " + actOnly;

                else if ((item.Number > 1 && item.Number < 5) || (item.Number > 20 && item.Number % 10 > 1 && item.Number % 10 < 5))
                    rep = "  " + actPlralRus;

                var repMessage = act + " " + item.Number.ToString() + rep;
                GridItems.Add(new LogToShow() { File = item.FileName, ReplaceMessage = repMessage });
            }

            DataContext = this;

            InitializeComponent();
            LoggsData.IsReadOnly = true;
        }
    }
}
