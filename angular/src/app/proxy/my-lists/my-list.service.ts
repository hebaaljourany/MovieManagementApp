import type { MyListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateMyListDto } from '../application/contracts/my-lists/models';

@Injectable({
  providedIn: 'root',
})
export class MyListService {
  apiName = 'Default';
  

  create = (input: CreateUpdateMyListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MyListDto>({
      method: 'POST',
      url: '/api/app/my-list',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/my-list/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MyListDto>({
      method: 'GET',
      url: `/api/app/my-list/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MyListDto>>({
      method: 'GET',
      url: '/api/app/my-list',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateMyListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MyListDto>({
      method: 'PUT',
      url: `/api/app/my-list/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
