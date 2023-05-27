import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IGameRoom } from '../Interfaces/Game';

@Injectable({
  providedIn: 'root'
})
export class BattleService {
  url = `${apiURL}`;

  constructor(private request: RequestService) { }

  //Para obtener la lista de decks de un jugador(Get)
  getUserDecks = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/api/Deck/Names/${Id}`);
    //return this.request.get(`${this.url}/api/Deck/Names/U-RXF7RJNBWEKD`);
  }

  //Para obtener las cartas de un deck a partir del  Id(GET)
  getDeckCards = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/api/Deck/${Id}`);

  }

  //Poner el deck como favorito
  setUserFavoriteDeck = (Id: string, idDeck: string): Promise<any> => {
    return this.request.put(`${this.url}/Account/${Id}/favorite/${idDeck}`, idDeck);
  }

  //Para solicitar un partida (POST)
  searchBattle = (playerId: string): Promise<IGameRoom> => {
    return this.request.post(`${this.url}/api/Game/${playerId}`, playerId);
  }

  //Poner el deck como favorito
  cancelBattleSearch = (Id: string): Promise<any> => {
    return this.request.put(`${this.url}/api/Game/${Id}/false`, Id);
  }
}
