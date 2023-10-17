using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace CorNProject
{
    /// <summary>
    /// Логика взаимодействия для TextWindow.xaml
    /// </summary>
    public partial class TextWindow : Window, INotifyPropertyChanged
    {
        private FindReplace findReplace = new FindReplace();

        private string txtToFind;
        private string txtToReplace;    

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<string> FileName { get; set; }
        public string TxtToFind
        {
            get { return txtToFind; }
            set
            {
                txtToFind = value;
                OnPropertyChanged("TxtToFind");
            }
        }
        public string TxtToReplace
        {
            get { return txtToReplace; }
            set
            {
                txtToReplace = value;
                OnPropertyChanged("TxtToReplace");
            }
        }

        public TextWindow(List<string> _fileName)
        {
            DataContext = this;
            InitializeComponent();

            FileName = _fileName;

            btnFind.Click += Find;
            menuSave.Click += Save_Executed;
            menuSaveAs.Click += SaveAs_Executed;
            menuExit.Click += Exit;
            //findReplace.toClose += WinClose;

            //TxtBoxDoc.Text = File.ReadAllText(_fileName);
        }

        private void Save_Executed(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }
        private void SaveAs_Executed(object sender, RoutedEventArgs e)
        {
            SaveFile(true);
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            WinClose();
        }
        public void WinClose()
        {
            this.Close();
        }
        private void Find(object sender, RoutedEventArgs e)
        {
            if (TxtToFind == null)
            {
                MessageBox.Show("Need to input text to find");
            }
            else
            {
              var logWin =  findReplace.FindAndReplace(TxtToFind, TxtToReplace, FileName);

                LoggWin logWindow = new LoggWin(logWin);
                logWindow.Show();
            }  
        }
       
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveFile(bool saveAs = false)
        {
            //if (File.Exists(FileName) && !saveAs)
            //{
            //    File.WriteAllText(FileName, TxtBoxDoc.Text);
            //}
            //else
            //{
            //    SaveFileDialog dlg = new SaveFileDialog();
            //    dlg.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*"; ;
            //    if (dlg.ShowDialog() == true)
            //    {
            //        File.WriteAllText(FileName, TxtBoxDoc.Text);
            //    }
            //}
        }
        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
