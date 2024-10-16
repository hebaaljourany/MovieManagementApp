import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { ActorDto } from '../actors/models';
import type { CategoryDto } from '../categories/models';
import type { RatingDto } from '../ratings/models';

export interface ActorLookupDto extends EntityDto<string> {
  actorName?: string;
}

export interface CategoryLookupDto extends EntityDto<string> {
  categoryName?: string;
}

export interface MovieDto extends AuditedEntityDto<string> {
  title?: string;
  releaseDate?: string;
  duration: number;
  description?: string;
  ageRating?: string;
  posterUrl?: string;
  videoUrl?: string;
  actors: ActorDto[];
  categories: CategoryDto[];
  ratings: RatingDto[];
  averageRating: number;
  totalViews: number;
  totalDownloads: number;
  actorIds: string[];
  categoryIds: string[];
}
