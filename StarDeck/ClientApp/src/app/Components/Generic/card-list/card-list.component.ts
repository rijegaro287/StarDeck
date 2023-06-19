import { Component, Input, OnChanges } from '@angular/core';

import { CARD_TYPES } from 'src/app/app.component';

import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss']
})
export class CardListComponent implements OnChanges {
  /** Lista de cartas a mostrar */
  @Input() cards: ICard[]
  /** Función que se ejecuta al hacer click sobre una carta */
  @Input() onCardClicked: (card: ICard) => void

  /** Cartas ultra raras */
  ultraRareCards: ICard[]
  /** Cartas muy raras */
  veryRareCards: ICard[]
  /** Cartas raras */
  rareCards: ICard[]
  /** Cartas normales */
  normalCards: ICard[]
  /** Cartas básicas */
  basicCards: ICard[]
  group: { key: number, list: ICard[]; }[] = [];

  constructor(
    protected helpers: HelpersService
  ) {
    this.cards = [];
    this.onCardClicked = (card: ICard) => { };

    this.ultraRareCards = [];
    this.veryRareCards = [];
    this.rareCards = [];
    this.normalCards = [];
    this.basicCards = [];
  }

  /**
   * Solicita todas las cartas al servidor y las filtra según su tipo
  */
  ngOnChanges() {
    if (this.cards.length > 0) {
      this.cards.forEach((card) => {
        if (!card.borderColor) {
          card.borderColor = this.helpers.getCardBorderColor(card.type);
        }
      })

      //Agrupar por tipo en un array de objetos { key:number,list: ICard[]; }
      this.group = [...new Set(this.cards.map(x => x.type))].map(x => { return { key: x, list: this.cards.filter(y => y.type == x) } })

      this.ultraRareCards = this.cards.filter(card => card.type === CARD_TYPES.ULTRA_RARE);
      this.veryRareCards = this.cards.filter(card => card.type === CARD_TYPES.VERY_RARE);
      this.rareCards = this.cards.filter(card => card.type === CARD_TYPES.RARE);
      this.normalCards = this.cards.filter(card => card.type === CARD_TYPES.NORMAL);
      this.basicCards = this.cards.filter(card => card.type === CARD_TYPES.BASIC);
    }
  }
}
