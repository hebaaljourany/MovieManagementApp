using MovieManagementApp.EntityFrameworkCore;
using MovieManagementApp.Movies;
using MovieManagementApp.MyLists;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

public class MyListRepository : EfCoreRepository<MovieManagementAppDbContext, MyList, Guid>, IMyListRepository
{
    public MyListRepository(IDbContextProvider<MovieManagementAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}