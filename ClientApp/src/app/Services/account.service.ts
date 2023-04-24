import { Injectable } from '@angular/core';
import { apiURL } from '../app.component';

import { RequestService } from './request.service';

import { IAccount } from '../Interfaces/Account';
import { IServerResponse } from '../Interfaces/ServerResponse';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  url = `${apiURL}/account`;

  constructor(private request: RequestService) { }

  

  addUser = (user: IAccount): Promise<IServerResponse> => {
    const response: IServerResponse = {
      status: 'ok',
      message: 'User created successfully',
    }

    return this.request.falseResponse(response);
  }
}
