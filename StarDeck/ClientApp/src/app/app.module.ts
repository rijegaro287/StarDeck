import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { AppComponent } from './app.component';

import { ImageUploaderComponent } from './Components/Generic/image-uploader/image-uploader.component';
import { LoginComponent } from './Components/Generic/login/login.component';
import { NavbarComponent } from './Components/Generic/navbar/navbar.component';
import { CardListComponent } from './Components/Generic/card-list/card-list.component';

import { AdminMainComponent } from './Components/Admin/admin-main/admin-main.component';
import { AllCardsListComponent } from './Components/Admin/all-cards-list/all-cards-list.component';
import { CardFormDialogComponent } from './Components/Dialogs/card-form-dialog/card-form-dialog.component';
import { CardFormComponent } from './Components/Forms/card-form/card-form.component';
import { CardComponent } from './Components/Generic/card/card.component';

import { RegisterAccountComponent } from './Components/Register/register-account/register-account.component';
import { SelectionCardComponent } from './Components/Register/selection-card/selection-card.component';

import { UserMainComponent } from './Components/User/user-main/user-main.component';
import { HomeComponent } from './Components/User/home/home.component';
import { CreatePlanetComponent } from './Components/Admin/planet/create/create-planet.component';
import { PlanetComponent } from './Components/Generic/planet/planet.component';
import { PlanetListComponent } from './Components/Admin/planet/list/planet-list.component';
import { SelectDeckComponent } from './Components/User/select-deck/select-deck.component';
import { SearchOpponentComponent } from './Components/User/search-oponent/search-opponent.component';
import { DeckListComponent } from './Components/User/deck-list/deck-list.component';

import { GameMainComponent } from './Components/Game/game-main/game-main.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminMainComponent,
    AllCardsListComponent,
    ImageUploaderComponent,
    NavbarComponent,
    CardListComponent,
    CardFormComponent,
    CardFormDialogComponent,
    CardComponent,
    ImageUploaderComponent,
    RegisterAccountComponent,
    SelectionCardComponent,
    UserMainComponent,
    CreatePlanetComponent,
    PlanetComponent,
    PlanetListComponent,
    SelectDeckComponent,
    SearchOpponentComponent,
    DeckListComponent,
    GameMainComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent /* , canActivate: [LoginGuard] */ },
      {
        path: "Register", children: [
          { path: 'account', data: { title: "Registro de Cuenta" }, component: RegisterAccountComponent },
          { path: 'selection-card', data: { title: "Seleccion de Cartas" }, component: SelectionCardComponent }
        ]
      },
      {
        path: 'admin',
        component: AdminMainComponent,
        // canActivate: [AdministratorGuard],
        children: [
          { path: '', redirectTo: 'cards', pathMatch: 'full' },
          { path: 'cards', component: AllCardsListComponent },
          { path: 'planets', component: PlanetListComponent },
        ]
      },
      {
        path: "user", component: UserMainComponent,
        children: [
          { path: '', component: HomeComponent },
          { path: 'decks', component: DeckListComponent },
          { path: 'battle/select-deck', component: SelectDeckComponent },
          { path: 'battle/search-opponent', component: SearchOpponentComponent }
        ]
      },
      {
        path: "game", component: GameMainComponent
      }

    ]),
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatCardModule,
    MatTabsModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
