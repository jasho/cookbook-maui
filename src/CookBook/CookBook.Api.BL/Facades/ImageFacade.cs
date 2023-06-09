using AutoMapper;
using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class ImageFacade : IImageFacade {
	private readonly IImageRepository imageRepository;
	private readonly IMapper mapper;

	public ImageFacade(
		IImageRepository imageRepository,
		IMapper mapper) {
		this.imageRepository = imageRepository;
		this.mapper = mapper;
	}

	public ImageModel? GetById(Guid id) {
		var imageEntity = imageRepository.GetById(id);
		return mapper.Map<ImageModel>(imageEntity);
	}

	public Guid Create(ImageModel imageModel) {
		var imageEntity = mapper.Map<ImageEntity>(imageModel);
		return imageRepository.Insert(imageEntity);
	}

	public Guid? Update(ImageModel imageModel) {
		var imageEntity = mapper.Map<ImageEntity>(imageModel);
		return imageRepository.Update(imageEntity);
	}

	public void Delete(Guid id) {
		imageRepository.Remove(id);
	}
}
