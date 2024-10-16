using MovieManagementApp.EntityFrameworkCore;
using MovieManagementApp.UserMovieInteractions;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class UserMovieInteractionRepository : EfCoreRepository<MovieManagementAppDbContext, UserMovieInteraction, Guid>, IUserMovieInteractionRepository
{
    public UserMovieInteractionRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
