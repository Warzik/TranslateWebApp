using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateApp.WebApp.Models.HomeViewModels
{
	public class ProjectViewModel
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public int NumberOfTranslations { get; set; }
		public string FromLanguage { get; set; }
		public string ToLanguage { get; set; }
		public string UserId { get; set; }
		public string ImportFromProject { get; set; }
		public bool AutomaticallyTranslate { get; set; }
	}
}
