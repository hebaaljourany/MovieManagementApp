using MovieManagementApp.Categories;
using MovieManagementApp.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class CategoryRepository : EfCoreRepository<MovieManagementAppDbContext, Category, Guid>, IMovieCategoryRepository
{
    public CategoryRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}