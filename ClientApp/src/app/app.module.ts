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

import { AppComponent } from './app.component';

import { AdminMainComponent } from './Components/Admin/admin-main/admin-main.component';
import { AddCardComponent } from './Components/Admin/add-card/add-card.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminMainComponent,
    AddCardComponent,
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
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
