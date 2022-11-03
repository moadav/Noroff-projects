import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageKeys } from '../consts/storage-keys.enum';
import { CollectorService } from '../services/collector.service';
import { StorageUtils } from '../utils/storage.utils';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private readonly router: Router,
    private readonly collectorService: CollectorService,
  ) { }
  /**
   * A function that checks if user exists, and navigates it accordingly
   */
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.collectorService.Collector) {
        return true;
    } else if (StorageUtils.storageRead(StorageKeys.collector) ) {
        this.collectorService.Collector = StorageUtils.storageRead(StorageKeys.collector);
        return true;
    }  else {
      this.router.navigateByUrl("/login");
      return false;
    }

  }

}
