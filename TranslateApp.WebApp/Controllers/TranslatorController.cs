using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TranslateApp.Common;
using TranslateApp.Logic.Interfaces;
using TranslateApp.Logic.Services;
using TranslateApp.WebApp.Helper;
using TranslateApp.WebApp.Models.TranslateViewModels;

namespace TranslateApp.WebApp.Controllers
{
	[Authorize]
	public class TranslatorController : Controller
    {

		private readonly ITranslateServices _translateServices;
		private readonly IMapper _mapper;
		private readonly IProjectServices _projectServices;

		public TranslatorController(ITranslateServices translateServices, IMapper mapper, IProjectServices projectServices)
		{
			_projectServices = projectServices;
			_translateServices = translateServices;
			_mapper = mapper;
		}

		[HttpPost]
		public IActionResult GetAllTranslats([FromBody] string projectId)
		{
			try
			{

				List<object> translates = new List<object>();
				translates.Add(_projectServices.GetProject(int.Parse(projectId)));

				translates.Add(_translateServices.GetTranslatesByProjectId(projectId).OrderByDescending(p => p.Id));


				return Ok(translates);
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpPost]
		public IActionResult AddTranslate([FromBody] TranlsateViewModel tranlsateModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_translateServices.AddTranslate(_mapper.Map<TranlsateViewModel, ITranslateBLL>(tranlsateModel));

					return Ok();
				}
				return BadRequest();
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpPost]
		public async Task<IActionResult> DeleteTranslate([FromBody] string id)
		{
			try
			{
				await _translateServices.DeleteTranslate(int.Parse(id));

				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}
		[HttpPost]
		public IActionResult UpdateTranslate([FromBody] TranlsateViewModel tranlsateModel)
		{
			try
			{
				if (ModelState.IsValid)
				{

					var project = _projectServices.GetProject(tranlsateModel.ProjectId);

					string fromLanguage=null, toLanguage=null;

					switch (project.FromLanguage)
					{
						case "English":
							fromLanguage = "en";
							break;
						case "Polish":
							fromLanguage = "pl";
							break;
						case "German":
							fromLanguage = "de";
							break;
					}

					switch (project.ToLanguage)
					{
						case "English":
							toLanguage = "en";
							break;
						case "Polish":
							toLanguage = "pl";
							break;
						case "German":
							toLanguage = "de";
							break;
					}

					if (tranlsateModel.TranslateApi == ITranslateApi.Yandex)
					{
						string translatedText = YandexTranslateApi.Translate(tranlsateModel.TextToTranslate, fromLanguage, toLanguage);
						tranlsateModel.TranslatedText = translatedText;
					}
					else
					{
						string translatedText = GoogleTranslateApi.Translate(tranlsateModel.TextToTranslate, fromLanguage, toLanguage);
						tranlsateModel.TranslatedText = translatedText;
					}

					_translateServices.UpdateTranslate(_mapper.Map<TranlsateViewModel, ITranslateBLL>(tranlsateModel));

					return Ok(JsonConvert.SerializeObject(tranlsateModel.TranslatedText));
				}
				return BadRequest();
			}
			catch
			{
				return BadRequest();
			}
		}

	}
}