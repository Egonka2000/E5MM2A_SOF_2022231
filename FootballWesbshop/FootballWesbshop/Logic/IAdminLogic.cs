using FootballWesbshop.Models;

namespace FootballWesbshop.Logic
{
    public interface IAdminLogic
    {
        void CreateShoe(Shoe shoe);
        void DeleteShoe(string uid);
        Shoe GetShoe(string uid);
        Brand GetBrand(string type);
        IEnumerable<Shoe> ReadAllShoe();
    }
}