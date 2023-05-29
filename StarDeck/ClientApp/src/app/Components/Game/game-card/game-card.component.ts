import { Component, Input } from '@angular/core';

import { ICard } from 'src/app/Interfaces/Card';
import { HelpersService } from 'src/app/Services/helpers.service';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {
  /** Recibe el color del borde de la carta como entrada */
  @Input() cardBorderColor: string
  /** Recibe un string en base 64 para la imagen de la carta */
  @Input() base64CardImage: string
  /** Recibe la información de la cara como entrada */
  @Input() card: ICard
  @Input() isPlayingCard: boolean

  imageURL: string;

  constructor(private helpers: HelpersService) {
    this.cardBorderColor = 'black';
    this.base64CardImage = '';
    this.card = {} as ICard;
    this.imageURL = '../../../../assets/images/card.png';
    this.isPlayingCard = false;
  }

  /** Se ejecuta cuando se detecta que la entrada cambió */
  ngOnChanges(): void {
    if (this.base64CardImage !== '') {
      this.imageURL = this.helpers.base64ToImageURL(this.base64CardImage);
    }
    if (!this.cardBorderColor) {
      this.cardBorderColor = this.helpers.getCardBorderColor(this.card.type);
    }
  }
}

