import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatGridListModule } from '@angular/material/grid-list';

import { AppComponent } from './app.component';

import { AdminMainComponent } from './Components/Admin/admin-main/admin-main.component';
import { AddCardComponent } from './Components/Admin/add-card/add-card.component';

import { ImageUploaderComponent } from './Components/Generic/image-uploader/image-uploader.component';
import { RegisterAccountComponent } from './Components/Register/register-account/register-account.component';
import { SelectionCardComponent } from './Components/Register/selection-card/selection-card.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminMainComponent,
    AddCardComponent,
    ImageUploaderComponent,
    RegisterAccountComponent,
    SelectionCardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      // { path: '', redirectTo: '/login', pathMatch: 'full' },
      // { path: 'login', component: LoginComponent, canActivate: [LoginGuard] },
      {
        path: 'admin',
        component: AdminMainComponent,
        // canActivate: [AdministratorGuard],
        children: [
          { path: '', redirectTo: 'add_card', pathMatch: 'full' },
          { path: 'add_card', component: AddCardComponent },
        ]
      },
      {
        path: "register", children: [
          { path: 'account', data: { title: "Registro de Cuenta" }, component: RegisterAccountComponent },
          { path: 'selection-card', data: { title: "Selección de Cartas" }, component: SelectionCardComponent }
        ]
      }
      // {
      //   path: 'player',
      //   component: PlayerMainComponent,
      //   // canActivate: [PlayerGuard],
      //   children: [
      //     { path: '', redirectTo: 'profile', pathMatch: 'full' },
      //     { path: 'profile', component: PlayerProfileComponent },
      //   ]
      // },
    ]),
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatCheckboxModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
