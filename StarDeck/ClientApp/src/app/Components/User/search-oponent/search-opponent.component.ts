import { Component, Inject, OnInit } from '@angular/core';
import { HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";

import { BattleService } from "src/app/Services/battle.service";
import { HelpersService } from "src/app/Services/helpers.service";

import { IGameRoom } from 'src/app/Interfaces/Game';

@Component({
  selector: 'app-search-opponent',
  templateUrl: './search-opponent.component.html',
  styleUrls: ['./search-opponent.component.scss']
})
export class SearchOpponentComponent implements OnInit {
  idAccount: string;
  dataBattle: IGameRoom;

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
    this.dataBattle = {} as IGameRoom;

  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {

    //Logica para la solicitud de una batalla 
    await this.battleS.searchBattle(this.idAccount)
      .then((battle) => {
        //Llamada al componente de Juego 
        this.dataBattle = battle;

        sessionStorage.setItem('GameRoomData', JSON.stringify(this.dataBattle));

        window.location.assign(this.baseurl + "game");
        console.log(battle)
      })
      .catch((error) => {
        alert("No hay jugadores disponibles, intentelo más tarde");
        window.location.assign(this.baseurl + "user/battle/select-deck");
      });

  }


  async searchCancel() {
    //Logica para la solicitud de una batalla 
    await this.battleS.cancelBattleSearch(this.idAccount)
      .then((battle) => {
        window.location.assign(this.baseurl + "user");
      });
  }
}