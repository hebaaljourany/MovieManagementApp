import type { ActorDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateActorDto } from '../application/contracts/actors/models';

@Injectable({
  providedIn: 'root',
})
export class ActorService {
  apiName = 'Default';
  

  create = (input: CreateUpdateActorDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ActorDto>({
      method: 'POST',
      url: '/api/app/actor',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/actor/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ActorDto>({
      method: 'GET',
      url: `/api/app/actor/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ActorDto>>({
      method: 'GET',
      url: '/api/app/actor',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateActorDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ActorDto>({
      method: 'PUT',
      url: `/api/app/actor/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
