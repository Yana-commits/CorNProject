using CorNProject.Models;
using CorNProject.Requests;
using CorNProject.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WindowsAPICodePack.Dialogs;
using static System.Net.WebRequestMethods;

namespace CorNProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ErrorMessages errorMessages = new ErrorMessages();
        private FindReplace findReplace = new FindReplace();
        private StatusChecker statusChecker = new StatusChecker();
      
        private List<string> fileList = new List<string>();
        public MainWindow()
        {
            SetLang.ToSetLang();
            statusChecker.isActualKey += CheckKey;
            statusChecker.SetTimer();

            DataContext = this;
            InitializeComponent();

            btnFile.Click += BtnFindClick;
            btnDirectory.Click += BtnDirectoryClick;
            btnClear.Click += ClearPahtInputFields;
            btnChange.Click += FindAndChange;

        }
        private string txtToFind;
        public string TxtToFind
        {
            get { return txtToFind; }
            set
            {
                txtToFind = value;
                OnPropertyChanged("TxtToFind");
            }
        }
        private string txtToReplace;
        public string TxtToReplace
        {
            get { return txtToReplace; }
            set
            {
                txtToReplace = value;
                OnPropertyChanged("TxtToReplace");
            }
        }
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async void BtnDirectoryClick(object sender, RoutedEventArgs e)
        {
            findReplace.RemoveLogWin();
           
            ClearInputFields();

            var fileDialog = new CommonOpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.IsFolderPicker = true;

            var success = fileDialog.ShowDialog();

            if (success == CommonFileDialogResult.Ok)
            {
                var isDerictory = errorMessages.DirectoryNotExists(fileDialog.FileName);

                if (isDerictory)
                {
                    var files = Directory.GetFiles(fileDialog.FileName);
                    foreach (var file in files)
                    {
                        AddFileFromDialog(file);
                    }
                    FilePath = fileDialog.FileName;
                }
            }
        }
        async void BtnFindClick(object sender, RoutedEventArgs e)
        {
            findReplace.RemoveLogWin();

            ClearInputFields();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                foreach (string file in fileDialog.FileNames)
                {
                    var isFile = errorMessages.FileNOtExists(file);

                    if (isFile)
                    {
                        AddFileFromDialog(file);
                        FilePath = file;
                    }
                }
            }
        }
       
        private void FindAndChange(object sender, RoutedEventArgs e)
        {
            findReplace.RemoveLogWin();
            var settings = new FindReplaceSettings()
            {
                FileList = fileList,
                FilePath = filePath,
                ToFind = TxtToFind,
                ToReplace = TxtToReplace,
                Owner = this,
                OnlyFind = false
            };

            findReplace.FindAndChange(settings);  
        }
        private void AddFileFromDialog(string file)
        {
            fileList.Add(file);
            FilePathTextBox.Text += file;
            FilePathTextBox.Text += "; ";
        }
        private async void ClearPahtInputFields(object sender, RoutedEventArgs e)
        {
       
            ClearInputFields();
        }
        private void ClearInputFields()
        {
            FilePathTextBox.Text = "";
            FilePath = null;
        }
        private void CheckKey(bool isActual)
        {
            if (CheckConnection.CheckForInternetConnection())
            {
                var random = new Random();
                var mmm = random.Next(1, 100);

                this.Dispatcher.Invoke(() =>
                {
                    Status.Text = mmm.ToString();
                });
            }
            else 
            {
            
            }
        }
    }
}