using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_API.Models
{
    public class ProducedOperation
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string License { get; set; }
        public string File { get; set; }
        public int Produced { get; set; }
    }
}
