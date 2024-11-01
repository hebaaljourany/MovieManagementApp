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
    updateMovie = (id: string, input: FormData, config?: Partial<Rest.Config>) =>
      this.restService.request<any, string>({
        method: 'PUT',
        responseType: 'text',

        url: `/api/app/movie/${id}`,
        body: input,
      },
      { apiName: this.apiName,...config  });
      createActor = (input: FormData, config?: Partial<Rest.Config>) =>
        this.restService.request<any, string>({
          method: 'POST',
          responseType: 'text',
          url: '/api/app/actor',
          body: input,
        },
        { apiName: this.apiName,...config });
        updateActor = (id: string, input: FormData, config?: Partial<Rest.Config>) =>
          this.restService.request<any, string>({
            method: 'PUT',
            responseType: 'text',
            url: `/api/app/actor/${id}`,
            body: input,
          },
          { apiName: this.apiName,...config  });
}