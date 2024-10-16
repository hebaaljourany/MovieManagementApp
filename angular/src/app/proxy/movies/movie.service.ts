import type { ActorLookupDto, CategoryLookupDto, MovieDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateMovieDto } from '../application/contracts/movies/models';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  apiName = 'Default';
  

  addToMyList = (movieId: string) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/movie/to-my-list/${movieId}`,
    },
    { apiName: this.apiName });
  

  calculateAverageRating = (movieId: string) =>
    this.restService.request<any, number>({
      method: 'POST',
      url: `/api/app/movie/calculate-average-rating/${movieId}`,
    },
    { apiName: this.apiName });
  

  create = (input: CreateUpdateMovieDto) =>
    this.restService.request<any, MovieDto>({
      method: 'POST',
      url: '/api/app/movie',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/movie/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: `/api/app/movie/${id}`,
    },
    { apiName: this.apiName });
  

  getActorLookup = (searchTerm: string) =>
    this.restService.request<any, ListResultDto<ActorLookupDto>>({
      method: 'GET',
      url: '/api/app/movie/actor-lookup',
      params: { searchTerm },
    },
    { apiName: this.apiName });
  

  getCategoryLookup = (searchTerm: string) =>
    this.restService.request<any, ListResultDto<CategoryLookupDto>>({
      method: 'GET',
      url: '/api/app/movie/category-lookup',
      params: { searchTerm },
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/app/movie',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  getTotalDownloads = (movieId: string, from?: string, to?: string) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: `/api/app/movie/total-downloads/${movieId}`,
      params: { from, to },
    },
    { apiName: this.apiName });
  

  getTotalViews = (movieId: string, from?: string, to?: string) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: `/api/app/movie/total-views/${movieId}`,
      params: { from, to },
    },
    { apiName: this.apiName });
  

  getUserRating = (movieId: string) =>
    this.restService.request<any, number>({
      method: 'GET',
      url: `/api/app/movie/user-rating/${movieId}`,
    },
    { apiName: this.apiName });
  

  isInMyList = (movieId: string) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/movie/is-in-my-list/${movieId}`,
    },
    { apiName: this.apiName });
  

  isWatchedOrDownloaded = (movieId: string) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: `/api/app/movie/is-watched-or-downloaded/${movieId}`,
    },
    { apiName: this.apiName });
  

  rateMovie = (movieId: string, ratingValue: number) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/movie/rate-movie/${movieId}`,
      params: { ratingValue },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateMovieDto) =>
    this.restService.request<any, MovieDto>({
      method: 'PUT',
      url: `/api/app/movie/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
