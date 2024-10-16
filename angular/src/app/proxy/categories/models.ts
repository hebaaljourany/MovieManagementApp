import type { AuditedEntityDto } from '@abp/ng.core';

export interface CategoryDto extends AuditedEntityDto<string> {
  categoryName?: string;
  movieIds: string[];
}
