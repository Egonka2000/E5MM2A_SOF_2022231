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
        public string? ContentType { get; set; }
        public byte[]? Data { get; set; }
        public string BrandId { get; set; }

        [NotMapped]
        public virtual Brand? Brand { get; set; }

        [NotMapped]
        public virtual ICollection<SiteUser> Owners { get; set; }

        public Shoe()
        {
            Uid = Guid.NewGuid().ToString();
            Owners = new HashSet<SiteUser>();
        }
        public override string ToString()
        {
            return $"{Brand.Name} {Brand.Type}";
        }
    }
}
