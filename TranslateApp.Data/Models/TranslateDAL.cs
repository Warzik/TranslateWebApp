using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TranslateApp.Common;
using TranslateApp.Data.Interfaces;

namespace TranslateApp.Data.Models
{
	[Table("Translate")]
	public class TranslateDAL : ITranslateDAL
	{
		[Key]
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public string TextToTranslate { get; set; }
		public string TranslatedText { get; set; }
		public ITranslateApi TranslateApi { get; set; }
	}
}
