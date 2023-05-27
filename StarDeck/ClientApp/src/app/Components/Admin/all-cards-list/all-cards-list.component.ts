import { Component, OnInit } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";

import { CardFormDialogComponent } from "src/app/Components/Dialogs/card-form-dialog/card-form-dialog.component";

import { CardService } from 'src/app/Services/card.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-all-cards-list',
  templateUrl: './all-cards-list.component.html',
  styleUrls: ['./all-cards-list.component.scss']
})
export class AllCardsListComponent implements OnInit {
  /** Contiene todas las cartas recibidas del servidor */
  cards: ICard[]

  constructor(
    private dialog: MatDialog,
    private cardService: CardService,
    protected helpers: HelpersService
  ) {
    this.cards = []
  }

  /** 
   * Solicita todas las cartas al servidor 
   * y las filtra según su tipo
  */
  ngOnInit() {
    this.cardService.getAllCards()
      .then((cards) => { this.cards = cards; });
  }

  /** Abre el formulario de creación de carta*/
  openDialog() {
    const dialogRef = this.dialog.open(CardFormDialogComponent);
  }
}