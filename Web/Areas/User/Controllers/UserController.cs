using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Services.MessageSender;
using Web.Areas.User.Models;

namespace Web.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageSender _messageSender;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Index(string returnUrl = null)
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "User", new { area = "User",returnUrl= returnUrl });
            return View();
        }
        [Route("Login/{returnUrl?}")]
        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home", new { area = "" });

            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home", new { area = "" });
            ViewData["returnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                if (result.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است";
                    return View(model);
                }

                ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
            }
            return View(model);
        }
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var getUser = await _userManager.FindByNameAsync(user.UserName);
                    if (getUser != null)
                    {
                        var addRole= await _userManager.AddToRoleAsync(getUser,"User");
                        if (result.Succeeded)
                        {
                             var emailConfirmationToken =
                             await _userManager.GenerateEmailConfirmationTokenAsync(user);
                             var emailMessage =
                             Url.Action("ConfirmEmail", "User",
                             new { Area = "User", username = user.UserName, token = emailConfirmationToken },Request.Scheme);
                            //  await _messageSender.SendEmailAsync(model.Email, "Email confirmation", emailMessage);
                            return RedirectToAction("Index", "Home", new { Area = "" });
                        }
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Json(true);
            return Json("ایمیل وارد شده از قبل موجود است");
        }
        public async Task<IActionResult> IsUserNameInUse(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return Json(true);
            return Json("نام کاربری وارد شده از قبل موجود است");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return NotFound();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Content(result.Succeeded ? "Email Confirmed" : "Email Not Confirmed");
        }
    }
}
