import type { IRemoteStreamContent } from '../../../volo/abp/content/models';

export interface CreateUpdateActorDto {
  actorName: string;
  actorImageBlob: IRemoteStreamContent;
  movieIds: string[];
}
