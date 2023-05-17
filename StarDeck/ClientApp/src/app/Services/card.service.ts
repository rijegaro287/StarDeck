import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { ICard } from '../Interfaces/Card';
import { IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  url = `${apiURL}/cards`;

  constructor(private request: RequestService) { }

  getAllCards = (): Promise<ICard[]> => {
    return this.request.get(`${this.url}/get_all`);
  }

  getCard = (cardID: string): Promise<any> => {
    return this.request.get(`${this.url}/get/${cardID}`);
  }

  addCard = (card: ICard): Promise<IServerResponse> => {
    return this.request.post(`${this.url}/add`, card);
  }

}