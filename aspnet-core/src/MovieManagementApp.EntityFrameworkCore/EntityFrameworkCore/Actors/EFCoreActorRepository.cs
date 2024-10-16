using MovieManagementApp.Actors;
using MovieManagementApp.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class ActorRepository : EfCoreRepository<MovieManagementAppDbContext, Actor, Guid>, IMovieActorRepository
{
    public ActorRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}