import * as random from "random-web-token";

import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AccountService } from 'src/app/Services/account.service';
import { RequestService } from 'src/app/Services/request.service';

import { IAccount, ICollection } from 'src/app/Interfaces/Account';
import { LoginComponent } from "../../Generic/login/login.component";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";


@Component({
  selector: 'app-register-account',
  templateUrl: './register-account.component.html',
  styleUrls: ['./register-account.component.scss']
})

/*
 * Clase donde se exporta los elementos requeridos
 */
export class RegisterAccountComponent {
  respuesta = {};
  http: HttpClient;
  router: Router | undefined;
  baseurl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'withCredentials': 'true'
    })
  };
  /* Formulario del Registro de Cuenta */
  newUser: FormGroup;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string,
    private _formBuilder: FormBuilder,
    private requestService: RequestService,
    private accountService: AccountService
  ) {
    //Variables a utilizar
    this.http = http;
    this.baseurl = baseUrl
    //Datos del formulario de Registro de Cuenta
    this.newUser = this._formBuilder.group({
      name: ['', Validators.required],
      nickname: ['', Validators.required],
      email: ['', Validators.required],
      nationality: ['', Validators.required],
      password: ['', Validators.required],
      passwordv: ['', Validators.required],
      term: ['', Validators.required]
    });
  }

  createAccount() {
    const initialCollection: ICollection = {
      idAccount:'{C- ,C- ,C- ,C- ,C- ,C- }',
      collection: '15',

    }
   
    const newUser: IAccount = {
      id: 'U-'+ random.genSync('medium+', 12),
      name: this.newUser.value.name.toString(),
      nickname: this.newUser.value.nickname.toString(),
      email: this.newUser.value.email.toString(),
      nationality: this.newUser.value.nationality.toString(),
      password: this.newUser.value.password.toString(),
      avatar: 1,
      active: true,
      config: 'user',
      points: 0,
      coins: 20,
      collection: initialCollection,
      
    };

    console.log(newUser);
    console.log(JSON.stringify(newUser));

    try {
      this.validateAccount(newUser);

      this.accountService.addUser(newUser)
        .then(response => {
          console.log(response);
          this.requestService.handleResponse(response);
        });
    } catch (error) {
      alert(error);
    }
    window.location.assign(this.baseurl + "Register/selection-card")
  }

  validateAccount(user: IAccount): void {
    Object.keys(user).forEach(key => {
      const value = user[key as keyof IAccount];
      if (value === '' || value === null || value === undefined) {
        if (key === ' ') {
          throw new Error('Sus datos están incompletos intente de nuevo');
        }
      }
    });

    if (this.newUser.value.password.toString() != this.newUser.value.passwordv.toString() ) {
      throw new Error('Las contraseñas no coinciden verifiquela e intente de nuevo');
    }
  }
}
