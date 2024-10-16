using AutoMapper;
using MovieManagementApp.MyAccounts;
using MovieManagementApp.Actors;
using MovieManagementApp.Application.Contracts.MyAccounts;
using MovieManagementApp.Application.Contracts.Actors;
using MovieManagementApp.Application.Contracts.Categories;
using MovieManagementApp.Application.Contracts.Movies;
using MovieManagementApp.Application.Contracts.MyLists;
using MovieManagementApp.Application.Contracts.Ratings;
using MovieManagementApp.Application.Contracts.UserMovieInteractions;
using MovieManagementApp.Categories;
using MovieManagementApp.MovieActors;
using MovieManagementApp.MovieCategories;
using MovieManagementApp.Movies;
using MovieManagementApp.MyLists;
using MovieManagementApp.Ratings;
using MovieManagementApp.UserMovieInteractions;
using System.Linq;

namespace MovieManagementApp;

public class MovieManagementAppApplicationAutoMapperProfile : Profile
{
    public MovieManagementAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<MyAccount, MyAccountDto>();
        CreateMap<CreateUpdateMyAccountDto, MyAccount>();
        CreateMap<Actor, ActorDto>();
        CreateMap<CreateUpdateActorDto, Actor>();
        CreateMap<Movie, MovieDto>();
        CreateMap<CreateUpdateMovieDto, Movie>();
        CreateMap<MovieActor, MovieActorDto>();
        CreateMap<MovieCategory, MovieCategoryDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateUpdateCategoryDto, Category>();
        CreateMap<MyList, MyListDto>();
        CreateMap<CreateUpdateMyListDto, MyList>();
        CreateMap<Rating, RatingDto>();
        CreateMap<CreateUpdateRatingDto, Rating>();
        CreateMap<UserMovieInteraction, UserMovieInteractionDto>();
        CreateMap<CreateUpdateUserMovieInteractionDto, UserMovieInteraction>();
        CreateMap<Actor, ActorLookupDto>();
        CreateMap<Category, CategoryLookupDto>();



    }
}
