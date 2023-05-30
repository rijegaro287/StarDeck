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
 * Solicita al servidor la información de todas las cartas activas
 * @returns Una promesa con la respuesta del servidor
 */
  getUserGameRoomData = (userID: string, gameRoomID: string): Promise<IPlayer> => {
    return this.request.get(`${this.url}/getGameRoomData/${gameRoomID}/${userID}`);
  }

  getGameRoomData = (gameRoomID: string): Promise<IGameRoom> => {
    return this.request.get(`${this.url}/getGameRoomData/${gameRoomID}`);
  }

  endTurn = (gameRoomID: string, playerID: string): Promise<any> => {
    return this.request.post(`${this.url}/${gameRoomID}/${playerID}/endTurn`,gameRoomID );
  }

  initTurn = (gameRoomID: string, playerID: string): Promise<any> => {
    return this.request.get(`${this.url}/${gameRoomID}/${playerID}/initTurn`);
  }
}
