using System;
using System.Collections.Generic;
using System.Windows;

namespace CorNProject.Models
{
    public class FindReplaceSettings
    { 
        public  List<string> FileList { get; set; }
        public string FilePath { get; set; }
        public string ToFind { get; set; }
        public string ToReplace { get; set; }
        public Window Owner { get; set; }
        public bool OnlyFind { get; set; }
    }
}
