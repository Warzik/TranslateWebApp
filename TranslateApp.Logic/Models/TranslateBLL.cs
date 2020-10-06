using System;
using System.Collections.Generic;
using System.Text;
using TranslateApp.Common;
using TranslateApp.Logic.Interfaces;

namespace TranslateApp.Logic.Models
{
	public class TranslateBLL : ITranslateBLL
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public string TextToTranslate { get; set; }
		public string TranslatedText { get; set; }
		public ITranslateApi TranslateApi { get; set; }
	}
}
