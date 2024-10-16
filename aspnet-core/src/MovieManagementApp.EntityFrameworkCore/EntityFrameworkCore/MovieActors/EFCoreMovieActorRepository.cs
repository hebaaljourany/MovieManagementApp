using MovieManagementApp.MovieActors;
using MovieManagementApp.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class MovieActorRepository : EfCoreRepository<MovieManagementAppDbContext, MovieActor, Guid>, IMovieActorRepository
{
    public MovieActorRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}