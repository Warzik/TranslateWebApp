
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TranslateApp.Data.Interfaces
{
	public interface IProjectRepository : IRepository<IProjectDAL>
	{
		IEnumerable<IProjectDAL> GetProjectsByUserId(string userId);
		IProjectDAL GetByTitle(string userId,string title);
	}
}
