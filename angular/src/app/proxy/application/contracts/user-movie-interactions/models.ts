import type { InteractionType } from '../../../user-movie-interactions/interaction-type.enum';

export interface CreateUpdateUserMovieInteractionDto {
  userId: string;
  movieId: string;
  interaction: InteractionType;
  interactionDate?: string;
}
