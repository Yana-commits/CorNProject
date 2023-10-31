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
        private Action<Button, StatusEnum> btnName;
        private ChoiceFieldEnum choiceField = ChoiceFieldEnum.Inputs;
        private string previousAdress = "";
        public DialogData Data { get; }
        public MainWindow()
        {
            SetLang.ToSetLang();
            StatusChecker.Instance().isActualKey += CheckKey;
            StatusChecker.Instance().SetTimer();

            Data = new DialogData();
            DataContext = this;
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnName += ChangeBtnName;

            btnFile.Click += BtnFileClick;
            btnDirectory.Click += BtnDirectoryClick;
            btnClear.Click += ClearPahtInputFields;
            btnChange.Click += FindAndChange;

            SetStatusLook(status);
        }

        async void BtnDirectoryClick(object sender, RoutedEventArgs e)
        {
            findReplace.RemoveLogWin();

            ClearFields();

            //Data.FilePath = dialogManager.DirectoryDialog(Data.FilePath, fileList, ref FilePathTextBox);

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
                    Data.FilePath = fileDialog.FileName;
                }
            }
            previousAdress = Data.FilePath;
        }
        async void BtnFileClick(object sender, RoutedEventArgs e)
        {
            findReplace.RemoveLogWin();

            ClearFields();

            //Data.FilePath = dialogManager.FileDialog(Data.FilePath,ref fileList,ref FilePathTextBox);

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
                        //Data.FilePath = file;
                    }
                }
            }
            previousAdress = Data.FilePath;
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
        private void AddFileFromDialog(string file)
        {
            fileList.Add(file);
            Data.FilePath += file;
            Data.FilePath += ";";
        }
        private async void ClearPahtInputFields(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            fileList.Clear();
            //Data.FilePath = "";
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

            btnName?.Invoke(btnChange, status);
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