import type { AuditedEntityDto } from '@abp/ng.core';

export interface ActorDto extends AuditedEntityDto<string> {
  actorName?: string;
  actorImage?: string;
  movieIds: string[];
}
