using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballWesbshop.Models
{
    public class Shoe
    {
        [Key]
        public string Uid { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string OwnerId { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        public string BrandId { get; set; }

        [NotMapped]
        public virtual Brand Brand { get; set; }

        public Shoe()
        {
            Uid = Guid.NewGuid().ToString();
        }
    }
}
