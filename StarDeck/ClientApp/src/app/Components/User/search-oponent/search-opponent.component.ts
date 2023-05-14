import * as random from "random-web-token";

import { Component, Inject, OnInit } from '@angular/core';

import { AccountService } from 'src/app/Services/account.service';
import { HelpersService } from "src/app/Services/helpers.service";

import { ICard } from 'src/app/Interfaces/Card';
import { HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'app-search-opponent',
  templateUrl: './search-opponent.component.html',
  styleUrls: ['./search-opponent.component.scss']
})
export class SearchOpponentComponent implements OnInit {
 
  respuesta = {};

  router: Router | undefined;
  baseurl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'withCredentials': 'true'
    })
  };
  /**
   * Constructor de la clase
   * @param baseUrl variable para manejar la direccion de la pagina
   * @param accountService injector del service de cuenta para las peticiones
   */
  constructor(@Inject('BASE_URL') baseUrl: string,
    private accountService: AccountService,
    protected helpers: HelpersService) {
    //-------------Inizializacion de variables --------------
    this.baseurl = baseUrl;
    
  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {
    

  }


  
}

