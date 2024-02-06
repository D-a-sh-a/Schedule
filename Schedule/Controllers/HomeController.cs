using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule.Data;
using Schedule.Entities;
using Schedule.Models;
using Schedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Schedule.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext dbContext;
		private readonly UserManager<DbUser> _userManager;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<DbUser> userManager)
		{
			this.dbContext = dbContext;
			_logger = logger;
			_userManager = userManager;
		}
		public IActionResult Privacy()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<IActionResult> IndexAsync()
		{

			var dbUser = await _userManager.GetUserAsync(User);
			if (User.IsInRole("Student"))
			{
				ViewBag.Role = "Student";
				if (dbUser != null)
				{
					var groupData = dbContext.Groups.FirstOrDefault(p => p.id_group == dbUser.id_group);

					var viewModel = new IndexViewModel
					{
						PIB = dbUser.PIB,
						group_name = groupData?.group_name,
						group_course = groupData?.group_course,
						group_specialty = groupData?.group_specialty,
						group_specialty_id = groupData?.group_specialty_id
					};
					return View(viewModel);
				}
			}
			if (User.IsInRole("Lecturer")|| User.IsInRole("Admin"))
			{
				ViewBag.Role = "Lecturer";
				if (dbUser != null)
				{
					var groupData = dbContext.Groups.FirstOrDefault(p => p.id_group == dbUser.id_group);

					var viewModel = new IndexViewModel
					{
						PIB = dbUser.PIB,
						Email = dbUser.Email,
					};
					return View(viewModel);
				}
			}
			return View("Error");
		}
	}
}