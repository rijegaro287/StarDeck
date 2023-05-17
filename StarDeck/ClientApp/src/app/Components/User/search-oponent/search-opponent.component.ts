
import { Component, Inject, OnInit } from '@angular/core';
import { BattleService } from "src/app/Services/battle.service";
import { HelpersService } from "src/app/Services/helpers.service";
import { HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'app-search-opponent',
  templateUrl: './search-opponent.component.html',
  styleUrls: ['./search-opponent.component.scss']
})
export class SearchOpponentComponent implements OnInit {
  idAccount: string;
  respuesta = {};
  dataBattle: string;

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
    private battleS: BattleService,
    protected helpers: HelpersService) {
    //-------------Inizializacion de variables --------------
    this.baseurl = baseUrl;
    this.idAccount = sessionStorage.getItem('ID')!;
    this.dataBattle = '';

  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {

    //Logica para la solicitud de una batalla 
    await this.battleS.search_battle(this.idAccount)
      .then(battle => {
        this.dataBattle = battle;
        console.log(battle)
      })
      .catch(error => {
        alert("No hay jugadores disponibles, intentelo más tarde");
        window.location.assign(this.baseurl + "user/battle/select-deck");

      })

    //Llamada al componente de Juego 
    //window.location.assign(this.baseurl + "");
  }


  async searchCancel() {
    //Logica para la solicitud de una batalla 
    await this.battleS.cancel(this.idAccount)
      .then(battle => {
        console.log(battle)
      });
    window.location.assign(this.baseurl + "user");
  }
  
  
}

