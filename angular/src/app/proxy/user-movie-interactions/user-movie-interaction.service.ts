import type { UserMovieInteractionDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateUserMovieInteractionDto } from '../application/contracts/user-movie-interactions/models';

@Injectable({
  providedIn: 'root',
})
export class UserMovieInteractionService {
  apiName = 'Default';
  

  create = (input: CreateUpdateUserMovieInteractionDto) =>
    this.restService.request<any, UserMovieInteractionDto>({
      method: 'POST',
      url: '/api/app/user-movie-interaction',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/user-movie-interaction/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, UserMovieInteractionDto>({
      method: 'GET',
      url: `/api/app/user-movie-interaction/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<UserMovieInteractionDto>>({
      method: 'GET',
      url: '/api/app/user-movie-interaction',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateUserMovieInteractionDto) =>
    this.restService.request<any, UserMovieInteractionDto>({
      method: 'PUT',
      url: `/api/app/user-movie-interaction/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
