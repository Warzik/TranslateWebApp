using TranslateApp.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateApp.Logic.Models
{
	public class ProjectBLL : IProjectBLL
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int NumberOfTranslations { get; set; }
		public string FromLanguage { get; set; }
		public string ToLanguage { get; set; }
		public string UserId { get; set; }
		public bool AutomaticallyTranslate { get; set; }
	}
}
