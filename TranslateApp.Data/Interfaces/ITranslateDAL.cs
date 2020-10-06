using System;
using System.Collections.Generic;
using System.Text;
using TranslateApp.Common;

namespace TranslateApp.Data.Interfaces
{
	public interface ITranslateDAL
	{
		int Id { get; set; }
		int ProjectId { get; set; }
		string TextToTranslate { get; set; }
		string TranslatedText { get; set; }
		ITranslateApi TranslateApi { get; set; }
	}
}
