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

  /**
   * Solicita al servidor la información de todas las cartas activas
   * @returns Una promesa con la respuesta del servidor
   */
  getAllCards = (): Promise<ICard[]> => {
    return this.request.get(`${this.url}/get_all`);
  }

  /**
   * Solicita al servidor la información de una carta por medio de su ID
   * @param cardID El ID de la carta a obtener
   * @returns Una promesa con la respuesta del servidor
   */
  getCard = (cardID: string): Promise<any> => {
    return this.request.get(`${this.url}/get/${cardID}`);
  }

  /**
   * Solicita la creación de una nueva carta al servidor
   * @param card La información de la nueva carta
   * @returns Una promesa con la respuesta del servidor
   */
  addCard = (card: ICard): Promise<IServerResponse> => {
    return this.request.post(`${this.url}/add`, card);
  }

  /** Obtiene 9 cartas aleatorias de entre todas las existentes */
  getNineRandomCards = (): Promise<ICard[]> => {
    return this.request.get(`${this.url}/get/nineCards`);
  }
}