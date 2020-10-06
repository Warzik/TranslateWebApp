using Microsoft.EntityFrameworkCore;
using TranslateApp.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslateApp.Data.Models;
using AutoMapper;

namespace TranslateApp.Data.Repositories
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly IMapper _mapper;
		private readonly TranslateAppDataContext _context;
		public ProjectRepository(TranslateAppDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public int Add(IProjectDAL item)
		{
			var data = _context.Add(item);
			_context.SaveChanges();
			_context.Entry(item).State = EntityState.Detached;
			return data.Entity.Id;
		}

		public void AddMany(IEnumerable<IProjectDAL> items)
		{
			throw new NotImplementedException();
		}

		public async Task Delete(int id)
		{
			ProjectDAL item = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
			_context.Remove(item);
			_context.SaveChanges();
		}

		public IProjectDAL Get(int id)
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Projects.FirstOrDefault(p => p.Id == id);
		}

		public IEnumerable GetAll()
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Projects.ToList();
		}

		public IProjectDAL GetByTitle(string userId,string title)
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Projects.FirstOrDefault(p => p.Title == title && p.UserId==userId);
		}

		public IEnumerable<IProjectDAL> GetProjectsByUserId(string userId)
		{
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return _context.Set<ProjectDAL>().Where(p => p.UserId == userId).ToList();
		}

		public void Update(IProjectDAL item)
		{
			_context.Update(item);
			_context.SaveChanges();
			_context.Entry(item).State = EntityState.Detached;
		}
	}
}
