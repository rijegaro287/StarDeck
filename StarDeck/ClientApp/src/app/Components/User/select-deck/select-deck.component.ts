import * as random from "random-web-token";

import { Component, Inject, OnInit } from '@angular/core';


import { HelpersService } from "src/app/Services/helpers.service";
import { BattleService } from "src/app/Services/battle.service";

import { HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";
import { ICard } from "src/app/Interfaces/Card";

import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CardService } from "../../../Services/card.service";
import { IDeckNames } from "../../../Interfaces/Deck";
@Component({
  selector: 'app-select-deck',
  templateUrl: './select-deck.component.html',
  styleUrls: ['./select-deck.component.scss']
})
export class SelectDeckComponent implements OnInit {
  //---------------Variables a utilizar--------------
  idDeck: string;
  nameDeck: string;
  idAccount: string;
  deckList:IDeckNames[]; 
  idcardsDeckList:[];
  cardsDeckList: ICard[];
  newBattle: FormGroup;
  
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
    protected helpers: HelpersService, protected cards: CardService, protected battle: BattleService, private _formBuilder: FormBuilder,) {
    //-------------Inizializacion de variables --------------
    this.idAccount = sessionStorage.getItem('ID')!;

    this.baseurl = baseUrl;
    this.idDeck = '';
    this.nameDeck = '';
    this.deckList = [];
    this.idcardsDeckList = [];
    this.cardsDeckList = [];
    //Datos del formulario de Registro de Cuenta
    this.newBattle = this._formBuilder.group({
      selectedDeck: ['', Validators.required]
    });

    
  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {

    //Logica para obtener la lista de Decks de un jugador

    await this.battle.decks(this.idAccount)
      .then(decks => {
        this.deckList = decks;
        console.log(this.deckList)
      });
  }

  /**
   *Funcion que se llama cuando se cambia de opci�n en el Select
   */
  async ObtenerCartas() {
    this.cardsDeckList = [];
    this.idDeck = this.newBattle.value.selectedDeck.id.toString();
    this.nameDeck = this.newBattle.value.selectedDeck.name.toString();
    console.log(this.nameDeck)
    console.log(this.idDeck)

    //Logica para obtener los ids del deck selecccionado en el Select
    await this.battle.cardsofdeck(this.idDeck)
      .then(deck=> {
        this.idcardsDeckList = deck.cardlist;
        
      });

     //Logica para obtener las cartas del deck selecccionado 
    for (let i = 0; i < this.idcardsDeckList.length; i++) {
      const cardID = this.idcardsDeckList[i];
      await this.cards.getCard(cardID)
        .then((card) => {
          this.cardsDeckList.push(card);
          this.cardsDeckList = this.cardsDeckList.slice()
        });
    }
    
  }
  /**
   *Funcion que se llama cuando se da click en Buscar Batalla
   */
  async Batalla() {
    try {
      this.onSelect();
      await this.battle.favoritedeck(this.idAccount, this.idDeck)
        .then(respuesta => {
          console.log(respuesta)
      });
      window.location.assign(this.baseurl + "user/battle/search-opponent");
    } catch (error) {
      console.error(error)
    }    
  }
  /*
   *Función que valida si se selecciono o no un escuadrón
   **/
  onSelect() {

    if (this.idDeck == '' && this.nameDeck == '') {
      throw new Error('Debe seleccionar un escuadrón de batalla');
    }

  }
  
  
}

