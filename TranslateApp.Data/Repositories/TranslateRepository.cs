using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslateApp.Data.Interfaces;
using TranslateApp.Data.Models;

namespace TranslateApp.Data.Repositories
{
	public class TranslateRepository : ITranslateRepository
	{
		private readonly IMapper _mapper;
		private readonly TranslateAppDataContext _context;
		public TranslateRepository(TranslateAppDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public int Add(ITranslateDAL item)
		{
			ProjectDAL project=_context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == item.ProjectId);
			if (project != null) project.NumberOfTranslations++;
			_context.Update(project);
			var data = _context.Add(item);
			_context.SaveChanges();
			_context.Entry(item).State = EntityState.Detached;
			return data.Entity.Id;
		}

		public void DeleteByProjectId(int projectId)
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			var articles = _context.Translate.Where(p => p.ProjectId == projectId);
			foreach (var article in articles)
				_context.Translate.Remove(article);
			_context.SaveChanges();
		}

		public void AddMany(IEnumerable<ITranslateDAL> items)
		{
			throw new NotImplementedException();
		}

		public async Task Delete(int id)
		{
			TranslateDAL item = await _context.Translate.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
			ProjectDAL project = _context.Projects.AsNoTracking().FirstOrDefault(p => p.Id == item.ProjectId);
			if (project != null) project.NumberOfTranslations--;
			_context.Update(project);
			_context.Remove(item);
			_context.SaveChanges();
		}

		public ITranslateDAL Get(int id)
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Translate.FirstOrDefault(p => p.Id == id);
		}

		public IEnumerable GetAll()
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Translate.ToList();
		}

		public IEnumerable<ITranslateDAL> GetTranslatsByProjectId(string projectId)
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Translate.Where(t=>t.ProjectId==int.Parse(projectId)).OrderByDescending(x => x.Id).ToList();
		}

		public void Update(ITranslateDAL item)
		{
			_context.Update(item);
			_context.SaveChanges();
			_context.Entry(item).State = EntityState.Detached;
		}

		public int AddImport(ITranslateDAL translate)
		{
			var data = _context.Add(translate);
			_context.SaveChanges();
			_context.Entry(translate).State = EntityState.Detached;
			return data.Entity.Id;
		}
	}
}
