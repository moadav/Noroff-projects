import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LandingPage } from './pages/landing/landing.page';
import { TrainerPage } from './pages/trainer/trainer.page';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { PageTitleComponent } from './components/page-title/page-title.component';
import { FormsModule } from '@angular/forms';
import { CataloguePage } from './pages/catalogue/catalogue.page';
import { PokemonCardComponent } from './components/pokemon-card/pokemon-card.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HandleLoginComponent } from './components/handle-login/handle-login.component';

/**
 * Ng module containing the structure of the project and dependencies
 */
@NgModule({
  declarations: [
    AppComponent,
    LandingPage,
    TrainerPage,
    HandleLoginComponent,
    PageTitleComponent,
    CataloguePage,
    NavbarComponent,
    PokemonCardComponent,
  ],

  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
