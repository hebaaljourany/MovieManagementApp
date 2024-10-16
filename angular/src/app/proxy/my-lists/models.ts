import type { AuditedEntityDto } from '@abp/ng.core';

export interface MyListDto extends AuditedEntityDto<string> {
  myAccountId?: string;
  movieId?: string;
}
