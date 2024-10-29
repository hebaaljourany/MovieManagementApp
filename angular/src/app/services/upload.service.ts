import { Injectable } from '@angular/core';
import { Rest, RestService } from '@abp/ng.core';
import { Observable } from 'rxjs';
import { CreateUpdateMovieDto } from '@proxy/application/contracts/movies';

@Injectable({
  providedIn: 'root',
})
export class FileUploadService {
  private apiName = 'App.Storage';

  constructor(private restService: RestService) {}

  createMovie = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/movie',
      body: input,
    },
    { apiName: this.apiName,...config });

}