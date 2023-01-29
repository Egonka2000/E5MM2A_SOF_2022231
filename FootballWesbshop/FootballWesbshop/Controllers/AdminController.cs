using FootballWesbshop.Data;
using FootballWesbshop.Logic;
using FootballWesbshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FootballWesbshop.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        IAdminLogic _adminLogic;

        public AdminController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManger, ILogger<HomeController> logger, IEmailSender emailSender, IAdminLogic adminlogic)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _logger = logger;
            _emailSender = emailSender;
            _adminLogic = adminlogic;
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
            return RedirectToAction("Index", "Home");
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
            return View(_adminLogic.ReadAllShoe());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_userManager.Users);
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            _adminLogic.CreateShoeImage(ref shoe, picturedata);
            shoe.BrandId = _adminLogic.GetBrand(shoe.BrandId).Uid;
            var old = _adminLogic.GetShoe(shoe.Uid);
            if (old == null)
            {
                _adminLogic.CreateShoe(shoe);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDelete(string uid)
        {
            _adminLogic.DeleteShoe(uid);
            return RedirectToAction(nameof(AdminEdit));
        }
    }
}
