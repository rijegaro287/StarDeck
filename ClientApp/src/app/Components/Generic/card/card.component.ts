import { Component, Input, OnChanges } from '@angular/core';

import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnChanges {
  /** Recibe el color del borde de la carta como entrada */
  @Input() cardBorderColor: string
  /** Recibe un string en base 64 para la imagen de la carta */
  @Input() base64CardImage: string
  /** Recibe la información de la cara como entrada */
  @Input() card: ICard

  imageURL: string;

  constructor(private helpers: HelpersService) {
    this.cardBorderColor = 'black';
    this.base64CardImage = '';
    this.card = {} as ICard;
    this.imageURL = '../../../../assets/images/card.png';
  }

  /** Se ejecuta cuando se detecta que la entrada cambió */
  ngOnChanges(): void {
    if (this.base64CardImage !== '') {
      this.imageURL = this.helpers.base64ToImageURL(this.base64CardImage);
    }
  }
}