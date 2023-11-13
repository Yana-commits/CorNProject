using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CorNProject.Services
{
    public class FileFeatures : FrameworkElement
    {
        public bool Exams(string file)
        {
            bool err = false;
            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.IsReadOnly)
            {
                string? message1 = FindResource("File:").ToString();
                string? message2 = FindResource("is_readonly").ToString();
                MyMessageBox.Show($"{message1} {file} {message2}", MessageBoxButton.OK);
                err = true;
            }

            if (fileInfo.Extension == "")
            {
                string? message1 = FindResource("File:").ToString();
                string? message2 = FindResource("is_readonly").ToString();
                MyMessageBox.Show($"{message1} {file} {message2}", MessageBoxButton.OK);
                err = true;
            }

            return err;
        }
    }
}
