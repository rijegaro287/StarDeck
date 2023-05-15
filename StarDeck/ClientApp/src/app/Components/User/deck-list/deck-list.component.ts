import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';

import { AccountService } from 'src/app/Services/account.service';
import { CardService } from 'src/app/Services/card.service';
import { ParametersService } from 'src/app/Services/parameters.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';
import { IDeck } from 'src/app/Interfaces/Decks';

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

  constructor(
    private accountService: AccountService,
    private cardService: CardService,
    private parameterService: ParametersService,
    protected helpers: HelpersService,
    private _formBuilder: FormBuilder
  ) {
    this.userID = sessionStorage.getItem('ID')!;
    this.deckSize = 0;

    this.deckIDs = [];
    this.decks = [
      {
        id: 'q12312312',
        name: 'Deck 1',
        cards: [
          {
            id: "C-b04420af9d57",
            name: "Carta 7",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-17c3609f1b8f",
            name: "CARDNEW",
            energy: 10,
            battlecost: 10,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-619768da07b8",
            name: "Carta 6",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-19945dc9ca8e",
            name: "Sprint 1",
            energy: 1,
            battlecost: 1,
            image: '',
            active: true,
            type: 0,
            description: "Descripcion"
          },
          {
            id: "C-f98f79c59d66",
            name: "Carta 9",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-0c2b742990df",
            name: "Carta 14",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-6645008c3e62",
            name: "Carta 10",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-db20cfceb020",
            name: "Carta 1",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-b64bd066e6aa",
            name: "Carta 4",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-6e54c6efd948",
            name: "Carta 12",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-ebc746a5265e",
            name: "Carta 15",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-5825fddec7f9",
            name: "Carta 3",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-f6a37cba436e",
            name: "Carta 13",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-52eff016ba08",
            name: "Carta 2",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-4b370960de0a",
            name: "Carta 11",
            energy: 0,
            battlecost: 0,
            image: '',
            active: true,
            type: 0,
            description: "string"
          },
          {
            id: "C-ab4e1e5b6005",
            name: "rara 2",
            energy: 64,
            battlecost: 12,
            image: '',
            active: true,
            type: 2,
            description: "wddwe"
          },
          {
            id: "C-3063f8c85744",
            name: "Normal 12",
            energy: 12,
            battlecost: 21,
            image: '',
            active: true,
            type: 1,
            description: "wsqsqwwqsqws"
          },
          {
            id: "C-cad805020d6c",
            name: "un x100to",
            energy: 21,
            battlecost: 21,
            image: '',
            active: true,
            type: 1,
            description: "Me queda 1 x100to"
          }
        ]
      }
    ];

    this.cardCollectionIDs = [];
    this.cardCollection = [];

    this.onCardClicked = (card: ICard) => { };

    this.newDeck = {
      id: '',
      name: 'Nuevo escuadrón',
      cards: []
    }
    this.newDeckName = this._formBuilder.control('', []);
    this.selectedTab = 0;
    this.creatingDeck = false;
  }

  async ngOnInit() {
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

    // await this.accountService.getAccountDecks(this.userID)
  }

  onCreateClicked() {
    this.onCardClicked = this.onCardSelected;
    this.decks.push(this.newDeck);

    this.selectedTab += 1
    this.creatingDeck = true;
  }

  onCardSelected = (card: ICard) => {
    const newDeckCardsIDs = this.newDeck.cards.map((card) => card.id);
    if (newDeckCardsIDs.includes(card.id)) {
      card.borderColor = this.helpers.getCardBorderColor(card.type);
      this.newDeck.cards = this.newDeck.cards.filter((deckCard) => deckCard.id !== card.id);
    }
    else {
      card.borderColor = 'white';
      this.newDeck.cards.push(card);
    }
  }

  onDeckCreateClicked = () => {
    this.newDeck.name = this.newDeckName.value;
    this.newDeck.cardsIDs = this.newDeck.cards.map((card) => card.id);
    console.log(this.newDeck);
  }

  onDeckCancelClicked = () => {
    this.newDeckName.setValue('');
    this.newDeck.cards = [];
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
    const isNewDeckValid = this.newDeck.cards.length === this.deckSize;
    return isNewNameFilled && isNewDeckValid;
  }
}
