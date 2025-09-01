export interface Game {
  id: number;
  name: string;
  genre: string;
  price: number;
  releaseDate: string; // DateOnly in C# → string in JSON
}
