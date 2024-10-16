import type { MyListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateMyListDto } from '../application/contracts/my-lists/models';

@Injectable({
  providedIn: 'root',
})
export class MyListService {
  apiName = 'Default';
  

  create = (input: CreateUpdateMyListDto) =>
    this.restService.request<any, MyListDto>({
      method: 'POST',
      url: '/api/app/my-list',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/my-list/${id}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, MyListDto>({
      method: 'GET',
      url: `/api/app/my-list/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<MyListDto>>({
      method: 'GET',
      url: '/api/app/my-list',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateMyListDto) =>
    this.restService.request<any, MyListDto>({
      method: 'PUT',
      url: `/api/app/my-list/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
