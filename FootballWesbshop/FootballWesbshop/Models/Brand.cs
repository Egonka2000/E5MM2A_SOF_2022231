using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballWesbshop.Models
{
    public class Brand
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public BrandType? Type { get; set; }
    }
    public enum BrandType
    {
        Predator, X, Nemesis, Mercurial, Hypervenom
    }
}