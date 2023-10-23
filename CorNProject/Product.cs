using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CorNProject
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Units { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
