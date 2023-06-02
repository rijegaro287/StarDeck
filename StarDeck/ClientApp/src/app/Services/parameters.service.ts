import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IParameter, KV } from '../Interfaces/Parameter';

@Injectable({
  providedIn: 'root'
})
export class ParametersService {
  url = `${apiURL}/api/Parameters`;

  constructor(private request: RequestService) { }

  /**
   * Solicita todos los parámetros al servidor.
   * @returns Una lista con todos los parámetros definidos y su valor.
   */
  getAllParameters = (): Promise<IParameter[]> => {
    return this.request.get(`${this.url}/get_all`);
  }

  /**
   * Solicita un parámetro en específico al servidor.
   * @param parameterKey Nombre del parámetro a solicitar.
   * @returns Un objeto con el nombre y valor del parámetro solicitado.
   */
  getParameter = (parameterKey: string): Promise<IParameter> => {
    return this.request.get(`${this.url}/get/${parameterKey}`);
  }
}
