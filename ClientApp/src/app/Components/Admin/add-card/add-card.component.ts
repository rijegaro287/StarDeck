import * as random from "random-web-token";

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { RequestService } from 'src/app/Services/request.service';
import { CardService } from 'src/app/Services/card.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent {
  newCard: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private requestService: RequestService,
    private cardService: CardService
  ) {
    this.newCard = this._formBuilder.group({
      name: ['', Validators.required],
      energy: ['', Validators.required],
      cost: ['', Validators.required],
      type: ['', Validators.required],
      race: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  onSubmit() {
    const fileInput: HTMLInputElement = document.querySelector('#file-input')!;

    const newCard: ICard = {
      id: random.genSync('medium+', 12),
      name: this.newCard.value.name.toString(),
      image: fileInput.files![0],
      energy: Number(this.newCard.value.energy),
      cost: Number(this.newCard.value.cost),
      type: this.newCard.value.type.toString(),
      race: this.newCard.value.race.toString(),
      active: true,
      skillID: 0,
      description: this.newCard.value.description,
    };

    console.log(newCard);

    try {
      this.validateCardForm(newCard);

      this.cardService.addCard(newCard)
        .then(response => {
          console.log(response);
          this.requestService.handleResponse(response);
        });
    } catch (error) {
      alert(error);
    }
  }

  validateCardForm(card: ICard): void {
    Object.keys(card).forEach(key => {
      const value = card[key as keyof ICard];
      if (value === '' || value === null || value === undefined) {
        if (key !== 'image') {
          throw new Error('Todos los campos son obligatorios');
        }
      }
    });

    if (card.name.length < 5 || card.name.length > 30) {
      throw new Error('El nombre de la carta debe tener entre 5 y 30 caracteres');
    }
    if (card.description.length > 1000) {
      throw new Error('La descripción de la carta debe tener como máximo 1000 caracteres');
    }
    if (isNaN(card.energy) || card.energy < -100 || card.energy > 100) {
      throw new Error('La energía de la carta debe ser un número entre -100 y 100');
    }
    if (isNaN(card.energy) || card.cost < 0 || card.cost > 100) {
      throw new Error('El costo de la carta debe ser un número entre 0 y 100');
    }
  }
}
