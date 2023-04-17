import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { ICard } from '../Interfaces/Card';
import { IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  url = `${apiURL}/card`;

  constructor(private request: RequestService) { }

  // Cuando el API esté listo
  // addCard = (assistant: ICard): Promise<IServerResponse> => {
  //   return this.request.post(`${this.url}/add`, assistant);
  // }

  addCard = (card: ICard): Promise<IServerResponse> => {
    const response: IServerResponse = {
      status: 'ok',
      message: 'Card created successfully',
    }

    return this.request.falseResponse(response);
  }
}