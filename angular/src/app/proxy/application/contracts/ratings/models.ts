
export interface CreateUpdateRatingDto {
  userId: string;
  movieId: string;
  rating: number;
  ratingDate?: string;
}
