using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TranslateApp.Data.Interfaces;
using TranslateApp.Data.Models;
using TranslateApp.Logic.Interfaces;
using TranslateApp.Logic.Models;

namespace TranslateApp.Logic.Services
{
	public class TranslateServices : ITranslateServices
	{
		private readonly ITranslateRepository _translateRepository;
		private readonly IMapper _mapper;

		public TranslateServices(ITranslateRepository translateRepository, IMapper mapper)
		{
			_translateRepository = translateRepository;
			_mapper = mapper;
		}

		public int AddTranslate(ITranslateBLL translate)
		{
			return _translateRepository.Add(_mapper.Map<TranslateDAL>(translate));
		}

		public async Task DeleteTranslate(int id)
		{
			await _translateRepository.Delete(id);
		}

		public ITranslateBLL GetTranslate(int id)
		{
			var dalTranslats = _translateRepository.Get(id);
			if (dalTranslats == null)
				throw new ArgumentNullException("Not Found");
			return _mapper.Map<TranslateBLL>(dalTranslats);
		}

		public IEnumerable<ITranslateBLL> GetTranslatesByProjectId(string projectId)
		{
			var dalTranslats = _translateRepository.GetTranslatsByProjectId(projectId);
			var result = new List<TranslateBLL>();
			foreach (var el in dalTranslats)
				result.Add(_mapper.Map<TranslateBLL>(el));
			return result;
		}

		public void UpdateTranslate(ITranslateBLL translate)
		{
			_translateRepository.Update(_mapper.Map<TranslateDAL>(translate));
		}
	}
}
