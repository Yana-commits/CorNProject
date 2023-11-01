using CorNProject.Enums;
using CorNProject.Models;
using CorNProject.Properties.Langs;
using CorNProject.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WindowsAPICodePack.Dialogs;

namespace CorNProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ErrorMessages errorMessages = new ErrorMessages();
        private FindReplace findReplace = new FindReplace();
        private FileDialogManager dialogManager = new FileDialogManager();
        private StatusEnum status = StatusEnum.OnlineVersion;

        private List<string> fileList = new List<string>();
        private string previousAdress = "";
        public DialogData Data { get; }
        public MainWindow()
        {
            SetLang.ToSetLang();
            InitializeComponent();

            StatusChecker.Instance().isActualKey += CheckKey;
            StatusChecker.Instance().SetTimer();

            Data = new DialogData();
            DataContext = this; 
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnFile.Click += BtnFileClick;
            btnDirectory.Click += BtnDirectoryClick;
            btnClear.Click += ClearPahtInputFields;
            btnChange.Click += FindAndChange;

            SetStatusLook(status);
        }

        private void BtnDirectoryClick(object sender, RoutedEventArgs e)
        {
            OpenDialogWin(dialogManager.DirectoryDialog);
        }
        private void BtnFileClick(object sender, RoutedEventArgs e)
        {
            OpenDialogWin(dialogManager.FileDialog);
        }

        private void OpenDialogWin(Func<DialogModel> dialogModelAction)
        {
            findReplace.RemoveLogWin();

            ClearFields();

            DialogModel dalogModel = dialogModelAction();
            Data.FilePath = dalogModel.FilePath;
            previousAdress = dalogModel.FilePath;
            fileList = dalogModel.FileList;
        }
       
        private void FindAndChange(object sender, RoutedEventArgs e)
        {
            CheckInputs();

            if (fileList.Count != 0)
            {
                var onlyFind = false;

                if (status != StatusEnum.OnlineVersion)
                    onlyFind = true;

                var settings = new FindReplaceSettings()
                {
                    FileList = fileList,
                    FilePath = Data.FilePath,
                    ToFind = Data.TxtToFind,
                    ToReplace = Data.TxtToReplace,
                    Owner = this,
                    OnlyFind = onlyFind
                };

                findReplace.FindAndChange(settings);
            }

        }
        private void CheckInputs()
        {
            findReplace.RemoveLogWin();

            if (Data.FilePath == null || Data.FilePath == "")
            {
                MyMessageBox.Show("No files are choosen to be changed", MessageBoxButton.OK);
                fileList.Clear();
                return;
            }
            else if (previousAdress != Data.FilePath)
            {
                fileList = findReplace.InputPathClick(Data.FilePath);
                previousAdress = Data.FilePath;
            }
        }
      
        private async void ClearPahtInputFields(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            fileList.Clear();
            Data.FilePath = null;
        }
        private void CheckKey(StatusEnum _status)
        {
            var currStatus = status;
            status = _status;

            if (status != currStatus)
                SetStatusLook(_status);
        }

        private void SetStatusLook(StatusEnum status)
        {
            var text = "";

            if (status != StatusEnum.OnlineVersion)
                text = status.ToString();

            ChangeBtnName(btnChange, status);
            ChangeTxt(Status, text);
        }
        private void ChangeBtnName(Button button, StatusEnum status)
        {
            if (status == StatusEnum.OnlineVersion)
                ChangeBtn(button, Lang.change);

            else
                ChangeBtn(button, Lang.find);
        }
        private void ChangeBtn(Button button, string cont)
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