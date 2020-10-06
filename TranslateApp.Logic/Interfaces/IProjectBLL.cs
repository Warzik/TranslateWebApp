using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateApp.Logic.Interfaces
{
	public interface IProjectBLL
	{
		int Id { get; set; }
		string Title { get; set; }
		string Description { get; set; }
		int NumberOfTranslations { get; set; }
		string FromLanguage { get; set; }
		string ToLanguage { get; set; }
		string UserId { get; set; }
		bool AutomaticallyTranslate { get; set; }
	}
}
