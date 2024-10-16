import type { AuditedEntityDto } from '@abp/ng.core';

export interface ActorDto extends AuditedEntityDto<string> {
  actorName?: string;
  movieIds: string[];
}
