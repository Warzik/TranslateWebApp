using System;
using System.Net;
using System.Web;

namespace TranslateApp.WebApp.Helper
{
	static public class GoogleTranslateApi
	{
		static public  string Translate(string input, string fromLanguage, string toLanguage)
		{
			string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(input)}";
			WebClient webClient = new WebClient
			{
				Encoding = System.Text.Encoding.Default
			};
			string result = webClient.DownloadString(url);
			result = result.Remove(result.Length - 2);
			try
			{

				int startIndex = result.IndexOf('"') + 1;
				int endIndex = result.IndexOf('"', startIndex);
				int length = endIndex - startIndex;

				string translatedText = null;

				while (endIndex > -1)
				{
					translatedText += result.Substring(startIndex, length);

					for (int i = 0; i < 4; i++)
					{
						startIndex = endIndex + 1;

						endIndex = result.IndexOf('"', startIndex);
					}

					length = endIndex - startIndex;

				}


				return translatedText;
			}
			catch
			{
				return "Error";
			}
		}
	}
}
