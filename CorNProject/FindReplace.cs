using CorNProject.Data;
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
        private FileFeatures features = new FileFeatures();

        private int number = 0;

        public List<MyLogger> FindAndReplace(string toFind, string toReplace, List<string> fileNames)
        {
             List<MyLogger> myLoggs = new List<MyLogger>();

            foreach (string file in fileNames)
            {
               String line;
               List<string> strings = new List<string>();
                //bool toLogg = true;
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
                            var myLine = Replace(line, toFind, toReplace);
                            //var myLine = line.Replace(toFind, toReplace);

                            //if (myLine != line)
                            //    i++;

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

        private string Replace(string srcStr, string searchStr, string replStr)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < srcStr.Length; i++)
            {
                if (IsSubstr(srcStr, i, searchStr))
                {
                    builder.Append(replStr);
                    i += searchStr.Length - 1;
                    number++;
                }
                else builder.Append(srcStr[i]);
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
