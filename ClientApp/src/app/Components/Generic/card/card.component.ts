import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnChanges {
  @Input() cardBorderColor: string
  @Input() card: ICard

  constructor() {
    this.cardBorderColor = 'black'
    this.card = {} as ICard
  }
  ngOnChanges(): void {
    console.log(this.card);
  }
}
