import * as random from "random-web-token";

import { Component, Inject, OnInit } from '@angular/core';


import { HelpersService } from "src/app/Services/helpers.service";
import { BattleService } from "src/app/Services/battle.service";

import { HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { ICard } from "src/app/Interfaces/Card";
@Component({
  selector: 'app-select-deck',
  templateUrl: './select-deck.component.html',
  styleUrls: ['./select-deck.component.scss']
})
export class SelectDeckComponent implements OnInit {
  //---------------Variables a utilizar--------------
  idSquad: string;
  idAccount: string;
  deckList: []; //Lista de decks
  cardsDeckList: ICard[];
  selectedDeck: string; //Deck seleccionado

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
    protected helpers: HelpersService, protected battle: BattleService) {
    //-------------Inizializacion de variables --------------
    this.idAccount = sessionStorage.getItem('ID')!;

    this.baseurl = baseUrl;
    this.idSquad = '';
    this.deckList = [];
    this.cardsDeckList = [];

    this.selectedDeck = '';

  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {

    //Logica para obtener la lista de Decks de un jugador

    await this.battle.decks(this.idAccount)
      .then(decks => {
        this.deckList = decks;
      });


  }

  /**
   *Funcion que se llama cuando se da click en Aceptar 
   */
  async ObtenerCartas() {

    //Logica para obtener las cartas del deck selecccionado en el Select
    await this.battle.cardsofdeck(this.selectedDeck)
      .then(cards => {
        this.cardsDeckList = cards;
      });

  }
  /**
   *Funcion que se llama cuando se da click en Buscar Batalla
   */
  async Batalla() {
    window.location.assign(this.baseurl + "batalla/search-opponent");

    //Logica para la solicitud de una partida al servidor
    // await this.battle.search_battle(this.selectedDeck)
    //   .then(battle => {
    //   });


  }

}

