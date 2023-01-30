using FootballWesbshop.Data;
using FootballWesbshop.Logic;
using FootballWesbshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FootballWesbshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        IHomeLogic _homeLogic;

        public HomeController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManger, ILogger<HomeController> logger, IEmailSender emailSender, IHomeLogic homeLogic)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _logger = logger;
            _emailSender = emailSender;
            _homeLogic = homeLogic;
        }

        public IActionResult Index()
        {
            return View(_homeLogic.ReadAllShoe());
        }

        [Authorize]
        public  IActionResult GetImage(string userid)
        {
            var user =  _userManager.Users.FirstOrDefault(t => t.Id == userid);
            return new FileContentResult(user.Data, user.ContentType);
        }

        public IActionResult GetShoeImage(string id)
        {
            var shoe = _homeLogic.GetShoe(id);
            if (shoe.ContentType.Length >= 3)
                return new FileContentResult(shoe.Data, shoe.ContentType);
            else
                return BadRequest();
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(string shoeid)
        {
            var shoe = _homeLogic.GetShoe(shoeid);
            if(shoe != null)
            {
                var user = await _userManager.GetUserAsync(this.User);
                _homeLogic.AddShoeToCart(user, shoe);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(string shoeid)
        {
            var shoe = _homeLogic.GetShoe(shoeid);
            if (shoe != null)
            {
                var user = await _userManager.GetUserAsync(this.User);
                _homeLogic.RemoveShoeFromCart(user, shoe);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(this.User);
            return View(user.Cart);
        }

        public async Task<IActionResult> Order()
        {
            var user = await _userManager.GetUserAsync(this.User);
            string ordered = "";
            foreach (var item in user.Cart)
            {
                ordered += item + ", ";
            }
            _homeLogic.ClearCart(user);
            await _emailSender.SendEmailAsync(user.Email, "You have ordered the following shoes: ", $"{ordered}");
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}