using System;
using System.Collections.Generic;
using System.Text;
using TranslateApp.Common;

namespace TranslateApp.Logic.Interfaces
{
	public interface ITranslateBLL
	{
		int Id { get; set; }
		int ProjectId { get; set; }
		string TextToTranslate { get; set; }
		string TranslatedText { get; set; }
		ITranslateApi TranslateApi { get; set; }
	}
}
