import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from "@angular/router";
import {FormControl, FormGroup} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";
import { LoginService } from "../../Services/login.service";
import { ILoginData} from "../../Interfaces/login-data"




/**
 * Componentes utilizados para el funcionamiento de la pagina
 */
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

/**
 * Clase donde se desarrolla lo relacionada a la pantalla inicial de la pagina
 */
export class HomeComponent implements OnInit {
  //Variables a utilizar
  token = sessionStorage.getItem("Token");
  user = sessionStorage.getItem("Nombre")
  rol = sessionStorage.getItem("Rol")

  headers = {};
  respuesta = {};
  router: Router | undefined;
  baseurl: string;
  login = new FormGroup({
      Nickname: new FormControl(),
      Password: new FormControl(),
    }
  );

  /**
   * Constructor de la clase
   * @param http variable para la manipulacion del get y post
   * @param baseUrl variable para manejar la direccion de la pagina
   * @param snackBar injector of snackbar
   * @param service injector of service
   */
  constructor(@Inject('BASE_URL') baseUrl: string, public snackBar: MatSnackBar, private service: LoginService) {
    this.baseurl = baseUrl;
    if (this.token === undefined) {
      this.token = "null";
    }
    console.log(this.token)
    console.log(this.user)
  }
  ngOnInit(): void {
    }

  /**
   * Metodo donde se define la accion de atraer los datos para realizar las verificaciones correspondientes e iniciar sesion
   * @constructor metodo relacionado
   */
  async Sig_In(data: ILoginData) {
    console.log(data);
    let res = await this.service.login(data);
    if (res.status!==null) {
    sessionStorage.setItem("Nombre", <string>(data.Nickname));
    sessionStorage.setItem("Token", "True");
      sessionStorage.setItem("Rol", <string><unknown>res);
      if (<string><unknown>res === "Admin") {
        window.location.assign(this.baseurl + "/admin")
      } else {
        window.location.assign(this.baseurl + "/User")
      }
    }
    console.log(res)


  }

  /**
   * Metodo donde se desarrolla la accion de cerrar sesion en la pagina
   */
  async logout() {
    this.service.logout();
  }

  Register() {
      window.location.assign(this.baseurl + "Register")
  }
}
