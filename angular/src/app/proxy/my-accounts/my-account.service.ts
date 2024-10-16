import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { RegisterDto, ResetPasswordDto, SendPasswordResetCodeDto } from '../volo/abp/account/models';
import type { IdentityUserDto } from '../volo/abp/identity/models';

@Injectable({
  providedIn: 'root',
})
export class MyAccountService {
  apiName = 'Default';
  

  register = (input: RegisterDto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/my-account/register',
      body: input,
    },
    { apiName: this.apiName });
  

  resetPassword = (input: ResetPasswordDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/my-account/reset-password',
      body: input,
    },
    { apiName: this.apiName });
  

  sendPasswordResetCode = (input: SendPasswordResetCodeDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/my-account/send-password-reset-code',
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
