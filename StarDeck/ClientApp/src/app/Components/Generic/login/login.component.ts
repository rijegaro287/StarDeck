import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ILoginData } from '../../../Interfaces/login-data';
import { LoginService } from '../../../Services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  //Variables a utilizar
  token = sessionStorage.getItem("Token");
  user = sessionStorage.getItem("Nombre")
  rol = sessionStorage.getItem("Rol")

  headers = {};
  respuesta = {};
  router: Router | undefined;
  baseurl: string;
  login = new FormGroup({
    Email: new FormControl(),
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
  public async Sig_In(data: ILoginData) {
    await this.service.login(data);
    let Rol = sessionStorage.getItem('Rol')
    if (Rol === "Admin") {
      window.location.assign(this.baseurl + "/admin")
    } else {
      window.location.assign(this.baseurl + "/User")
    }
  }

  /**
   * Metodo donde se desarrolla la accion de cerrar sesion en la pagina
   */
  async logout() {
    let res = this.service.logout();
    window.location.assign(this.baseurl)
  }

  Register() {
    window.location.assign(this.baseurl + "Register/account")

  }
}
