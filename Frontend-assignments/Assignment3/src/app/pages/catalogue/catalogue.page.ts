import { Component, OnInit } from '@angular/core';
import { Pokemon } from 'src/app/models/pokemon.model';
import { PokemonApiService } from 'src/app/services/pokemon-api.service';

@Component({
  selector: 'app-catalogue',
  templateUrl: './catalogue.page.html',
  styleUrls: ['./catalogue.page.scss'],
})
export class CataloguePage implements OnInit {
  public page: number = 0;

  constructor(private pokemonApiService: PokemonApiService) {}

  // Returns a boolean value of pokemonApiService loading property
  get loading(): boolean {
    return this.pokemonApiService.loading;
  }

  // Returns a pokemon array of pokemonApiService pokemons property
  get pokemons(): Pokemon[] | undefined {
    return this.pokemonApiService.pokemons;
  }
  /**
   *
   * A method that fetches pokemons for a given page
   *
   * @param page an integer indicating the page number
   */

  public getPokemons(page: number = 0) {
    this.pokemonApiService.getPokemons(page);
  }
  /**
   * A method that increments the page and gets the corresponding pokemons
   */

  public handleNextPageClick() {
    this.page++;
    this.getPokemons(this.page);
  }

  /**
   * A method that decrements the page and gets the corresponding pokemons. Does not decrement if page === 0.
   */

  public handlePrevPageClick() {
    if (this.page === 0) return;
    this.page--;
    this.getPokemons(this.page);
  }

  ngOnInit(): void {
    this.getPokemons();
  }
}
