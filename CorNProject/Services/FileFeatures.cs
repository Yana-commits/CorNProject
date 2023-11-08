using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CorNProject.Services
{
    public class FileFeatures
    {
        public bool Exams(string file)
        {
            bool err = false;
            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.IsReadOnly)
            {
                MyMessageBox.Show($"File: {file} is readonly", MessageBoxButton.OK);
                err = true;
            }

            if (fileInfo.Extension == "")
            {
                MyMessageBox.Show($"File: {file} has no extention", MessageBoxButton.OK);
                err = true;
            }

            return err;
        }
    }
}
