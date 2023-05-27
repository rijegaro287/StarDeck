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

  /**
   * Solicita la lista de escuadrones de un usuario al servidor.
   * @param userID ID del jugador cuyos escuadrones se solicitan.
   * @returns Una lista con los escuadrones del usuario.
   */
  getUserDecks = (userID: string): Promise<IDeck[]> => {
    return this.request.get(`${this.url}/User/${userID}`);
  }

  /**
   * Agrega un nuevo escuadrón perteneciente a un usuario.
   * @param newDeck Información del escuadrón a agregar.
   * @returns El escuadrón agregado.
   */
  addUserDeck = (newDeck: IDeck): Promise<any> => {
    return this.request.post(`${this.url}`, newDeck);
  }
}
