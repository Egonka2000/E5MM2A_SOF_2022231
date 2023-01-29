using FootballWesbshop.Models;

namespace FootballWesbshop.Logic
{
    public interface IAdminLogic
    {
        void CreateShoe(Shoe shoe);
        void DeleteShoe(string uid);
        Shoe GetShoe(string uid);
        Brand GetBrand(string name);
        IEnumerable<Shoe> ReadAllShoe();
        void CreateShoeImage(ref Shoe shoe, IFormFile picturedata);
    }
}