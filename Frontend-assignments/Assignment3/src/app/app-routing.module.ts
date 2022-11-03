import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CataloguePage } from './pages/catalogue/catalogue.page';

import { AuthGuard } from './guards/auth.guard';

import { LandingPage } from './pages/landing/landing.page';
import { TrainerPage } from './pages/trainer/trainer.page';

/**
 * The routes of the project used to navigate around
 */
const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/login',

    
  },
  {
    path: 'login',
    component: LandingPage,


  },
  {
    path: 'trainer',
    component: TrainerPage,
    canActivate: [AuthGuard],
  },
  {
    path: 'catalogue',
    component: CataloguePage,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
