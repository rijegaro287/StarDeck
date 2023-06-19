import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IAccount } from '../Interfaces/Account';
import { ICard } from '../Interfaces/Card';
import {IGlobalRanking, IIndividualRanking} from "../Interfaces/Ranking";


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  url = `${apiURL}/Account`;

  constructor(private request: RequestService) { }


  // Para añadir un jugador nuevo
  addUser = (user: IAccount): Promise<any> => {
    return this.request.post(this.url, user);
  }
  // Para añadir una nueva carta
  addCards = (IdUser: string, IdCard: string): Promise<any> => {
    return this.request.post(`${this.url}/addCards/${IdUser}/${IdCard}`, {});
  }
  // Para añadir una conjunto de cartas
  addCardsList = (IdUser: string, IdCard: string[]): Promise<any> => {
    return this.request.post(`${this.url}/addCards/${IdUser}`, IdCard);
  }
  // Para añadir una parametro a la cuenta
  addParameter = (IdUser: string, Name: string, Value: string): Promise<any> => {
    return this.request.post(`${this.url}/${IdUser}/Parameters/${Name}`, Value);
  }
  //Para obtener la coleccion inicial dada
  getAccountCards = (Id: string): Promise<any> => {
    return this.request.get(`${this.url}/${Id}/cards`);
  }

  //Para obtener el ranking personal de un usuario
  getMyPosition = (Id: string): Promise<IIndividualRanking> => {
    return this.request.get(`${this.url}/Ranking/true/${Id}`);
  }

  //Para obtener el ranking global
  getRanking = (Id: string): Promise<IGlobalRanking[]> => {
    return this.request.get(`${this.url}/Ranking/false/${Id}`);
  }
}
