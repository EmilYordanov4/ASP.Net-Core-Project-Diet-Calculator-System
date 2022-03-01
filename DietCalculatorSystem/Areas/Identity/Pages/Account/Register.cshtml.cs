﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using DietCalculatorSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using DietCalculatorSystem.Data;

namespace DietCalculatorSystem.Areas.Identity.Pages.Account
{
    using static DataConstants.User;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly DietCalculatorDbContext data;

        public RegisterModel(
            DietCalculatorDbContext data,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(MaxFullNameLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinFullNameLength)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(MaxPasswordLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinPasswordLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User {FullName = Input.FullName, UserName = Input.FullName, Email = Input.Email };

                var balanced = new Diet();
                var deficit = new Diet();
                var surplus = new Diet();

                var balancedDiet = new BalancedDiet
                {
                    User = user,
                    UserId = user.Id,
                    Diet = balanced,
                    DietId = balanced.Id
                };

                var deficitDiet = new DeficitDiet
                {
                    User = user,
                    UserId = user.Id,
                    Diet = deficit,
                    DietId = deficit.Id
                };

                var surplusDiet = new SurplusDiet
                {
                    User = user,
                    UserId = user.Id,
                    Diet = surplus,
                    DietId = surplus.Id
                };

                user.BalancedDiet = balancedDiet;
                user.BalancedDietId = balanced.Id;
                user.DeficitDiet = deficitDiet;
                user.DeficitDietId = deficit.Id;
                user.SurplusDiet = surplusDiet;
                user.SurplusDietId = surplus.Id;

                this.data.Diets.AddRange(balanced, deficit, surplus);

                this.data.SaveChanges();

                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
