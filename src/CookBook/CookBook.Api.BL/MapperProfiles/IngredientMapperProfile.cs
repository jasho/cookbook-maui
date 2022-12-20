﻿using AutoMapper;
using CookBook.Api.DAL.Entities;
using CookBook.Common.Models;

namespace CookBook.Api.BL.MapperProfiles
{
    public class IngredientMapperProfile : Profile
    {
        public IngredientMapperProfile()
        {
            CreateMap<IngredientEntity, IngredientEntity>();

            CreateMap<IngredientEntity, IngredientListModel>();
            CreateMap<IngredientEntity, IngredientDetailModel>();

            CreateMap<IngredientDetailModel, IngredientEntity>();
        }
    }
}