
import { Component, Inject, OnInit } from '@angular/core';
import { BattleService } from "src/app/Services/battle.service";
import { HelpersService } from "src/app/Services/helpers.service";
import { HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { SrvRecord } from 'dns';

@Component({
  selector: 'app-search-opponent',
  templateUrl: './search-opponent.component.html',
  styleUrls: ['./search-opponent.component.scss']
})
export class SearchOpponentComponent implements OnInit {
  idAccount: string;
  respuesta = {};
  dateBattle: string;

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
   * @param battleS injector del service de batalla para las peticiones
   */
  constructor(@Inject('BASE_URL') baseUrl: string,
    private battleS: BattleService ,
    protected helpers: HelpersService) {
    //-------------Inizializacion de variables --------------
    this.baseurl = baseUrl;
    this.idAccount = sessionStorage.getItem('ID')!;
    this.dateBattle = '';
    
  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {

    try {
      //Logica para la solicitud de una batalla 
      await this.battleS.search_battle(this.idAccount)
        .then(battle => {
          this.dateBattle = battle;
          console.log(battle)
        });
      window.location.assign(this.baseurl + "");
    } catch (error) {
      alert(error);
    }
    
    
    

  }
  /**
   * Función para validar que se creo la partida
   **/
  onValidate() {

  }
  
}

