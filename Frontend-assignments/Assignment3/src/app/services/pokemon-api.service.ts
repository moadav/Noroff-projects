import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { finalize } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Pokemon, PokemonResponse } from '../models/pokemon.model';
import { CollectorService } from './collector.service';
const { apiPokemonCollectors, apiKey } = environment;

@Injectable({
  providedIn: 'root',
})
export class PokemonApiService {
  // Variables
  public loading: boolean = false;
  public pokemons: Pokemon[] = [];
  public error: any = '';
  private URL: string = 'https://pokeapi.co/api/v2/pokemon/';

  // Injecting service and httpClient
  constructor(
    private http: HttpClient,
    private readonly collectorService: CollectorService
  ) {}

  /**
   *
   * A function to get the pokemon id
   *
   * @param pokemon Pokemon Object
   * @returns a number with the id of the pokemon
   */
  private getPokemonId(pokemon: Pokemon): number {
    const arr = pokemon.url.split('/');
    return Number(arr[arr.length - 2]);
  }
  /**
   *
   * A function to set the pokemon image url
   *
   * @param pokemon pokemon object
   * @returns the pokemon object with an image
   */

  private setPokemonImageUrl(pokemon: Pokemon): Pokemon {
    const imageUrl =
      'https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/' +
      pokemon.id +
      '.png';
    pokemon.image = imageUrl;
    return pokemon;
  }
  /**
   *
   * A function that returns the pokemons of the given page
   *
   * @param page The page number
   */

  public getPokemons(page: number = 0) {
    this.loading = true;
    const offset: number = page * 10;
    this.http
      .get<PokemonResponse>(this.URL + '?limit=' + 10 + '&offset=' + offset)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe({
        next: (res: PokemonResponse) => {
          const pokemons: Pokemon[] = res.results.map((pokemon) => {
            pokemon.id = this.getPokemonId(pokemon);
            this.setPokemonImageUrl(pokemon);
            return pokemon;
          });
          this.pokemons = pokemons;
        },
      });
  }

  /**
   *
   * A function to update the collector user
   *
   * @returns the updated collector user
   */
  public updateTrainer() {
    const collectorPokemons = {
      pokemons: [...this.collectorService.Collector?.pokemons!],
    };

    const headers = new HttpHeaders({
      'Content-type': 'application/json',
      'x-api-key': apiKey,
    });
    return this.http
      .patch(
        `${apiPokemonCollectors}/${this.collectorService.Collector?.id}`,
        collectorPokemons,
        {
          headers,
        }
      )
      .subscribe();
  }
}
