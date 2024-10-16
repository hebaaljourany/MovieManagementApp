using MovieManagementApp.EntityFrameworkCore;
using MovieManagementApp.Ratings;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class RatingRepository : EfCoreRepository<MovieManagementAppDbContext, Rating, Guid>, IRatingRepository
{
    public RatingRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
