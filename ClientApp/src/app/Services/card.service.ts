import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { ICard } from '../Interfaces/Card';
import { ICardsResponse, IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  url = `${apiURL}/cards`;

  constructor(private request: RequestService) { }

  getAllCards = (): Promise<ICardsResponse> => {
    return this.request.get(`${this.url}/get_all`);
  }

  // Cuando el API esté listo
  // addCard = (card: ICard): Promise<IServerResponse> => {
  //   return this.request.post(`${this.url}/add`, card);
  // }

  addCard = (card: ICard): Promise<IServerResponse> => {
    const response: IServerResponse = {
      status: 'ok',
      message: 'Card created successfully',
    }

    return this.request.falseResponse(response);
  }
}