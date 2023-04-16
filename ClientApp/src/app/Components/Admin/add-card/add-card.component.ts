import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
      id: this.generateCardID(),
      name: this.newCard.value.name,
      image: fileInput.files![0],
      energy: this.newCard.value.energy,
      cost: this.newCard.value.cost,
      type: this.newCard.value.type,
      race: this.newCard.value.race,
      active: true,
      skillID: 0,
      description: this.newCard.value.description,
    }

    try {
      this.validateCardForm(newCard)
      // Send to server
    } catch (error) {
      console.log(error);
    }
  }

  generateCardID(): string {
    return 'asdasd'
  }

  validateCardForm(card: ICard): boolean {
    return true;
  }
}