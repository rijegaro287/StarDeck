import { Inject, Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { ICard } from '../Interfaces/Card';
import { ILoginData } from '../Interfaces/login-data';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  url = `${apiURL}/login`;
  private baseUrl: string | URL = "";
  constructor(@Inject('BASE_URL') baseUrl: string, private request: RequestService) { this.baseUrl = baseUrl }


  login = (login: ILoginData): Promise<IServerResponse> => {
    return this.request.put(this.url + "/Signin", login);

  };

  logout = (): void => {
    let ans:string;
    let res = this.request.put(this.url+"/logout", JSON.stringify({}));
    res.then(x => ans=x.status);
    sessionStorage.clear();
    window.location.reload()
  }
}
