using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorNProject.Models
{
    public struct CheckInputsModel
    {
        public CheckInputsModel()
        {
        }

        public string PreviousAdress { get; set; } = null;
        public List<string> FileList { get; set; } = new List<string>();
    }
}
