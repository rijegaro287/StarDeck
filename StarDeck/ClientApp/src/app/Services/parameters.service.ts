import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IParameter } from '../Interfaces/Parameter';

@Injectable({
  providedIn: 'root'
})
export class ParametersService {
  url = `${apiURL}/api/Parameters`;

  constructor(private request: RequestService) { }

  getAllParameters = (): Promise<IParameter[]> => {
    return this.request.get(`${this.url}/get_all`);
  }

  getParameter = (parameterKey: string): Promise<IParameter> => {
    return this.request.get(`${this.url}/get/${parameterKey}`);
  }
}
