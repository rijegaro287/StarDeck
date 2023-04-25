import { Inject, Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { ICard } from '../Interfaces/Card';
import { ILoginData } from '../Interfaces/login-data';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IServerResponse } from '../Interfaces/ServerResponse';
import { async } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url = `${apiURL}/login`;
  private baseUrl: string | URL = "";
  constructor(@Inject('BASE_URL') baseUrl: string, private request: RequestService) { this.baseUrl = baseUrl }


  login = async(login: ILoginData): Promise<IServerResponse> => {
    
    
    var hash = await window.crypto.subtle.digest("SHA-256", new TextEncoder().encode(login.Password));
    login.Password = Array.from(new Uint8Array(hash)).map(b => b.toString(16).padStart(2, "0")).join("")
    let sign = await this.request.put(this.url + "/Signin", login)
    let res = <ILoginData><unknown> sign;

    if (res.Id !== null) {
      sessionStorage.setItem("Nombre", <string>(login.Email));
      sessionStorage.setItem("Token", "True");
      sessionStorage.setItem("Rol", <string>res.Rol);
      sessionStorage.setItem("ID", <string>res.Id);
      
    } else {
      alert('Usuario o contraseña incorrecto')
    }
    console.log(res)
    return sign;
  };

  logout = (): Promise<IServerResponse> => {
    return this.request.put(this.url+"/logout", JSON.stringify({}));
  }
}
