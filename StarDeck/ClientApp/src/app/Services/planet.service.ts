import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IPlanet } from '../Interfaces/Planet';
import { IPlanetResponse, IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class PlanetService {
  url = `${apiURL}/planet`;

  constructor(private request: RequestService) { }

  getAllPlanet = (): Promise<IPlanet[]> => {
    return this.request.get(`${this.url}/get_all`);
  }

  addPlanet = (planet: IPlanet): Promise<IServerResponse> => {
    return this.request.post(`${this.url}/add`, planet);
  }

}
