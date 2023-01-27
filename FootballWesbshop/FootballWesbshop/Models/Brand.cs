using System.ComponentModel.DataAnnotations;

namespace FootballWesbshop.Models
{
    public class Brand
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
    }
}
