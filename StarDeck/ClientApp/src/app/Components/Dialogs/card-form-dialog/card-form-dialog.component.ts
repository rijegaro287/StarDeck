import * as random from "random-web-token";

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { CardService } from 'src/app/Services/card.service';
import { RequestService } from 'src/app/Services/request.service';
import { HelpersService } from "src/app/Services/helpers.service";

import { ICard, ICardType } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card-form-dialog',
  templateUrl: './card-form-dialog.component.html',
  styleUrls: ['./card-form-dialog.component.scss']
})
export class CardFormDialogComponent {
  /** Formulario de carta */
  newCard: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private cardService: CardService,
    private helpers: HelpersService
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

  /** Se ejecuta el presionar el botón de crear */
  async onSubmit() {
    /** Selecciona la foto */
    const fileInput: HTMLInputElement = document.querySelector('#file-input')!;
    const fileList: FileList = fileInput.files!;

    const imageString = fileList.length ? await this.helpers.fileToBase64(fileList[0]) : '';

    /** Convierte todos los tipos de dato */
    const newCard: ICard = {
      id: random.genSync('medium+', 12),
      name: this.newCard.value.name.toString(),
      energy: Number(this.newCard.value.energy),
      battlecost: Number(this.newCard.value.cost),
      image: imageString,
      active: true,
      type: Number(this.newCard.value.type) as ICardType,
      // race: this.newCard.value.race.toString(),
      // ability: 0,
      description: this.newCard.value.description
    };

    try {
      /** Valida los datos ingresados */
      this.validateCardForm(newCard);

      /** Envía la petición al servidor para que cree la carta */
      this.cardService.addCard(newCard)
        .then(response => {
          console.log(response);
          window.location.reload();
        });
    } catch (error) {
      /** Muestra una alerta en caso de error */
      alert(error);
    }
  }

  /**
   * Valida que las entradas del formulario sean correctas
   * @param newCard Carta a validar
   * @throws Error si alguna entrada es incorrecta o no está llena
   */
  validateCardForm(newCard: ICard): void {
    Object.keys(newCard).forEach(key => {
      const value = newCard[key as keyof ICard];
      if (value === '' || value === null || value === undefined) {
        if (key !== 'image') {
          throw new Error('Todos los campos son obligatorios');
        }
      }
    });

    if (newCard.name.length < 5 || newCard.name.length > 30) {
      throw new Error('El nombre de la carta debe tener entre 5 y 30 caracteres');
    }
    if (newCard.description.length > 1000) {
      throw new Error('La descripción de la carta debe tener como máximo 1000 caracteres');
    }
    if (isNaN(newCard.energy) || newCard.energy < -100 || newCard.energy > 100) {
      throw new Error('La energía de la carta debe ser un número entre -100 y 100');
    }
    if (isNaN(newCard.energy) || newCard.battlecost < 0 || newCard.battlecost > 100) {
      throw new Error('El costo de la carta debe ser un número entre 0 y 100');
    }
  }
}
