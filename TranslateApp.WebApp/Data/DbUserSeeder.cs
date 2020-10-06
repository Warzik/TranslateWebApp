using TranslateApp.Common;
using TranslateApp.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TranslateApp.WebApp.Data
{
	public class DbUserSeeder
	{
		public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
	   RoleManager<IdentityRole> roleManager)
		{
			context.Database.EnsureCreated();

			if (context.Roles.FirstOrDefault(p => p.Name == "Administrator") != null)
			{
				return;
			}

			await CreateDefaultUserAndRoleForApplication(userManager, roleManager);
		}

		private static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
		{
			const string administratorRole = "Administrator";
			const string userName = ConstVal.AdminEmail;

			await CreateDefaultAdministratorRole(rm, administratorRole);
			var user = await CreateDefaultUser(um, userName);
			await SetPasswordForDefaultUser(um, userName, user);
			await AddDefaultRoleToDefaultUser(um, userName, administratorRole, user);

			string code = await um.GenerateEmailConfirmationTokenAsync(user);
			await um.ConfirmEmailAsync(user, code);



		}

		private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, string administratorRole)
		{
			var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
		}

		private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> um, string userName)
		{

			var user = new ApplicationUser() { UserName = userName, Email = userName };

			var ir = await um.CreateAsync(user);

			var createdUser = await um.FindByNameAsync(userName);
			return createdUser;
		}

		private static async Task SetPasswordForDefaultUser(UserManager<ApplicationUser> um, string userName, ApplicationUser user)
		{

			const string password = ConstVal.AdminPassword;
			var ir = await um.AddPasswordAsync(user, password);

		}

		private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> um, string userName, string administratorRole, ApplicationUser user)
		{

			var ir = await um.AddToRoleAsync(user, administratorRole);

		}

		private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
		{
			string errors = null;
			foreach (var identityError in ir.Errors)
			{
				errors += identityError.Description;
				errors += ", ";
			}
			return errors;
		}
	}
}
