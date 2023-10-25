using CorNProject.Data;
using CorNProject.Models;
using CorNProject.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace CorNProject
{
    internal class FindReplace
    {
        private LoggWin logWindow;
        private FileFeatures features = new FileFeatures();
        private ErrorMessages errorMessages = new ErrorMessages();

        private int number = 0;

        public void RemoveLogWin()
        {
            if (logWindow != null)
                logWindow.Close();
        }
        public void FindAndChange(FindReplaceSettings settings)
        {

            if (settings.FileList.Count == 0)
            {
                if (settings.FilePath == null || settings.FilePath == "")
                {
                    MyMessageBox.Show("No files are choosen to be changed", MessageBoxButton.OK);
                }
                else
                {
                    InputPathClick(settings.FileList, settings.FilePath);
                }

            }

            if (settings.FileList.Count != 0)
            {
                if (settings.ToFind == null || settings.ToFind == "")
                {
                    MyMessageBox.Show("Need to input text to find", MessageBoxButton.OK);
                }
                else
                {
                    var logWin = FindAndReplace(settings.ToFind, settings.ToReplace, settings.FileList, settings.OnlyFind);

                    logWindow = new LoggWin(logWin,settings.OnlyFind);
                    logWindow.Owner = settings.Owner;
                    logWindow.Show();
                }
                settings.FileList.Clear();
            }
        }
        private void InputPathClick(List<string> fileList, string FilePath)
        {

            var isDerictory = errorMessages.DirectoryNotExists(FilePath);

            if (isDerictory)
            {
                var files = Directory.GetFiles(FilePath);
                foreach (var file in files)
                {
                    fileList.Add(file);
                }
            }
            else
            {
                var isFile = errorMessages.FileNOtExists(FilePath);

                if (isFile)
                {
                    fileList.Add(FilePath);
                }
            }
        }
        public List<MyLogger> FindAndReplace(string toFind, string toReplace, List<string> fileNames, bool onlyFind)
        {
            List<MyLogger> myLoggs = new List<MyLogger>();

            foreach (string file in fileNames)
            {
                String line;
                List<string> strings = new List<string>();

                number = 0;

                if (features.Exams(file))
                    break;

                try
                {
                    using (var sr = new StreamReader(file))
                    {
                        line = sr.ReadLine();

                        while (line != null)
                        {
                            var myLine = Replace(line, toFind, toReplace, onlyFind);

                            strings.Add(myLine);
                            line = sr.ReadLine();
                        }

                        sr.Close();
                        File.Delete(file);
                    }

                    using (var outputFile = new StreamWriter(file))
                    {
                        foreach (string tt in strings)
                            outputFile.WriteLineAsync(tt);
                    }
                }
                catch (SystemException e)
                {
                    MyMessageBox.Show($"Exception information: {e}", MessageBoxButton.OK);
                    break;
                }
                //finally
                //{
                //    MessageBox.Show("Executing finally block.");
                //}

                var logger = new MyLogger()
                {
                    FileName = file,
                    Name = Path.GetFileName(file),
                    Number = number,
                };
                myLoggs.Add(logger);
            }
            return myLoggs;
        }

        private string Replace(string srcStr, string searchStr, string replStr, bool onlyFnd)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < srcStr.Length; i++)
            {

                if (IsSubstr(srcStr, i, searchStr))
                {
                    if (!onlyFnd)
                    {
                        builder.Append(replStr);
                        i += searchStr.Length - 1;
                    }
                    else 
                        builder.Append(srcStr[i]);
                
                    number++;
                }
                else
                    builder.Append(srcStr[i]);
            }
            return builder.ToString();
        }
        private bool IsSubstr(string src, int index, string substr)
        {
            for (int i = 0; i < substr.Length; i++)
            {
                if (substr[i] != src[index + i]) return false;
            }
            return true;
        }
    }
}
