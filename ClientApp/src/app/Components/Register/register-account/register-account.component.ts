import * as random from "random-web-token";

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AccountService } from 'src/app/Services/account.service';
import { RequestService } from 'src/app/Services/request.service';

import { IAccount, IDeck } from 'src/app/Interfaces/Account';

@Component({
  selector: 'app-register-account',
  templateUrl: './register-account.component.html',
  styleUrls: ['./register-account.component.css']
})
export class RegisterAccountComponent {
  newUser: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private requestService: RequestService,
    private accountService: AccountService
  ) {
    this.newUser = this._formBuilder.group({
      name: ['', Validators.required],
      nickname: ['', Validators.required],
      email: ['', Validators.required],
      nationality: ['', Validators.required],
      password: ['', Validators.required],
      passwordv: ['', Validators.required],
      term: ['', Validators.required]
    });
  }

  createAccount() {
    const initialDeck: IDeck = {
      idAccount: 'JBCS',
      deck: '{1,2,3,4,5,6}',

    }
   
    const newUser: IAccount = {
      id: 'U-'+ random.genSync('medium+', 12),
      name: this.newUser.value.name.toString(),
      nickname: this.newUser.value.nickname.toString(),
      email: this.newUser.value.email.toString(),
      nationality: this.newUser.value.nationality.toString(),
      password: this.newUser.value.password.toString(),
      avatar: 1,
      active: true,
      config: 'user',
      points: 0,
      coins: 20,
      deck: initialDeck,
      
    };

    console.log(newUser);

    try {
      this.validateAccount(newUser);

      this.accountService.addUser(newUser)
        .then(response => {
          console.log(response);
          this.requestService.handleResponse(response);
        });
    } catch (error) {
      alert(error);
    }
  }

  validateAccount(user: IAccount): void {
    Object.keys(user).forEach(key => {
      const value = user[key as keyof IAccount];
      if (value === '' || value === null || value === undefined) {
        if (key === ' ') {
          throw new Error('Sus datos están incompletos intente de nuevo');
        }
      }
    });

    if (this.newUser.value.password.toString() != this.newUser.value.passwordv.toString() ) {
      throw new Error('Las contraseñas no coinciden verifiquela e intente de nuevo');
    }
  }
}
