import { Injectable } from '@angular/core';
import { StorageKeys } from '../consts/storage-keys.enum';
import { Collector } from '../models/collector.model';
import { Pokemon } from '../models/pokemon.model';
import { StorageUtils } from '../utils/storage.utils';

@Injectable({
  providedIn: 'root',
})
export class CollectorService {
  // Variable
  private _collector?: Collector;

  constructor() {
    this._collector = StorageUtils.storageRead<Collector>(
      StorageKeys.collector
    );
  }
  /**
   * Gets the current collector
   */

  public get Collector(): Collector | undefined {
    return this._collector;
  }
  /**
   * Sets the current collector to new value
   */

  public set Collector(collector: Collector | undefined) {
    StorageUtils.storageSave<Collector>(StorageKeys.collector, collector!);
    this._collector = collector;
  }

  /**
   * A function to add pokemons to the collector user
   *
   * @param pokemon pokemon object
   */
  public addPokemon(pokemon: Pokemon | any) {
    if (this.Collector?.pokemons.includes(pokemon)) return;
    const updatedPokemon: any = {
      name: pokemon?.name,
      image: pokemon?.image,
    };
    this._collector?.pokemons.push(updatedPokemon);
    StorageUtils.storageSave<Collector>(
      StorageKeys.collector,
      this._collector!
    );
  }

  /**
   * A function that removes a pokemon from the collector
   *
   * @param pokemon the pokemon object
   */
  public removePokemon(pokemon: Pokemon | any) {
    const index: any = this.Collector?.pokemons.indexOf(pokemon);

    this.Collector?.pokemons.splice(index, 1);

    StorageUtils.storageSave<Collector>(
      StorageKeys.collector,
      this._collector!
    );
  }
}
