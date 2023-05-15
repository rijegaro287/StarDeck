import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IDeck } from '../Interfaces/Deck';

@Injectable({
  providedIn: 'root'
})
export class DeckService {
  url = `${apiURL}/api/Deck`;

  constructor(private request: RequestService) { }

  getUserDecks = (userID: string): Promise<IDeck[]> => {
    return this.request.get(`${this.url}/User/${userID}`);
  }

  addUserDeck = (newDeck: IDeck): Promise<any> => {
    return this.request.post(`${this.url}`, newDeck);
  }
}
