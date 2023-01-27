using FootballWesbshop.Data;
using FootballWesbshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FootballWesbshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public HomeController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManger, ILogger<HomeController> logger, ApplicationDbContext db, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _logger = logger;
            _db = db;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> DelegateAdmin()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var role = new IdentityRole()
            {
                Name = "Admin"
            };
            if (!await _roleManger.RoleExistsAsync("Admin"))
            {
                await _roleManger.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View(_db.Shoes);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_userManager.Users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GrantAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Users));
        }

        public IActionResult Index()
        {
            return View(_db.Shoes);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDelete(string uid)
        {
            var item = _db.Shoes.FirstOrDefault(t => t.Uid == uid);
            if (item != null)
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