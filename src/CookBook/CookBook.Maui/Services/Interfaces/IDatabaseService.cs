﻿using CookBook.Maui.Entities;

namespace CookBook.Maui.Services.Interfaces;

public interface IDatabaseService
{
    Task<List<T>> GetAllAsync<T>()
        where T : new();

    Task<T?> GetByIdAsync<T>(Guid id)
        where T : EntityBase, new();

    Task<Guid> CreateOrUpdateAsync<T>(T entity)
        where T : EntityBase, new();

    Task DeleteAsync<T>(Guid id);

    void InitializeDatabase();
}