using AutoMapper;
using ContactApp.Data.Services.Interfaces;
using ContactApp.Domain.Entities;
using ContactApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
                    bool emailResponse = await _emailService.SendEmailAsync(user.Email, confirmationLink, "Confirm your email");

                    if (emailResponse)
                        return RedirectToAction("Index", "Home");
                    else
                    {
                        // log email failed 
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid registration attempt");
                }
            }

            return View(model);
        }
        #endregion

        #region Login/Logout
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = await _userManager.FindByEmailAsync(model.Email);
                if (identityUser != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(identityUser, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");

                    bool emailStatus = await _userManager.IsEmailConfirmedAsync(identityUser);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(model.Email), "Email is unconfirmed, please confirm it first");
                    }
                }
                ModelState.AddModelError(nameof(model.Email), "Login Failed: Invalid Email or password");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ForgotPassword
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            bool emailResponse = await _emailService.SendEmailAsync(user.Email, link, "Password Reset");
            if (emailResponse)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            else
            {
                // log email failed 
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}
