import type { ActorLookupDto, CategoryLookupDto, GetMovieInputDto, MovieDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
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
  

  getBytes = () =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/movie/bytes',
    },
    { apiName: this.apiName });
  

  getCategoryLookup = (searchTerm: string) =>
    this.restService.request<any, ListResultDto<CategoryLookupDto>>({
      method: 'GET',
      url: '/api/app/movie/category-lookup',
      params: { searchTerm },
    },
    { apiName: this.apiName });
  

  getList = (input: GetMovieInputDto) =>
    this.restService.request<any, PagedResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/app/movie',
      params: { filter: input.filter, actorId: input.actorId, categoryId: input.categoryId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getMyList = (input: GetMovieInputDto) =>
    this.restService.request<any, PagedResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/app/movie/my-list',
      params: { filter: input.filter, actorId: input.actorId, categoryId: input.categoryId, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
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
  

  removeMovieFromUserList = (movieId: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/movie/movie-from-user-list/${movieId}`,
    },
    { apiName: this.apiName });
  

  streamVideoByBlobName = (blobName: string) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/stream-video',
      params: { blobName },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateMovieDto) =>
    this.restService.request<any, MovieDto>({
      method: 'PUT',
      url: `/api/app/movie/${id}`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
