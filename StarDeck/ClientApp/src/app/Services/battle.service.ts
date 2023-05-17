import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';




@Injectable({
  providedIn: 'root'
})
export class BattleService {
  url = `${apiURL}`;

  constructor(private request: RequestService) { }

  //Para obtener la lista de decks de un jugador(Get)
  decks = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/api/Deck/Names/${Id}`);
    //return this.request.get(`${this.url}/api/Deck/Names/U-RXF7RJNBWEKD`);
  }

  //Para obtener las cartas de un deck a partir del  Id(GET)
  cardsofdeck = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/api/Deck/${Id}`);
    
  }

  //Poner el deck como favorito
  favoritedeck = (Id: string, idDeck:string): Promise<any> => {
    return this.request.put(`${this.url}/Account/${Id}/favorite/${idDeck}`, idDeck);
  }

  //Para solicitar un partida (POST)
  search_battle = (playerId: string): Promise<any> => {
    return this.request.post(`${this.url}/api/Game/${playerId}`, playerId);
  }

  //Poner el deck como favorito
  cancel = (Id: string): Promise<any> => {
    return this.request.put(`${this.url}/api/Game/${Id}/false`, Id);

  }
  



}
