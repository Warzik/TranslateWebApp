using System;

namespace TranslateApp.Data.Interfaces
{
	public interface IProjectDAL : IParrentModel
	{
		string Title { get; set; }
		string Description { get; set; }
		int NumberOfTranslations { get; set; }
		string FromLanguage { get; set; }
		string ToLanguage { get; set; }
		string UserId { get; set; }
		bool AutomaticallyTranslate { get; set; }

	}
}
