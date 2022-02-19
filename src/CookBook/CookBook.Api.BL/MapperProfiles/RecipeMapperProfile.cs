using AutoMapper;
using CookBook.Api.Entities;
using CookBook.Common.Models;

namespace CookBook.Api.MapperProfiles
{
    public class RecipeMapperProfile : Profile
    {
        public RecipeMapperProfile()
        {
            CreateMap<RecipeEntity, RecipeEntity>();

            CreateMap<RecipeEntity, RecipeListModel>();
            CreateMap<RecipeEntity, RecipeDetailModel>();
            CreateMap<IngredientAmountEntity, RecipeDetailIngredientModel>();

            CreateMap<RecipeDetailModel, RecipeEntity>()
                .ForMember(dst => dst.IngredientAmounts, action => action.Ignore());
        }
    }
}