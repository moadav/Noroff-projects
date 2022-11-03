import { Component, OnInit } from '@angular/core';
import { Pokemon } from 'src/app/models/pokemon.model';
import { CollectorService } from 'src/app/services/collector.service';

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.page.html',
  styleUrls: ['./trainer.page.scss'],
})
export class TrainerPage implements OnInit {
  constructor(private collectorService: CollectorService) {}

  // Gets the collector's list of pokemons
  get pokemons(): Pokemon[] | any {
    return this.collectorService.Collector?.pokemons;
  }

  ngOnInit(): void {}
}
