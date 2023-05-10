import { Component, OnInit } from '@angular/core';

import { AccountService } from 'src/app/Services/account.service';
import { CardService } from 'src/app/Services/card.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-deck-list',
  templateUrl: './deck-list.component.html',
  styleUrls: ['./deck-list.component.scss']
})
export class DeckListComponent implements OnInit {
  userID: string;

  cardCollectionIDs: string[];
  cardCollection: ICard[];

  constructor(
    private accountService: AccountService,
    private cardService: CardService,
  ) {
    this.userID = sessionStorage.getItem('ID')!;
    console.log(this.userID)

    this.cardCollectionIDs = [];
    this.cardCollection = [];
  }

  async ngOnInit() {
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
  }
}
