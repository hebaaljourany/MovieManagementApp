using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MovieManagementApp.Actors
{
    public interface IActorRepository : IRepository<Actor, Guid>
    {

    }
}
