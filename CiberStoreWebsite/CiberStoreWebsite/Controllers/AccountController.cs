using AutoMapper;
using System.Threading.Tasks;
using CiberStoreWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CiberStoreWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _IdentityUserManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(IMapper mapper, UserManager<IdentityUser> IdentityUserManager, SignInManager<IdentityUser> signInManager)
        {
            _mapper = mapper;
            _IdentityUserManager = IdentityUserManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel IdentityUserModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(IdentityUserModel);
            }

            var result = await _signInManager.PasswordSignInAsync(IdentityUserModel.Email, IdentityUserModel.Password, IdentityUserModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid IdentityUserName or Password");
                return View();
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel IdentityUserModel)
        {
            if (!ModelState.IsValid)
            {
                return View(IdentityUserModel);
            }

            var IdentityUser = _mapper.Map<IdentityUser>(IdentityUserModel);
            var result = await _IdentityUserManager.CreateAsync(IdentityUser, IdentityUserModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(IdentityUserModel);
            }

            await _IdentityUserManager.AddToRoleAsync(IdentityUser, "Visitor");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
