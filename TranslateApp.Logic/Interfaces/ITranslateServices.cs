using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TranslateApp.Logic.Interfaces
{
	public interface ITranslateServices
	{
		Task DeleteTranslate(int id);
		ITranslateBLL GetTranslate(int id);
		IEnumerable<ITranslateBLL> GetTranslatesByProjectId(string projectId);
		int AddTranslate(ITranslateBLL translate);
		void UpdateTranslate(ITranslateBLL translate);
	}
}
