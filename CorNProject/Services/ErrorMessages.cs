using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace CorNProject.Services
{
    public class ErrorMessages
    {
        public bool FileNOtExists(string path)
        {
            var exist = File.Exists(path);

            if (exist == false)
            {
                MyMessageBox.Show("File is not exists", MessageBoxButton.OK);
                return false;
            }

            return true;
        }
        public bool DirectoryNotExists(string path)
        {
            var exists = Directory.Exists(path);
            if (exists == false)
            {
                //MessageBox.Show("Directory is not exists", "Path eror", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
