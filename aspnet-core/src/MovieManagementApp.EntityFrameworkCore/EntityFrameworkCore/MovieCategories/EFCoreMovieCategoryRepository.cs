using MovieManagementApp.MovieCategories;
using MovieManagementApp.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class MovieCategoryRepository : EfCoreRepository<MovieManagementAppDbContext, MovieCategory, Guid>, IMovieCategoryRepository
{
    public MovieCategoryRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}