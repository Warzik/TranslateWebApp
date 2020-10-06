
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TranslateApp.WebApp.Models;
using TranslateApp.WebApp.Models.AccountViewModels;
using TranslateApp.WebApp.Services;

namespace TranslateApp.WebApp.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;
		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IEmailSender emailSender,
			RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
		}


		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody]LoginViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
				if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
				{
					ModelState.AddModelError(string.Empty, "Zły adres email,hasło lub email nie został potwierdzony");
					return BadRequest(ModelState);
				}
				var validCredentials = await _signInManager.UserManager.CheckPasswordAsync(user, model.Password);
				if (!validCredentials)
				{
					ModelState.AddModelError(string.Empty, "Zły adres email lub hasło");
					return BadRequest(ModelState);
				}

				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					return Ok();

				}
				if (result.IsLockedOut)
				{
					ModelState.AddModelError(string.Empty, "User account locked out.");
					return BadRequest(ModelState);

				}
				else
				{
					ModelState.AddModelError(string.Empty, "Błąd podczas logowania. Spróbuj poźniej lub skontaktuj się z administratorem");
					return BadRequest(ModelState);

				}
			}
			ModelState.Clear();
			ModelState.AddModelError(string.Empty, "Zły adres email,hasło lub email nie został potwierdzony");
			// If we got this far, something failed, redisplay form
			return BadRequest(ModelState);
		}


		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody]RegisterViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);


				if (result.Succeeded)
				{

					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

					var url = HttpContext.Request.Host.ToString();

					var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme, url);
					_emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

					//await _signInManager.SignInAsync(user, isPersistent: false);

					return Ok();
				}
				ModelState.AddModelError(string.Empty, "Podany adres jest już zarejestrowany ");
			}

			// If we got this far, something failed, redisplay form
			return BadRequest(ModelState);
		}



		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return BadRequest();
			}
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{userId}'.");
			}
			var result = await _userManager.ConfirmEmailAsync(user, code);
			return Ok();
		}



		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
				{
					// Don't reveal that the user does not exist or is not confirmed
					ModelState.AddModelError(string.Empty, "Użytkownik nie istnieje lub nie jest potwierdzony");
					return BadRequest(ModelState);
				}

				// For more information on how to enable account confirmation and password reset please
				// visit https://go.microsoft.com/fwlink/?LinkID=532713
				var code = await _userManager.GeneratePasswordResetTokenAsync(user);

				var url = HttpContext.Request.Host.ToString();

				var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme, url);
				//callbackUrl += "&email=" + user.Email;

				_emailSender.SendEmailForgetPasswordAsync(model.Email, callbackUrl);

				return Ok();
			}

			// If we got this far, something failed, redisplay form
			return BadRequest(ModelState);
		}


		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var user = await _userManager.FindByIdAsync(model.UserId);

			if (user == null)
			{
				// Don't reveal that the user does not exist
				ModelState.AddModelError(string.Empty, "Użytkownik nie istnieje lub nie jest potwierdzony");
				return BadRequest(ModelState);
			}
			var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
			if (result.Succeeded)
			{
				return Ok();
			}
			return BadRequest(ModelState);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult SignInWithGoogle()
		{
			var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action(nameof(HandleExternalLogin)));
			return Challenge(authenticationProperties, "Google");
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult SignInWithFacebook()
		{
			var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action(nameof(HandleExternalLogin)));
			return Challenge(authenticationProperties, "Facebook");
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> HandleExternalLogin()
		{

			var info = await _signInManager.GetExternalLoginInfoAsync();
			var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

			if (!result.Succeeded) //user does not exist yet
			{
				var email = info.Principal.FindFirstValue(ClaimTypes.Email) + "-" + info.LoginProvider;

				var newUser = new ApplicationUser
				{
					UserName = email,
					Email = email,
					EmailConfirmed = false
				};
				var createResult = await _userManager.CreateAsync(newUser);
				if (!createResult.Succeeded)
					throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));

				await _userManager.AddLoginAsync(newUser, info);
				var newUserClaims = info.Principal.Claims.Append(new Claim("userId", newUser.Id));
				await _userManager.AddClaimsAsync(newUser, newUserClaims);
				await _signInManager.SignInAsync(newUser, isPersistent: false);
				await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			}
			return Redirect("http://localhost:5000/#/home");
		}


		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await _signInManager.SignOutAsync();
				return Ok();
			}
			catch
			{
				return BadRequest();
			}

		}


		[HttpPost]
		[AllowAnonymous]
		public bool CheckAuthentication()
		{
			if (User.Identity.IsAuthenticated)
				return true;

			return false;
		}


		[HttpPost]
		[AllowAnonymous]
		public bool UserIsAdmin()
		{
			if (User.IsInRole("Administrator"))
				return true;

			return false;
		}



		//[HttpPost]
		//      [AllowAnonymous]
		//      public IActionResult ExternalLogin([FromBody] string provider, [FromBody] string returnUrl = null)
		//      {
		//          // Request a redirect to the external login provider.
		//          var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
		//          var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
		//          return Challenge(properties, provider);
		//      }

		//      [HttpGet]
		//      [AllowAnonymous]
		//      public async Task<IActionResult> ExternalLoginCallback([FromBody] string returnUrl = null, [FromBody] string remoteError = null)
		//      {
		//          if (remoteError != null)
		//          {
		//              //ErrorMessage = $"Error from external provider: {remoteError}";
		//              return RedirectToAction(nameof(Login));
		//          }
		//          var info = await _signInManager.GetExternalLoginInfoAsync();
		//          if (info == null)
		//          {
		//              return RedirectToAction(nameof(Login));
		//          }

		//          // Sign in the user with this external login provider if the user already has a login.
		//          var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
		//          if (result.Succeeded)
		//          {
		//              //_logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
		//              return Redirect(returnUrl);
		//          }
		//          if (result.IsLockedOut)
		//          {
		//		// return RedirectToAction(nameof(Lockout));
		//		return RedirectToAction();
		//	}
		//          else
		//          {
		//              // If the user does not have an account, then ask the user to create an account.
		//              ViewData["ReturnUrl"] = returnUrl;
		//              ViewData["LoginProvider"] = info.LoginProvider;
		//              var email = info.Principal.FindFirstValue(ClaimTypes.Email);
		//              return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
		//          }
		//      }

		//      [HttpPost]
		//      [AllowAnonymous]
		//      public async Task<IActionResult> ExternalLoginConfirmation([FromBody] ExternalLoginViewModel model, [FromBody] string returnUrl = null)
		//      {
		//          if (ModelState.IsValid)
		//          {
		//              // Get the information about the user from the external login provider
		//              var info = await _signInManager.GetExternalLoginInfoAsync();
		//              if (info == null)
		//              {
		//                  throw new ApplicationException("Error loading external login information during confirmation.");
		//              }
		//              var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
		//              var result = await _userManager.CreateAsync(user);
		//              if (result.Succeeded)
		//              {
		//                  result = await _userManager.AddLoginAsync(user, info);
		//                  if (result.Succeeded)
		//                  {
		//                      await _signInManager.SignInAsync(user, isPersistent: false);
		//                      //_logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
		//                      return Redirect(returnUrl);
		//                  }
		//              }
		//             // AddErrors(result);
		//          }

		//          ViewData["ReturnUrl"] = returnUrl;
		//          return View(nameof(ExternalLogin), model);
		//      }


	}
}