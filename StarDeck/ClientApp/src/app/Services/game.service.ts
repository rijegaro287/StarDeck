import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IGameRoom, IPlayer } from '../Interfaces/Game';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  url = `${apiURL}/api/Game`;

  constructor(private request: RequestService) { }

  /**
  * Solicita al servidor la información de la sala de juego de un jugador
  * @returns Una promesa con la respuesta del servidor
  */
  getUserGameRoomData = (userID: string, gameRoomID: string): Promise<IPlayer> => {
    return this.request.get(`${this.url}/getGameRoomData/${gameRoomID}/${userID}`);
  }

  /**
  * Solicita al servidor la información de la sala de juego de un jugador
  * @returns Una promesa con la respuesta del servidor
  */
  placeCard = (gameRoomID: string, userID: string, cardID: string, planetIndex: number): Promise<IPlayer> => {
    return this.request.post(`${this.url}/getGameRoomData/${gameRoomID}/${userID}/${cardID}/${planetIndex}`, {});
  }

  getGameRoomData = (gameRoomID: string): Promise<IGameRoom> => {
    return this.request.get(`${this.url}/getGameRoomData/${gameRoomID}`);
  }

  initTurn = (gameRoomID: string, playerID: string): Promise<any> => {
    return this.request.get(`${this.url}/${gameRoomID}/${playerID}/initTurn`);
  }

  endTurn = (gameRoomID: string, playerID: string): Promise<any> => {
    return this.request.post(`${this.url}/${gameRoomID}/${playerID}/endTurn`, gameRoomID);
  }

  surrender = (gameRoomID: string, playerID: string): Promise<any> => {
    return this.request.post(`${this.url}/${gameRoomID}/${playerID}/Surrender`, gameRoomID);
  }

  isInGame(playerID: string): Promise<any> {
    return this.request.get(`${this.url}/${playerID}/IsInGame`);
  }
}