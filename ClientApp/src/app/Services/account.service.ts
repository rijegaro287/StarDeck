import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IAccount } from '../Interfaces/Account';
import { ICard } from '../Interfaces/Card';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  url = `${apiURL}/Account`;

  constructor(private request: RequestService) { }

  
  // Para añadir un jugador nuevo
  addUser = (user: IAccount): Promise<any> => {
    return this.request.post(this.url,user);
  }
  // Para añadir una nueva carta 
  addCards = (IdUser: string, IdCard: string): Promise<any> => {
    return this.request.post(`${this.url}/addCards/${IdUser}`, IdCard);
  }
  //Para obtener la coleccion inicial dada
  cards = (Id:string): Promise<any> => {
    return this.request.get(`${this.url}/${Id}/cards`);
  }
  //Para obtener todas las cartas del juego
  allCards = (): Promise<ICard[]> => {
    return this.request.get(`${apiURL}/cards/get_all`);
  }
  //Para obtener una carta especifica por id 
  getCard = (Id: string): Promise<any> => {
    return this.request.get(`${apiURL}/cards/get/${Id}`);
  }
  


}
