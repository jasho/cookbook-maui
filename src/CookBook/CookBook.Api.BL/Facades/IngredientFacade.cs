using AutoMapper;
using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Api.DAL.Entities;
using CookBook.Api.DAL.Repositories.Interfaces;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public class IngredientFacade : IIngredientFacade
{
    private readonly IIngredientRepository ingredientRepository;
    private readonly IMapper mapper;

    public IngredientFacade(
        IIngredientRepository ingredientRepository,
        IMapper mapper)
    {
        this.ingredientRepository = ingredientRepository;
        this.mapper = mapper;
    }

    public List<IngredientListModel> GetAll()
    {
        return mapper.Map<List<IngredientListModel>>(ingredientRepository.GetAll());
    }

    public IngredientDetailModel? GetById(Guid id)
    {
        var ingredientEntity = ingredientRepository.GetById(id);
        return mapper.Map<IngredientDetailModel>(ingredientEntity);
    }


    public List<IngredientDetailModel> GetByRecipeId(Guid id)
    {
        var ingredientEntity = ingredientRepository.GetByRecipeId(id);
        return mapper.Map<List<IngredientDetailModel>>(ingredientEntity);
    }

    public Guid Create(IngredientDetailModel ingredientModel)
    {
        var ingredientEntity = mapper.Map<IngredientEntity>(ingredientModel);
        return ingredientRepository.Insert(ingredientEntity);
    }

    public Guid? Update(IngredientDetailModel ingredientModel)
    {
        var ingredientEntity = mapper.Map<IngredientEntity>(ingredientModel);
        return ingredientRepository.Update(ingredientEntity);
    }

    public void Delete(Guid id)
    {
        ingredientRepository.Remove(id);
    }
}