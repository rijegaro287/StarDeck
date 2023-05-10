import { ChangeDetectorRef, Component, Input, OnChanges } from '@angular/core';

import { CARD_TYPES } from 'src/app/app.component';

import { CardService } from 'src/app/Services/card.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss']
})
export class CardListComponent implements OnChanges {
  @Input() cards: ICard[]
  @Input() onCardClicked: (card: ICard) => void

  ultraRareCards: ICard[]
  veryRareCards: ICard[]
  rareCards: ICard[]
  normalCards: ICard[]
  basicCards: ICard[]

  constructor(
    protected helpers: HelpersService
  ) {
    this.cards = []
    this.onCardClicked = (card: ICard) => { }

    this.ultraRareCards = []
    this.veryRareCards = []
    this.rareCards = []
    this.normalCards = []
    this.basicCards = []
  }

  /** 
   * Solicita todas las cartas al servidor 
   * y las filtra según su tipo
  */
  ngOnChanges() {
    if (this.cards.length > 0) {
      this.ultraRareCards = this.cards.filter(card => card.type === CARD_TYPES.ULTRA_RARE);
      this.veryRareCards = this.cards.filter(card => card.type === CARD_TYPES.VERY_RARE);
      this.rareCards = this.cards.filter(card => card.type === CARD_TYPES.RARE);
      this.normalCards = this.cards.filter(card => card.type === CARD_TYPES.NORMAL);
      this.basicCards = this.cards.filter(card => card.type === CARD_TYPES.BASIC);
    }
  }
}
