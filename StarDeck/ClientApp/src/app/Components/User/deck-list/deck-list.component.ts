import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';

import { AccountService } from 'src/app/Services/account.service';
import { CardService } from 'src/app/Services/card.service';
import { ParametersService } from 'src/app/Services/parameters.service';
import { DeckService } from 'src/app/Services/deck.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';
import { IDeck } from 'src/app/Interfaces/Deck';
import {CreatePlanetComponent} from "../../Admin/planet/create/create-planet.component";
import {CardInformationComponent} from "../../Generic/card-information/card-information.component";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-deck-list',
  templateUrl: './deck-list.component.html',
  styleUrls: ['./deck-list.component.scss']
})
export class DeckListComponent implements OnInit {
  /** ID del usuario que ha iniciado sesión */
  userID: string;
  /** Tamaño máximo de los escuadrones */
  deckSize: number;

  /** IDs de la colección de cartas del usuario */
  cardCollectionIDs: string[];
  /** Cartas de la colección del usuario */
  cardCollection: ICard[];
  /** Escuadrones del usuario */
  decks: IDeck[];

  /** Se ejecuta al hacer click sobre una carta */
  onCardClicked: (card: ICard) => void;

  /** Contiene la información de un nuevo escuadrón */
  newDeck: IDeck;
  /** Input para el nombre del nuevo escuadrón */
  newDeckName: FormControl;
  /** Indica la pestaña del escuadrón seleccionado */
  selectedTab: number;

  /** Indica si se están cargando los escuadrones del usuario */
  loadingDecks: boolean;
  /** Indica si se está creando un nuevo escuadrón */
  creatingDeck: boolean;

  constructor(
    private dialog: MatDialog,
    private accountService: AccountService,
    private cardService: CardService,
    private parameterService: ParametersService,
    private deckService: DeckService,
    protected helpers: HelpersService,
    private _formBuilder: FormBuilder
  ) {
    this.userID = sessionStorage.getItem('ID')!;
    this.deckSize = 0;


    this.cardCollectionIDs = [];
    this.decks = [];
    this.cardCollection = [];

    this.onCardClicked = (card: ICard) => { };

    this.newDeck = {
      id: '',
      idAccount: '',
      deckName: 'Nuevo escuadrón',
      cardlist: [],
      cards: []
    }
    this.newDeckName = this._formBuilder.control('', []);
    this.selectedTab = 0;

    this.creatingDeck = false;
    this.loadingDecks = false;
  }

  /**
   * Se ejecuta le inicializar el componente
   */
  async ngOnInit() {
    console.log(this.userID);
    this.loadingDecks = true;

    await this.parameterService.getParameter('deckSize')
      .then((parameter) => this.deckSize = Number(parameter.value));

    await this.accountService.getAccountCards(this.userID)
      .then((cardIDs) => this.cardCollectionIDs = cardIDs);

    for (let i = 0; i < this.cardCollectionIDs.length; i++) {
      const cardID = this.cardCollectionIDs[i];
      await this.cardService.getCard(cardID)
        .then((card) => {
          this.cardCollection.push(card);
          this.cardCollection = this.cardCollection.slice();
        });
    }

    await this.deckService.getUserDecks(this.userID)
      .then((decks) => { this.decks = decks; })
      .catch((error) => console.log(error.error));

    this.decks.forEach((deck) => {
      deck.cards = this.cardCollection
        .filter((card) => deck.cardlist.includes(card.id));
    })

    this.loadingDecks = false;
  }

  /** Se ejecuta al hacer click sobre el botón que habilita la creación de escuadrón */
  onCreateClicked() {
    this.onCardClicked = this.onCardSelected;
    this.decks.push(this.newDeck);

    this.selectedTab = this.decks.length;
    this.creatingDeck = true;
  }

  /** Se ejecuta al hacer click sobre una carta mientras se crea un escuadrón */
  onCardSelected = (card: ICard) => {
    const newDeckCardsIDs = this.newDeck.cards!.map((card) => card.id);

    if (newDeckCardsIDs.includes(card.id)) {
      card.borderColor = this.helpers.getCardBorderColor(card.type);
      this.newDeck.cards! = this.newDeck.cards!.filter((deckCard) => deckCard.id !== card.id);
    }
    else {
      card.borderColor = 'white';
      this.newDeck.cards!.push(card);
    }
  }

  /** Se ejecuta al hacer click sobre el botón de crear escuadrón */
  onDeckCreateClicked = () => {
    this.newDeck.deckName = this.newDeckName.value;
    this.newDeck.cardlist = this.newDeck.cards!.map((card) => card.id);
    this.newDeck.idAccount = this.userID;

    this.deckService.addUserDeck(this.newDeck)
      .then((response) => window.location.reload())
      .catch((error) => alert(error.message));
  }

  /** Se ejecuta al hacer click sobre el botón de cancelar creación de escuadrón */
  onDeckCancelClicked = () => {
    this.newDeckName.setValue('');
    this.newDeck.cards! = [];
    this.selectedTab -= 1
    this.creatingDeck = false;
    this.decks.pop();

    this.cardCollection.forEach((card) => {
      card.borderColor = this.helpers.getCardBorderColor(card.type);
    });

    this.onCardClicked = (card: ICard) => { };
  }

  /** Valida que el nuevo escuadrón cumpla con las restricciones */
  validateNewDeck = () => {
    const isNewNameFilled = this.newDeckName.value.length > 0;
    const isNewDeckValid = this.newDeck.cards!.length === this.deckSize;
    return isNewNameFilled && isNewDeckValid;
  }

  cardInformation() {

    const dialogRef = this.dialog.open(CardInformationComponent);
  }
}
