import type { IRemoteStreamContent } from '../../../volo/abp/content/models';

export interface CreateUpdateMovieDto {
  title: string;
  blob: IRemoteStreamContent;
  posterBlob: IRemoteStreamContent;
  coverBlob: IRemoteStreamContent;
  duration: number;
  description?: string;
  ageRating: string;
  releaseDate: string;
  actorIds: string[];
  categoryIds: string[];
}
