using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace TranslateApp.WebApp.Helper
{
    public static class YandexTranslateApi
    {
        public static string Translate(string textToTranslate, string fromLanguage, string toLanguage)
        {
            if (textToTranslate.Length > 0)
            {

                string lang = fromLanguage + "-" + toLanguage;

                WebRequest request = WebRequest.Create("https://translate.yandex.net/api/v1.5/tr.json/translate?"
                    + "key=***"
                    + "&text=" + textToTranslate
                    + "&lang=" + lang);

                WebResponse response = request.GetResponse();

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    string line;

                    if ((line = stream.ReadLine()) != null)
                    {
                        Translation translation = JsonConvert.DeserializeObject<Translation>(line);

                        textToTranslate = "";

                        foreach (string str in translation.text)
                        {
                            textToTranslate += str;
                        }
                    }
                }

                return textToTranslate;
            }
            else
            {
                return "";
            }
        }
    }

    internal class Translation
    {
        public string code { get; set; }
        public string lang { get; set; }
        public string[] text { get; set; }
    }
}
