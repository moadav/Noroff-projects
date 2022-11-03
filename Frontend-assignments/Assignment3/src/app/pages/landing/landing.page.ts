import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StorageKeys } from 'src/app/consts/storage-keys.enum';
import { CollectorService } from 'src/app/services/collector.service';
import { StorageUtils } from 'src/app/utils/storage.utils';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.page.html',
  styleUrls: ['./landing.page.scss']
})
export class LandingPage {


  constructor(private readonly router: Router, private readonly collectorService: CollectorService) {
    if (StorageUtils.storageRead(StorageKeys.collector)) {
      this.collectorService.Collector = StorageUtils.storageRead(StorageKeys.collector);
      this.router.navigateByUrl("/catalogue");

    }
  }

  //logs the user to the trainer url
  public handleLogin(): void {
    this.router.navigateByUrl("/catalogue");
  }
}
