import { Pokemon } from './pokemon.model';

export interface Collector {
  id: number;
  username: string;
  pokemons: Pokemon[];
}

