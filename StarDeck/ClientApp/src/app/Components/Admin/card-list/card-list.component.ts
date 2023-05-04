import { Component, OnInit } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";

import { CardFormDialogComponent } from "src/app/Components/Dialogs/card-form-dialog/card-form-dialog.component";

import { CardService } from 'src/app/Services/card.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss']
})
export class CardListComponent implements OnInit {
  /** Contiene todas las cartas recibidas del servidor */
  cards: ICard[]

  ultraRareCards: ICard[]
  veryRareCards: ICard[]
  rareCards: ICard[]
  normalCards: ICard[]
  basicCards: ICard[]

  constructor(
    private dialog: MatDialog,
    private cardService: CardService,
    protected helpers: HelpersService
  ) {
    this.cards = []

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
  ngOnInit() {
    this.cardService.getAllCards()
      .then((cards) => {
        this.ultraRareCards = cards.filter(card => card.type === 4);
        this.veryRareCards = cards.filter(card => card.type === 3);
        this.rareCards = cards.filter(card => card.type === 2);
        this.normalCards = cards.filter(card => card.type === 1);
        this.basicCards = cards.filter(card => card.type === 0);
      });
  }

  /** Abre el formulario de creación de carta*/
  openDialog() {
    const dialogRef = this.dialog.open(CardFormDialogComponent);
  }
}