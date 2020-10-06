using AutoMapper;
using TranslateApp.Data.Models;
using TranslateApp.Logic.Models;
using TranslateApp.WebApp.Models;
using System.Collections.Generic;
using TranslateApp.Data.Interfaces;
using TranslateApp.WebApp.Models.HomeViewModels;
using TranslateApp.Logic.Interfaces;
using TranslateApp.WebApp.Models.TranslateViewModels;

namespace TranslateApp.WebApp.CommonWeb
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<ProjectDAL, ProjectBLL>();
			CreateMap<ProjectBLL, ProjectDAL>();
			CreateMap<ProjectDAL, IProjectDAL>();
			CreateMap<IProjectDAL, ProjectDAL>();
			CreateMap<IProjectBLL, ProjectViewModel>();
			CreateMap<ProjectViewModel, IProjectBLL>();

			CreateMap<TranslateDAL, TranslateBLL>();
			CreateMap<TranslateBLL, TranslateDAL>();
			CreateMap<TranslateDAL, ITranslateDAL>();
			CreateMap<ITranslateDAL, TranslateDAL>();
			CreateMap<ITranslateBLL, TranlsateViewModel>();
			CreateMap<TranlsateViewModel, ITranslateBLL>();
		}
	}
}