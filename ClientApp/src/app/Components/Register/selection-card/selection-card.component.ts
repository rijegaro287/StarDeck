import * as random from "random-web-token";

import { Component, Inject, OnInit } from '@angular/core';

import { AccountService } from 'src/app/Services/account.service';
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
export class SelectionCardComponent implements OnInit {
  //---------------Variables a utilizar--------------
  idAccount: string;
  idCard: string;
  //Coleccion asignada
  collectionInitial: [];

  //Como obtener estas cartas sin Id
  cards: ICard[];
  //Rondas de cartas a seleccionar
  selectionCard1: ICard[];
  selectionCard2: ICard[];
  selectionCard3: ICard[];
  //Lista de cartas seleccionadas
  selectedCards: ICard[];
  //Banderas de seleccion en ronda 
  cardSelected1: boolean;
  cardSelected2: boolean;
  cardSelected3: boolean;
  respuesta = {};
 
  router: Router | undefined ;
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
  constructor( @Inject('BASE_URL') baseUrl: string, private accountService: AccountService,)
  {
    //-------------Inizializacion de variables --------------
    this.idAccount = sessionStorage.getItem('ID')!;
    this.idCard = '';
    console.log(this.idAccount);
    this.baseurl = baseUrl;
    this.collectionInitial=[]
    this.cards = [];
    this.selectionCard1 = [];
    this.selectionCard2 = [];
    this.selectionCard3 = [];
    this.selectedCards = [];
    this.cardSelected1 = false;
    this.cardSelected2 = false;
    this.cardSelected3 = false;
  }
  /**
   *Funcion que se ejecuta cuando se carga el componente
   * */
  async ngOnInit() {
    //Obtiene la coleccion de 15 cartas que le asigna el sistema
    this.accountService.cards(this.idAccount)
      .then(collection => {
        // Coleccion Inicial designada al jugador
        this.collectionInitial = collection;
        console.log(collection)
      });

    //Busca la informacion de la cartas que fueron asignadas
    for (let i = 0; i < this.collectionInitial.length; i++) {
      this.accountService.getCard(this.collectionInitial[i])
        .then(card => {
          //Agregar las cartas a la lista principal de la coleccion del jugador para mostrarlas
          this.cards.push(card);
        });
    }
    //Solicita todas las cartas, para luego filtrarlas, revolverlas y asignarlas en los 3 grupos de seleccion 
    await this.accountService.allCards()
      .then((cards) => {
       
        //Cartas filtradas de tipo Rara y Basica
        let filteredCard = cards.filter(card => card.type === 1 || card.type === 2);
        //Cartas ordenadas aleatoriamente
        let randomCard = filteredCard.sort(() => Math.random() - 0.5)
        //Lista de cartas seleccionadas
        randomCard = randomCard.slice(0, 9);
        
        //Asignar las cartas a seleccionar
        this.selectionCard1 = randomCard.slice(0, 2);
        console.log(this.selectionCard1)
        this.selectionCard2 = randomCard.slice(3, 5);
        console.log(this.selectionCard2)
        this.selectionCard3 = randomCard.slice(6, 8);
        console.log(this.selectionCard3)
    
      });    
    }

  /**
   *Funcion que se llama cuando se da click en Aceptar 
   */
  createInitialCollection() {
    for (let i = 0; i <= 2; i++) {
      this.idCard = this.selectedCards[i].id.toString()
      console.log(this.idCard)
      /*//Metodo de enviar cartas seleccionadas
      this.accountService.addCards(this.idAccount, this.idCard)
        .then(response => {
          console.log(response);
        });*/
    }
    

    window.location.assign(this.baseurl + "login")
  }
  /**
   * Funcion que se utilizar para seleccionar las cartas
   * @param card informacion de la carta que seleccione
   * @param selection este numero indica en la ronda de cartas que el usuario selecciono 
   * @param selection este numero indica en la ronda de cartas que el usuario selecciono 
   * */
  onSelect(card: ICard, selection:number) {
    switch (selection) {
      //Seleccion de la primer carta
      case 1:
        if (!this.cardSelected1) {
          this.selectedCards.push(card);
          this.cardSelected1 = true;
        }
        break;
      //Seleccion de la segunda carta
      case 2:
        if (!this.cardSelected2) {
          this.selectedCards.push(card);
          this.cardSelected2 = true;
        }
        break;
      //Seleccion de la tercer carta
      case 3:
        if (!this.cardSelected3) {
          this.selectedCards.push(card);
          this.cardSelected3 = true;
        }
        break;
    }
    console.log(this.selectedCards);
     

  }
}
  
