import { Component} from '@angular/core';
import { Router } from '@angular/router';
import { StorageKeys } from 'src/app/consts/storage-keys.enum';
import { Collector } from 'src/app/models/collector.model';
import { CollectorService } from 'src/app/services/collector.service';
import { StorageUtils } from 'src/app/utils/storage.utils';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  get collector(): Collector | undefined {
    return this.collectorService.Collector;
  }

  constructor(
    private readonly collectorService: CollectorService,
    private readonly router: Router
  ) {}

  /**
   * A function that logs the user out if the user agrees to it
   */
  public logOut(): void {
    if (confirm('Are you sure you want to log out?')) {
      this.collectorService.Collector = undefined;
      StorageUtils.storageDelete(StorageKeys.collector);
      this.router.navigateByUrl('/login');
    }
  }
}
