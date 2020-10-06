using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TranslateApp.Data.Interfaces;
using TranslateApp.Data.Models;
using TranslateApp.Logic.Interfaces;
using TranslateApp.Logic.Models;

namespace TranslateApp.Logic.Services
{
	public class ProjectServices : IProjectServices
	{
	    private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
		private readonly ITranslateRepository _translateRepository;
		public ProjectServices(IProjectRepository projectRepository, ITranslateRepository translateRepository, IMapper mapper)
	    {
			_translateRepository = translateRepository;
			_projectRepository = projectRepository;
			_mapper = mapper;
		}

		public int AddProject(IProjectBLL project)
		{
			return _projectRepository.Add(_mapper.Map<ProjectDAL>(project));
		}

		public async Task DeleteProject(int id)
		{
			_translateRepository.DeleteByProjectId(id);
			await _projectRepository.Delete(id);
		}

		public IEnumerable<IProjectBLL> GetAllProjects()
		{
			var dalProjects = _projectRepository.GetAll();
			var result = new List<ProjectBLL>();
			foreach (var el in dalProjects)
				result.Add(_mapper.Map<ProjectBLL>(el));
			return result;
		}

		public IProjectBLL GetProject(int id)
		{
			var dalProject = _projectRepository.Get(id);
			if (dalProject == null)
				throw new ArgumentNullException("Not Found");
			return _mapper.Map<ProjectBLL>(dalProject);

		}

		public IProjectBLL GetProjectByTitle(string userId, string title)
		{
			var dalProject = _projectRepository.GetByTitle(userId,title);
			return _mapper.Map<ProjectBLL>(dalProject);
		}

		public IEnumerable<IProjectBLL> GetProjectsByUserId(string userId)
		{
			if (userId == null)
				throw new ArgumentNullException("Null");
			var dalProjects = _projectRepository.GetProjectsByUserId(userId);
			var result = new List<ProjectBLL>();
			foreach (var el in dalProjects)
				result.Add(_mapper.Map<ProjectBLL>(el));
			return result;
		}

		public int ImportTranslatesToProject(string userId, int projectId, string importProjectTitle)
		{
			var projectImport=_projectRepository.GetByTitle(userId,importProjectTitle);
			var projectImportTranslates=_translateRepository.GetTranslatsByProjectId(projectImport.Id.ToString()).OrderBy(t=>t.Id);
			int count = 0;
			foreach (var translate in projectImportTranslates)
			{
				count++;

				_translateRepository.AddImport(new TranslateDAL() { TextToTranslate = translate.TranslatedText,
					TranslateApi = translate.TranslateApi , ProjectId = projectId });
			}
			return count;
		}

		public void UpdateProject(IProjectBLL project)
		{
			_projectRepository.Update(_mapper.Map<ProjectDAL>(project));
		}
	}
}
