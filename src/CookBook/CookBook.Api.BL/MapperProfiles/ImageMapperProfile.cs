using AutoMapper;
using CookBook.Api.DAL.Entities;
using CookBook.Common.Models;

namespace CookBook.Api.BL.MapperProfiles;

public class ImageMapperProfile : Profile {
	public ImageMapperProfile() {
		CreateMap<ImageEntity, ImageEntity>();

		CreateMap<ImageEntity, ImageModel>()
			.ForMember(dst => dst.Data, action => action.MapFrom(src => src.Image));
		CreateMap<ImageModel, ImageEntity>()
			.ForMember(dst => dst.Image, action => action.MapFrom(src => src.Data))
			.ForMember(dst => dst.ImageB64, action => action.Ignore());
	}
}
