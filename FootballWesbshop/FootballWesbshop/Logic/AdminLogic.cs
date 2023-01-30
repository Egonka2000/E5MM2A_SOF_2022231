using FootballWesbshop.Controllers;
using FootballWesbshop.Data;
using FootballWesbshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FootballWesbshop.Logic
{
    public class AdminLogic : IAdminLogic
    {
        public ApplicationDbContext _db { get; set; }
        public AdminLogic(ApplicationDbContext db)

        {
            this._db = db;
        }

        public Shoe GetShoe(string uid)
        {
            return _db.Shoes.FirstOrDefault(t => t.Uid == uid);
        }
        public Brand GetBrand(string type)
        {
            var brandtype = (BrandType)Enum.Parse(typeof(BrandType), type);
            return _db.Brands.FirstOrDefault(t => t.Type == brandtype);
        }
        public IEnumerable<Shoe> ReadAllShoe()
        {
            return _db.Shoes;
        }

        public void CreateShoe(Shoe shoe)
        {
            _db.Shoes.Add(shoe);
            _db.SaveChanges();
        }

        public void DeleteShoe(string uid)
        {
            var shoe = GetShoe(uid);
            if (shoe != null)
            {
                _db.Shoes.Remove(shoe);
                _db.SaveChanges();
            }
        }

        public void CreateShoeImage(ref Shoe shoe, IFormFile picturedata)
        {
            using (var stream = picturedata.OpenReadStream())
            {
                byte[] buffer = new byte[picturedata.Length];
                stream.Read(buffer, 0, (int)picturedata.Length);
                shoe.Data = buffer;
                shoe.ContentType = picturedata.ContentType;
            }
        }
    }
}
