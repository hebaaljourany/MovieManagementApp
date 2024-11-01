import type { AuditedEntityDto } from '@abp/ng.core';

export interface ActorDto extends AuditedEntityDto<string> {
  actorName?: string;
  actorImageBlob?: string;
  movieIds: string[];
}
