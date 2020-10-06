using TranslateApp.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TranslateApp.Data.Models
{
	[Table("Project")]
	public class ProjectDAL : IProjectDAL
	{
		[Key]
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
