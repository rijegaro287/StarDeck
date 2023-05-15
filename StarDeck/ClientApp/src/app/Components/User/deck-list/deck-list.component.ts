import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';

import { AccountService } from 'src/app/Services/account.service';
import { CardService } from 'src/app/Services/card.service';
import { ParametersService } from 'src/app/Services/parameters.service';
import { DeckService } from 'src/app/Services/deck.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';
import { IDeck } from 'src/app/Interfaces/Deck';

@Component({
  selector: 'app-deck-list',
  templateUrl: './deck-list.component.html',
  styleUrls: ['./deck-list.component.scss']
})
export class DeckListComponent implements OnInit {
  userID: string;
  deckSize: number;

  deckIDs: string[];
  decks: IDeck[];

  cardCollectionIDs: string[];
  cardCollection: ICard[];

  onCardClicked: (card: ICard) => void;

  newDeck: IDeck;
  newDeckName: FormControl;
  selectedTab: number;
  creatingDeck: boolean;
  loadingDecks: boolean;

  constructor(
    private accountService: AccountService,
    private cardService: CardService,
    private parameterService: ParametersService,
    private deckService: DeckService,
    protected helpers: HelpersService,
    private _formBuilder: FormBuilder
  ) {
    this.userID = sessionStorage.getItem('ID')!;
    this.deckSize = 0;

    this.deckIDs = [];
    this.decks = [];

    this.cardCollectionIDs = [];
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
          this.cardCollection = this.cardCollection.slice()
        });
    }

    await this.deckService.getUserDecks(this.userID)
      .then((decks) => { this.decks = decks; });

    this.decks.forEach((deck) => {
      deck.cards = this.cardCollection
        .filter((card) => deck.cardlist.includes(card.id));
    })

    this.loadingDecks = false;
  }

  onCreateClicked() {
    this.onCardClicked = this.onCardSelected;
    this.decks.push(this.newDeck);

    this.selectedTab += 1
    this.creatingDeck = true;
  }

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

  onDeckCreateClicked = () => {
    this.newDeck.deckName = this.newDeckName.value;
    this.newDeck.cardlist = this.newDeck.cards!.map((card) => card.id);
    this.newDeck.idAccount = this.userID;

    this.deckService.addUserDeck(this.newDeck)
      .then((response) => {
        console.log(response);
      });
  }

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

  validateNewDeck = () => {
    const isNewNameFilled = this.newDeckName.value.length > 0;
    const isNewDeckValid = this.newDeck.cards!.length === this.deckSize;
    return isNewNameFilled && isNewDeckValid;
  }
}
