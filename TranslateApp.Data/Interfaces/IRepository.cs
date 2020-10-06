using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TranslateApp.Data.Interfaces
{
	public interface IRepository<T> where T : class
	{
		int Add(T item);
		Task Delete(int id);
		void Update(T item);
		T Get(int id);
		IEnumerable GetAll();
		void AddMany(IEnumerable<T> items);
	}
}
