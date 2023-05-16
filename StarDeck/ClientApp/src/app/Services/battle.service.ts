import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { ICard } from '../Interfaces/Card';


@Injectable({
  providedIn: 'root'
})
export class BattleService {
  url = `${apiURL}/battle`;

  constructor(private request: RequestService) { }
  //Para obtener la lista de decks de un jugador(Get)
  decks = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/${Id}/cards`);
  }

  //Para obtener las cartas de un deck a partir del  Id(GET)
  cardsofdeck = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/${Id}/cards`);
  }

  //Para solicitar un partida (POST)
  // search_battle = (deck: string): Promise<any> => {
  //   return this.request.post(this.url, user);
  // }

}
