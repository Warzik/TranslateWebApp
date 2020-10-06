using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslateApp.Common;

namespace TranslateApp.WebApp.Models.TranslateViewModels
{
	public class TranlsateViewModel
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public string TextToTranslate { get; set; }
		public string TranslatedText { get; set; }
		public ITranslateApi TranslateApi { get; set; }
	}
}
