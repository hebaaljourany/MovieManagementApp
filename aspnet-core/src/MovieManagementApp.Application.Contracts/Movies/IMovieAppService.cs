using MovieManagementApp.Movies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MovieManagementApp.Application.Contracts.Movies
{
    public interface IMovieAppService :
        ICrudAppService< // Defines CRUD methods
            MovieDto, // Used to show movies
            Guid, // Primary key of the movie entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateMovieDto> // Used to create/update a movie
    {
        // تعديل دالة GetActorLookupAsync لتقبل معامل searchTerm
        Task<ListResultDto<ActorLookupDto>> GetActorLookupAsync(string searchTerm );

        // تعديل دالة GetCategoryLookupAsync لتقبل معامل searchTerm
        Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync(string searchTerm );
    }
}
