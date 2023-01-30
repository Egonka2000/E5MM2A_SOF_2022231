using FootballWesbshop.Data;
using FootballWesbshop.Models;

namespace FootballWesbshop.Logic
{
    public class HomeLogic : IHomeLogic
    {
        public ApplicationDbContext _db { get; set; }
        public HomeLogic(ApplicationDbContext db)

        {
            this._db = db;
        }

        public Shoe GetShoe(string uid)
        {
            return _db.Shoes.FirstOrDefault(t => t.Uid == uid);
        }

        public IEnumerable<Shoe> ReadAllShoe()
        {
            return _db.Shoes;
        }

        public void AddShoeToCart(SiteUser user, Shoe shoe)
        {
            user.Cart.Add(shoe);
            _db.SaveChanges();
        }

        public void RemoveShoeFromCart(SiteUser user, Shoe shoe)
        {
            user.Cart.Remove(shoe);
            _db.SaveChanges();
        }

        public void ClearCart(SiteUser user)
        {
            user.Cart.Clear();
            _db.SaveChanges();
        }
    }
}
