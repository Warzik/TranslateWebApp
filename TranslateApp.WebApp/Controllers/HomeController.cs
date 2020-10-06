using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslateApp.Logic.Interfaces;
using TranslateApp.WebApp.Models.HomeViewModels;

namespace TranslateApp.WebApp.Controllers
{
	[Authorize]
	public class HomeController : Controller
    {

		private readonly IProjectServices _projectServices;
		private readonly IMapper _mapper;

		public HomeController(IProjectServices projectServices, IMapper mapper)
		{
			_projectServices = projectServices;
			_mapper = mapper;
		}

		[HttpPost]
		public IActionResult GetAllProjects()
        {
			try
			{
				string UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				return Ok(_projectServices.GetProjectsByUserId(UserId).OrderByDescending(p=>p.Id));
			}
			catch {
				return BadRequest();
			}
        }
		[HttpPost]
		public IActionResult AddProject([FromBody] ProjectViewModel project)
		{
			try
			{
				if (ModelState.IsValid)
				{

					string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

					if (_projectServices.GetProjectByTitle(userId,project.Title) != null)
					{
						ModelState.AddModelError(string.Empty, "Podany tytuł jest zajęty");

						return BadRequest(ModelState);
					}
					project.UserId = userId;	
					
					int projectId=_projectServices.AddProject(_mapper.Map<ProjectViewModel, IProjectBLL>(project));

					if (project.ImportFromProject != null)
					{
						int countOfTranslets = _projectServices.ImportTranslatesToProject(userId,projectId, project.ImportFromProject);

						project.Id = projectId;
						project.NumberOfTranslations += countOfTranslets;

						_projectServices.UpdateProject(_mapper.Map<ProjectViewModel, IProjectBLL>(project));
					}

					return Ok();
				}
				return BadRequest();
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		[HttpPost]
		public async Task<IActionResult> DeleteProject([FromBody] string id)
		{
			try
			{
				await _projectServices.DeleteProject(int.Parse(id));

				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost]
		public IActionResult UpdateProject([FromBody] ProjectViewModel project)
		{
			try
			{
				if (ModelState.IsValid)
				{

					string userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

					var projectByTitle = _projectServices.GetProjectByTitle(userId,project.Title);

					var projectById = _projectServices.GetProject(project.Id);

					if (projectByTitle!=null && projectByTitle.Title!= projectById.Title)
					{
						ModelState.AddModelError(string.Empty, "Podany tytuł jest zajęty");

						return BadRequest(ModelState);
					}

					project.UserId = userId;


					if (project.ImportFromProject != null)
					{
						int countOfTranslets = _projectServices.ImportTranslatesToProject(userId,project.Id, project.ImportFromProject);

						project.NumberOfTranslations += countOfTranslets;
					}

					_projectServices.UpdateProject(_mapper.Map<ProjectViewModel, IProjectBLL>(project));

					return Ok();
				}
				return BadRequest();
			}
			catch(Exception e)
			{
				return BadRequest();
			}
		}

	}
}