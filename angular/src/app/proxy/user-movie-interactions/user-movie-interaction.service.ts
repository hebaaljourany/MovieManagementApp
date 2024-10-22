import type { UserMovieInteractionDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateUserMovieInteractionDto } from '../application/contracts/user-movie-interactions/models';

@Injectable({
  providedIn: 'root',
})
export class UserMovieInteractionService {
  apiName = 'Default';
  

  create = (input: CreateUpdateUserMovieInteractionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UserMovieInteractionDto>({
      method: 'POST',
      url: '/api/app/user-movie-interaction',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/user-movie-interaction/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UserMovieInteractionDto>({
      method: 'GET',
      url: `/api/app/user-movie-interaction/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<UserMovieInteractionDto>>({
      method: 'GET',
      url: '/api/app/user-movie-interaction',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateUserMovieInteractionDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UserMovieInteractionDto>({
      method: 'PUT',
      url: `/api/app/user-movie-interaction/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
