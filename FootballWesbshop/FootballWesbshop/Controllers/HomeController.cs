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
        public async Task<IActionResult> GrantAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string uid)
        {
            var user = _userManager.Users.FirstOrDefault(t => t.Id == uid);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminEdit()
        {
            return View(_db.Shoes);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_userManager.Users);
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
        public async Task<IActionResult> Add(Shoe shoe, IFormFile picturedata)
        {
            var user = await _userManager.GetUserAsync(User);
            using (var stream = picturedata.OpenReadStream())
            {
                byte[] buffer = new byte[picturedata.Length];
                stream.Read(buffer, 0, (int)picturedata.Length);
                shoe.Data = buffer;
                shoe.ContentType = picturedata.ContentType;
            }
            //shoe.OwnerId = _userManager.GetUserId(this.User);
            shoe.BrandId = _db.Brands.FirstOrDefault(t => t.Name == shoe.BrandId).Uid;
            var old = _db.Shoes.FirstOrDefault(t => t.Uid == shoe.Uid);
            if (old == null)
            {
                _db.Shoes.Add(shoe);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string uid)
        {
            var item = _db.Shoes.FirstOrDefault(t => t.Uid == uid);
            if (item != null)
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

        public IActionResult GetShoeImage(string id)
        {
            var shoe = _db.Shoes.FirstOrDefault(t => t.Uid == id);
            if (shoe.ContentType.Length >= 3)
                return new FileContentResult(shoe.Data, shoe.ContentType);
            else
                return BadRequest();
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(string shoeid)
        {
            var shoe = _db.Shoes.FirstOrDefault(t => t.Uid == shoeid);
            if(shoe != null)
            {
                var user = await _userManager.GetUserAsync(this.User);
                _db.Users.FirstOrDefault(t => t.Id == user.Id).Cart.Add(shoe);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(string shoeid)
        {
            var shoe = _db.Shoes.FirstOrDefault(t => t.Uid == shoeid);
            if (shoe != null)
            {
                var user = await _userManager.GetUserAsync(this.User);
                user.Cart.Remove(shoe);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}