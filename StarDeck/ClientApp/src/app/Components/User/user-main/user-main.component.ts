import { Component } from '@angular/core';

import { INavbarItem } from 'src/app/Interfaces/Helpers';

@Component({
  selector: 'app-user-main',
  templateUrl: './user-main.component.html',
  styleUrls: ['./user-main.component.scss']
})
export class UserMainComponent {
  /** Contiene los elementos de la barra de navegación */
  navbarItems: INavbarItem[];

  constructor() {
    this.navbarItems = [
      {
        description: 'Escuadron', // Descripción del elemento
        link: '/user/decks' // Ruta a la que redirige
      },
      {
        description: 'Coleccion',
        link: '/'
      },
      {
        description: 'Configuración',
        link: '/'
      }
    ]
  }

  /**
    Se ejecuta al hacer click
    sobre el botón de jugar
  */
  onPlayClicked() {
    console.log('Play clicked');

  }
}
