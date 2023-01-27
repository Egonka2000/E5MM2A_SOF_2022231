using FootballWesbshop.Data;
using FootballWesbshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FootballWesbshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(UserManager<SiteUser> userManager, ILogger<HomeController> logger, ApplicationDbContext db)
        {
            this._userManager = userManager;
            this._logger = logger;
            this._db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Shoes);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Shoe shoe)
        {
            shoe.OwnerId = _userManager.GetUserId(this.User);
            shoe.BrandId = _db.Brands.FirstOrDefault(t => t.Name == shoe.BrandId).Uid;
            var old = _db.Shoes.FirstOrDefault(t => t.Price == shoe.Price && t.OwnerId == shoe.OwnerId);
            if(old == null) 
            {
                _db.Shoes.Add(shoe);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete (string uid)
        {
            var item = _db.Shoes.FirstOrDefault(t => t.Uid == uid);
            if(item != null && item.OwnerId == _userManager.GetUserId(this.User)) 
            {
                _db.Shoes.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public  IActionResult GetImage(string userid)
        {
            var user =  _userManager.Users.FirstOrDefault(t => t.Id == userid);
            return new FileContentResult(user.Data, user.ContentType);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}