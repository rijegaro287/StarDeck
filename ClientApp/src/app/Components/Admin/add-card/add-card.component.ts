import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
    console.log(this.newCard.value);
  }
}