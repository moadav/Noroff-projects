import { Component, Input, OnInit } from '@angular/core';
import { Pokemon } from 'src/app/models/pokemon.model';
import { CollectorService } from 'src/app/services/collector.service';
import { PokemonApiService } from 'src/app/services/pokemon-api.service';

@Component({
  selector: 'app-pokemon-card',
  templateUrl: './pokemon-card.component.html',
  styleUrls: ['./pokemon-card.component.scss'],
})
export class PokemonCardComponent implements OnInit {
  public isAdded: any = false;

  constructor(
    private collectorService: CollectorService,
    private readonly pokemonApi: PokemonApiService
  ) {}

  @Input() pokemon: Pokemon | any;

  // Checks if pokemon is added/collected on init
  ngOnInit(): void {
    this.isAdded = this.collectorService.Collector?.pokemons
      .map((p) => p.name)
      .includes(this.pokemon.name);
  }

  /**
   * A function that saves the pokemon to the collector
   */
  public handleSavePokemon(): void {
    if (!this.collectorService.Collector?.pokemons.includes(this.pokemon)) {
      this.collectorService.addPokemon(this.pokemon);
      this.pokemonApi.updateTrainer();
      this.isAdded = true;
    }
  }

  /**
   * A function that removes the pokemon of the collector
   */
  public handleRemovePokemon(): void {
    if (
      this.collectorService.Collector?.pokemons.map((p) =>
        p.name.includes(this.pokemon.name)
      )
    ) {
      this.collectorService.removePokemon(this.pokemon);
      this.pokemonApi.updateTrainer();
      this.isAdded = false;
    }
  }
}
