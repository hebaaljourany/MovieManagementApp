import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'MovieManagementApp',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44350/',
    redirectUri: baseUrl,
    clientId: 'MovieManagementApp_App',
    responseType: 'code',
    scope: 'offline_access MovieManagementApp',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44350',
      rootNamespace: 'MovieManagementApp',
    },
  },
} as Environment;
