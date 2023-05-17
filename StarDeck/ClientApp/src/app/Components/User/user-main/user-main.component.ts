import { Component, Inject } from '@angular/core';

import { INavbarItem } from 'src/app/Interfaces/Helpers';

@Component({
  selector: 'app-user-main',
  templateUrl: './user-main.component.html',
  styleUrls: ['./user-main.component.scss']
})
export class UserMainComponent {
  /** Contiene los elementos de la barra de navegación */
  navbarItems: INavbarItem[];
    baseurl: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.baseurl = baseUrl;

    this.navbarItems = [
      {
        description: 'Escuadrón', // Descripción del elemento
        link: '/user/decks' // Ruta a la que redirige
      },
      {
        description: 'Configuración Cuenta',
        link: '/'
      }
    ]
  }

  /**
    Se ejecuta al hacer click
    sobre el botón de jugar
  */
  onPlayClicked() {
    window.location.assign(this.baseurl + "user/battle/select-deck");

  }
}
