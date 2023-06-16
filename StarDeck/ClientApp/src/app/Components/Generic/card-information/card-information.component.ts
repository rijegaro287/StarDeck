import {Component, Inject, Input, OnChanges} from '@angular/core';

import { CARD_TYPES } from 'src/app/app.component';

import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';

@Component({
  selector: 'app-card-info',
  templateUrl: './card-information.component.html',
  styleUrls: ['./card-information.component.scss']
})
export class CardInformationComponent  {

  card: ICard

  baseurl: string;
  constructor(protected helpers: HelpersService, @Inject('BASE_URL') baseUrl: string, ) {

    this.baseurl = "";

    this.card = {
      id: "C-58741236852",
      name: "Nombre de la carta",
      description: "Descripción de la carta",
      race:"Raza 1",
      energy: 0,
      type: CARD_TYPES.BASIC,
      image: "",
      battlecost: 0
    }

  }

}
