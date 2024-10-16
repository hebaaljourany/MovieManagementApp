import type { AuditedEntityDto } from '@abp/ng.core';
import type { InteractionType } from './interaction-type.enum';

export interface UserMovieInteractionDto extends AuditedEntityDto<string> {
  myAccountId?: string;
  movieId?: string;
  interaction: InteractionType;
}
