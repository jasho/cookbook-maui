﻿using CookBook.Maui.Entities;

namespace CookBook.Maui.Services.Interfaces;

public interface IDatabaseService
{
    Task<List<T>> GetAllAsync<T>()
        where T : new();

    Task<T> GetByIdAsync<T>(Guid id)
        where T : EntityBase, new();

    Task SetAsync<T>(T entity)
        where T : EntityBase, new();

    Task CreateDatabaseAsync();
}