import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnChanges {
  /** Recibe el color del borde de la carta como entrada */
  @Input() cardBorderColor: string
  /** Recibe la información de la cara como entrada */
  @Input() card: ICard

  constructor() {
    this.cardBorderColor = 'black'
    this.card = {} as ICard
  }

  /** Se ejecuta cuando se detecta que la entrada cambió */
  ngOnChanges(): void {
    console.log(this.card);
  }
}
