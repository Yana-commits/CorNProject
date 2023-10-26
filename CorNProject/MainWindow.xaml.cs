using CorNProject.Enums;
using CorNProject.Models;
using CorNProject.Properties.Langs;
using CorNProject.Requests;
using CorNProject.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
        private StatusEnum status = StatusEnum.OnlineVersion;
      
        private List<string> fileList = new List<string>();
        private Action<Button, StatusEnum> btnName;
        public MainWindow()
        {
            SetLang.ToSetLang();
            statusChecker.isActualKey += CheckKey;

            btnName += ChangeBtnName;
           
            DataContext = this;
            InitializeComponent();
            statusChecker.SetTimer();

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
                if (isActual)
                {
                    status = StatusEnum.OnlineVersion;
                    ChangeTxt(Status, "");
                  
                    btnName?.Invoke(btnChange, status);
                }
                else 
                {
                    status = StatusEnum.NoLicense;
                    ChangeTxt(Status, status.ToString());
                  
                    btnName?.Invoke(btnChange, status);
                } 
            }
            else 
            {
                status = StatusEnum.NoConnection;
                ChangeTxt(Status, status.ToString());
             
                btnName?.Invoke(btnChange, status);
            }
        }

        private void ChangeBtnName(Button button,StatusEnum status)
        {
            if (status == StatusEnum.OnlineVersion)
                ChangeBtn(button, Lang.change);
            
            else
                ChangeBtn(button, Lang.find); 
        }
        private void ChangeBtn(Button button,string cont)
        {
            this.Dispatcher.Invoke(() =>
            {
                button.Content = cont;
            });
        }
        private void ChangeTxt(TextBlock block, string cont)
        {
            this.Dispatcher.Invoke(() =>
            {
                block.Text = cont;
            });
        }
    }
}