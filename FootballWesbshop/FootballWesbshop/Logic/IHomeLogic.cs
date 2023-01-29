using FootballWesbshop.Models;

namespace FootballWesbshop.Logic
{
    public interface IHomeLogic
    {
        Shoe GetShoe(string uid);
        IEnumerable<Shoe> ReadAllShoe();
        void AddShoeToCart(SiteUser user, Shoe shoe);
        void RemoveShoeFromCart(SiteUser user, Shoe shoe);
    }
}