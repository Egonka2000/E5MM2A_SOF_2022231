using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballWesbshop.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public virtual ICollection<Shoe> Cart { get; set; }
        public SiteUser()
        {
            Cart = new HashSet<Shoe>();
        }
    }
}
