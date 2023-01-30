using FootballWesbshop.Data;
using FootballWesbshop.Models;

namespace FootballWesbshop.Logic
{
    public class HomeLogic : IHomeLogic
    {
        static Random r = new Random();
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
            return _db.Shoes.OrderBy(t => t.Sequence);
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

        public void Highlight()
        {
            var h = _db.Shoes.FirstOrDefault(t => t.Sequence == 1);
            if (h != null)
            {
                h.Sequence = r.Next(10, 1000);
            }
            var arr = _db.Shoes.ToArray();
            var n = arr[r.Next(0, arr.Length)];
            n.Sequence = 1;
            _db.SaveChanges();
        }
    }
}
