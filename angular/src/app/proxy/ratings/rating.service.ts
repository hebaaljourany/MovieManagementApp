import type { RatingDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateRatingDto } from '../application/contracts/ratings/models';

@Injectable({
  providedIn: 'root',
})
export class RatingService {
  apiName = 'Default';
  

  create = (input: CreateUpdateRatingDto) =>
    this.restService.request<any, RatingDto>({
      method: 'POST',
      url: '/api/app/rating',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/rating/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, RatingDto>({
      method: 'GET',
      url: `/api/app/rating/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<RatingDto>>({
      method: 'GET',
      url: '/api/app/rating',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateRatingDto) =>
    this.restService.request<any, RatingDto>({
      method: 'PUT',
      url: `/api/app/rating/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
