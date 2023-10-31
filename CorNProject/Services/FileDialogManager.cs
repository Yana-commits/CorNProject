
using System.IO;
using WindowsAPICodePack.Dialogs;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Security.Policy;
using CorNProject.Models;

namespace CorNProject.Services
{
    internal class FileDialogManager
    {
        private ErrorMessages errorMessages = new ErrorMessages();
        public DialogModel DirectoryDialog( )
        {
            DialogModel model= new DialogModel();

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
                        model.FileList.Add(file);
                    }
                    model.FilePath = fileDialog.FileName;
                }
            }
           
            return model;
        }

        public DialogModel FileDialog()
        {
            DialogModel model = new DialogModel();

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
                        model.FileList.Add(file);
                        model.FilePath += file;
                        model.FilePath += ";";
                    }
                }
            }
            return model;
        }
       
    }
}
