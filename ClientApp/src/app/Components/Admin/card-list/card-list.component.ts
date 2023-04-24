import { Component, OnInit } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";

import { CardFormDialogComponent } from "src/app/Components/Dialogs/card-form-dialog/card-form-dialog.component";

import { CardService } from 'src/app/Services/card.service';

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
    public dialog: MatDialog,
    private cardService: CardService,
  ) {
    this.cards = [
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 4,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 4,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 4,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 4,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 4,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 3,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 2,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 1,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
      {
        id: 'C-djieijdejidj',
        name: 'Nombre de la carta',
        // image: File,
        energy: 123,
        cost: 5000,
        type: 0,
        race: "Raza",
        active: true,
        skillID: 1,
        description: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat explicabo quae eligendi expedita sit ut obcaecati, repellendus porro dolore eos iure iste saepe error facere in et officia maiores.Ab.'
      },
    ]

    this.ultraRareCards = []
    this.veryRareCards = []
    this.rareCards = []
    this.normalCards = []
    this.basicCards = []
  }

  /** 
   * Se filtran las cartas según su tipo
   * Falta conectarlo al api**
  */
  ngOnInit() {
    this.ultraRareCards = this.cards.filter(card => card.type === 4)
    this.veryRareCards = this.cards.filter(card => card.type === 3)
    this.rareCards = this.cards.filter(card => card.type === 2)
    this.normalCards = this.cards.filter(card => card.type === 1)
    this.basicCards = this.cards.filter(card => card.type === 0)

    this.cardService.getAllCards()
      .then(cards => {
        console.log(cards);
      })
  }

  /** Abre el formulario de creación de carta*/
  openDialog() {
    const dialogRef = this.dialog.open(CardFormDialogComponent);
  }
}