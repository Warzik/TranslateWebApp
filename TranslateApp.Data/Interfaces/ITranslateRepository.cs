using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateApp.Data.Interfaces
{
	public interface ITranslateRepository : IRepository<ITranslateDAL>
	{
		 void DeleteByProjectId(int projectId);
		int AddImport(ITranslateDAL translate);
		IEnumerable<ITranslateDAL> GetTranslatsByProjectId(string projectId);
	}
}
