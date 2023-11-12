using CorNProject.Data;
using CorNProject.Models;
using CorNProject.Services.ConnectionServises;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace CorNProject.Services
{
    internal class FindReplace : FrameworkElement
    {
        private LoggWin logWindow;
        private FileFeatures features ;
        private ErrorMessages errorMessages;
        private RequestManager requestManager ;

        private int number = 0;

        public FindReplace()
        {
            features = new FileFeatures();
            errorMessages = new ErrorMessages();
            requestManager = new RequestManager();
        }

        public void RemoveLogWin()
        {
            if (logWindow != null)
                logWindow.Close();
        }

        public async void FindAndChange(FindReplaceSettings settings)
        {
            if (settings.FileList.Count != 0)
            {
                if (settings.ToFind == null || settings.ToFind == "")
                {
                    string? message = FindResource("no_txt_find").ToString();
                    MyMessageBox.Show(message, MessageBoxButton.OK);
                }
                else
                {
                    var logWin = FindAndReplace(settings.ToFind, settings.ToReplace, settings.FileList, settings.OnlyFind);

                    if (settings.OnlyFind == false)
                        await  requestManager.SetLoggsIntoDatabase(logWin);

                    logWindow = new LoggWin(logWin, settings.OnlyFind);
                    logWindow.Owner = settings.Owner;
                    logWindow.Show();
                }
            }
        }
        public List<string> InputPathClick(string FilePath)
        {
            List<string> fileList = new List<string>();
            string[] possAddresses = FilePath.Split(';');

            foreach (var address in possAddresses)
            {
                var isDerictory = errorMessages.DirectoryNotExists(address);

                if (isDerictory)
                {
                    var files = Directory.GetFiles(address);
                    foreach (var file in files)
                    {
                        fileList.Add(file);
                    }
                }
                else
                {
                    if (address != "")
                    {
                        var isFile = errorMessages.FileNOtExists(address);

                        if (isFile)
                        {
                            fileList.Add(address);
                        }
                    }
                }
            }

            return fileList;
        }
        public List<MyLogger> FindAndReplace(string toFind, string toReplace, List<string> fileNames, bool onlyFind)
        {
            List<MyLogger> myLoggs = new List<MyLogger>();

            foreach (string file in fileNames)
            {
                string line;
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
                    string? message = FindResource("Exception_info").ToString();
                    MyMessageBox.Show($"{message} {e}", MessageBoxButton.OK);
                    break;
                }

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
