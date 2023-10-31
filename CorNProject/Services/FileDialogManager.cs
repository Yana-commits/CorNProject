
using System.IO;
using WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CorNProject.Services
{
    internal class FileDialogManager
    {
        private ErrorMessages errorMessages = new ErrorMessages();
        public string DirectoryDialog(string filePath, List<string> fileList,ref TextBox textBox)
        {
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
                        AddFileFromDialog(file,fileList,ref textBox);
                    }
                    filePath = fileDialog.FileName;
                    //Data.FilePath
                }
            }
            return filePath;
        }

        public string FileDialog(string filePath,ref List<string> fileList,ref TextBox textBox)
        {

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
                        AddFileFromDialog(file, fileList,ref textBox);
                        filePath = file;
                        //Data.FilePath
                    }
                }
            }
            return filePath;
        }
        private void AddFileFromDialog(string file, List<string> fileList,ref TextBox textBox)
        {
            fileList.Add(file);
            textBox.Text += file;
            textBox.Text += "; ";
        }
    }
}
