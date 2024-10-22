import type { ActorLookupDto, CategoryLookupDto, MovieDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateMovieDto } from '../application/contracts/movies/models';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  apiName = 'Default';
  

  addToMyList = (movieId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/movie/to-my-list/${movieId}`,
    },
    { apiName: this.apiName,...config });
  

  calculateAverageRating = (movieId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number>({
      method: 'POST',
      url: `/api/app/movie/calculate-average-rating/${movieId}`,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateUpdateMovieDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'POST',
      url: '/api/app/movie',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/movie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: `/api/app/movie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getActorLookup = (searchTerm: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<ActorLookupDto>>({
      method: 'GET',
      url: '/api/app/movie/actor-lookup',
      params: { searchTerm },
    },
    { apiName: this.apiName,...config });
  

  getCategoryLookup = (searchTerm: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<CategoryLookupDto>>({
      method: 'GET',
      url: '/api/app/movie/category-lookup',
      params: { searchTerm },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/app/movie',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getTotalDownloads = (movieId: string, from?: string, to?: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: `/api/app/movie/total-downloads/${movieId}`,
      params: { from, to },
    },
    { apiName: this.apiName,...config });
  

  getTotalViews = (movieId: string, from?: string, to?: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: `/api/app/movie/total-views/${movieId}`,
      params: { from, to },
    },
    { apiName: this.apiName,...config });
  

  getUserRating = (movieId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: `/api/app/movie/user-rating/${movieId}`,
    },
    { apiName: this.apiName,...config });
  

  isInMyList = (movieId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/movie/is-in-my-list/${movieId}`,
    },
    { apiName: this.apiName,...config });
  

  isWatchedOrDownloaded = (movieId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/movie/is-watched-or-downloaded/${movieId}`,
    },
    { apiName: this.apiName,...config });
  

  rateMovie = (movieId: string, ratingValue: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/movie/rate-movie/${movieId}`,
      params: { ratingValue },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateMovieDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'PUT',
      url: `/api/app/movie/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
