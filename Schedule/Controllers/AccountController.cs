using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schedule.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Schedule.Data;
using Schedule.Models;
using Schedule.ViewModels;
using Schedule.Entities;
using Schedule.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Schedule.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
		private readonly UserManager<DbUser> userManager;
		private readonly SignInManager<DbUser> signInManager;
		private readonly RoleManager<DbRole> roleManager;

		public AccountController(UserManager<DbUser> userManager, SignInManager<DbUser> signInManager,
			RoleManager<DbRole> roleManager, ApplicationDbContext dbContext)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
            this.dbContext = dbContext;
    }
        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();
            viewModel.group_names = dbContext.Groups.Select(e => e.group_name).ToList();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel Model)
        {
            if (ModelState.IsValid)
            {
                var group = dbContext.Groups.FirstOrDefault(p => p.group_name == Model.group_name && p.group_course == Model.group_course);

                if(group == null)
                {
                    ViewBag.Error = $"Групи {Model.group_name} на {Model.group_course} курсі не існує";
                    return View("Error");
                }

                var user = new DbUser()
                {
                    UserName = Model.email,
                    Email = Model.email,
                     id_group = group.id_group,
                      PIB = Model.PIB,
                };

                var result = await userManager.CreateAsync(user, Model.password);
                if (result.Succeeded)
                {
                    if(Model.group_course == 0)
                        await userManager.AddToRoleAsync(user, "Lecturer");
                    else
                        await userManager.AddToRoleAsync(user, "Student");
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.Error = "Model is invalid";
            return View("Error");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPostAsync(LoginViewModel Model)
        {
            if (!string.IsNullOrEmpty(Model.Email) && !string.IsNullOrEmpty(Model.Password))
            {
                var identityResult = await signInManager.PasswordSignInAsync(Model.Email, Model.Password, true, false);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }
            }
			ModelState.AddModelError(string.Empty, "Невірний email або пароль");
            return View("Login", Model);
        }

    }
}
