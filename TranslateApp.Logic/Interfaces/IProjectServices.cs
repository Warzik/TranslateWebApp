using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TranslateApp.Data.Interfaces;
using TranslateApp.Logic.Models;

namespace TranslateApp.Logic.Interfaces
{
	public interface IProjectServices
	{
		Task DeleteProject(int id);
		IProjectBLL GetProject(int id);
		IEnumerable<IProjectBLL> GetProjectsByUserId(string userId);
		IEnumerable<IProjectBLL> GetAllProjects();
		IProjectBLL GetProjectByTitle(string userId,string title);
		int AddProject(IProjectBLL project);
		void UpdateProject(IProjectBLL project);
		int ImportTranslatesToProject(string userId,int projectId, string importProjectTitle);
	}
}
