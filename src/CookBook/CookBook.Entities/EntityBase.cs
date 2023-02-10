using System;
using SQLite;

namespace CookBook.Entities;

public abstract record EntityBase {
    [PrimaryKey]
    public Guid Id { get; init; } = Guid.NewGuid();
}