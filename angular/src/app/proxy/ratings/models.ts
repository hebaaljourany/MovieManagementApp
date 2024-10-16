import type { AuditedEntityDto } from '@abp/ng.core';

export interface RatingDto extends AuditedEntityDto<string> {
  myAccountId?: string;
  movieId?: string;
  ratingValue: number;
}
