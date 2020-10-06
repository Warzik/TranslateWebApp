using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TranslateApp.Common;
using TranslateApp.Data;
using TranslateApp.Data.Interfaces;
using TranslateApp.Data.Repositories;
using TranslateApp.Logic.Interfaces;
using TranslateApp.Logic.Services;
using TranslateApp.WebApp.Data;
using TranslateApp.WebApp.Models;
using TranslateApp.WebApp.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TranslateApp.WebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddAutoMapper();

			services.AddCors(options =>
			{
				options.AddPolicy("EnableCORS", builder =>
				{
					builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
				});
			});

			services
			.AddAuthentication(options =>
			{
				options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;

			})
			.AddGoogle("Google", googleoptions =>
			{
				googleoptions.CallbackPath = new PathString("/signin-google");
				googleoptions.ClientId = Configuration["Authentication:Google:ClientId"];
				googleoptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
			})
			.AddFacebook("Facebook", facebookOptions =>
			{
				facebookOptions.CallbackPath = new PathString("/signin-facebook");
				facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
				facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
			}); 
			

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 6;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;

			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

				// If the LoginPath isn't set, ASP.NET Core defaults 
				// the path to /Account/Login.
				options.LoginPath = "/account/login";


				// If the AccessDeniedPath isn't set, ASP.NET Core defaults 
				// the path to /Account/AccessDenied.
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.SlidingExpiration = true;
			});


			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(ConstDbString.ConnectionStringDb))
			.AddDbContext<TranslateAppDataContext>(options =>
				options.UseSqlServer(ConstDbString.ConnectionStringDb));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();


			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<IProjectRepository, ProjectRepository>();
			services.AddTransient<IProjectServices, ProjectServices>();
			services.AddTransient<ITranslateRepository, TranslateRepository>();
			services.AddTransient<ITranslateServices, TranslateServices>();

			services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(60);
				options.Cookie.HttpOnly = true;
			});

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseCors("EnableCORS");

			app.UseStaticFiles();

			app.UseSpaStaticFiles();

			app.UseSession();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});


			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
