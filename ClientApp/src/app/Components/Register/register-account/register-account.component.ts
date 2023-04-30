import * as random from "random-web-token";

import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AccountService } from 'src/app/Services/account.service';
import { RequestService } from 'src/app/Services/request.service';
import { LoginService } from 'src/app/Services/login.service';

import { IAccount, ICollection } from 'src/app/Interfaces/Account';
import { LoginComponent } from "../../Generic/login/login.component";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { ILoginData } from "../../../Interfaces/login-data";


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
  term: boolean;
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
    private accountService: AccountService,
    private service: LoginService,
  ) {
    //Variables a utilizar
    this.http = http;
    this.baseurl = baseUrl
    this.term = false;

    //Datos del formulario de Registro de Cuenta
    this.newUser = this._formBuilder.group({
      name: ['', Validators.required],
      nickname: ['', Validators.required],
      email: ['', Validators.required],
      nationality: ['', Validators.required],
      password: ['', Validators.required],
      passwordv: ['', Validators.required],

    });
  }
  /*
   *Funcion que crea la cuenta del nuevo jugador 
   */
  async createAccount() {
    const user: ILoginData =
    {
      Email: this.newUser.value.email.toString(),
      Password: this.newUser.value.password.toString()
    }

    const newUser: IAccount = {
      id: 'U-' + random.genSync('medium+', 12),
      name: this.newUser.value.name.toString(),
      nickname: this.newUser.value.nickname.toString(),
      email: this.newUser.value.email.toString(),
      country: this.newUser.value.nationality.toString(),
      password: this.newUser.value.password.toString(),
    };
    var hash = await window.crypto.subtle.digest("SHA-256", new TextEncoder().encode(newUser.password));
    newUser.password = Array.from(new Uint8Array(hash)).map(b => b.toString(16).padStart(2, "0")).join("")
    console.log(newUser);
    console.log(JSON.stringify(newUser));

    try {
      this.validateAccount(newUser);

      await this.accountService.addUser(newUser)
        .then(response => {
          console.log(response);
        });
      await this.service.login(user)
      window.location.assign(this.baseurl + "Register/selection-card")
    } catch (error) {
      alert(error);
    }


  }
  /*
   * Funcion para validar el checkbox de terminos y condiciones 
   */
  onCheck() {
    this.term = !this.term;
    console.log(this.term)

  }


  /*
   *Funcion para validar los datos ingresados en el formulario
   */
  validateAccount(user: IAccount): void {
    Object.keys(user).forEach(key => {
      const value = user[key as keyof IAccount];
      if (value === '' || value === null || value === undefined) {
        throw new Error('Sus datos están incompletos, intente de nuevo');
      }
    });

    const validEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(this.newUser.value.email);
    if (this.newUser.value.name.toString().length < 5 || this.newUser.value.name.toString().length > 30) {
      throw new Error('El nombre debe contener entre 5 y 30 carácteres');
    }
    if (this.newUser.value.nickname.toString().length < 5 || this.newUser.value.nickname.toString().length > 30) {
      throw new Error('El nickname debe contener entre 5 y 30 carácteres');
    }
    if (!validEmail) {
      throw new Error("Ingrese una dirección de correo electrónico válida");
    }
    if (this.newUser.value.password.toString().length != 8) {
      throw new Error('La contraseña debe ser de 8 carácteres');
    }
    if (this.newUser.value.password.toString() != this.newUser.value.passwordv.toString()) {
      throw new Error('Las contraseñas no coinciden verifiquela e intente de nuevo');
    }
    if (this.term == false) {
      throw new Error('Debe aceptar los terminos y condiciones del juego para continuar');
    }
  }
}
