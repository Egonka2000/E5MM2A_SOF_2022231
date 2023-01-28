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

        public IActionResult Index()
        {
            return View(_db.Shoes);
        }

        [Authorize]
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