import { mapEnumToOptions } from '@abp/ng.core';

export enum InteractionType {
  Watched = 0,
  Downloaded = 1,
}

export const interactionTypeOptions = mapEnumToOptions(InteractionType);
