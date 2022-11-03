export interface Pokemon {
  id: number;
  name: string;
  image: string;
  url: string;
}

export interface PokemonResponse {
  results: Pokemon[];
}
