import * as random from "random-web-token";

import { Component, Inject } from '@angular/core';


import { RequestService } from 'src/app/Services/request.service';

import { ICollection } from 'src/app/Interfaces/Account';
import {ICard } from 'src/app/Interfaces/Card';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'app-selection-card',
  templateUrl: './selection-card.component.html',
  styleUrls: ['./selection-card.component.scss']
})
export class SelectionCardComponent {
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

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    //Variables a utilizar
    this.http = http;
    this.baseurl = baseUrl
  }

  createInitialCollection() {

    window.location.assign(this.baseurl + "login")
  }
}
  
