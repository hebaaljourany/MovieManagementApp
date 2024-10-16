
export interface CreateUpdateMovieDto {
  title: string;
  duration: number;
  description?: string;
  ageRating: string;
  releaseDate: string;
  posterUrl: string;
  videoUrl: string;
  actorIds: string[];
  categoryIds: string[];
}
