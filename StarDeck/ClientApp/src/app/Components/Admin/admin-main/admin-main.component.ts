import { Component } from '@angular/core';

import { INavbarItem } from 'src/app/Interfaces/Helpers';

@Component({
  selector: 'app-admin-main',
  templateUrl: './admin-main.component.html',
  styleUrls: ['./admin-main.component.scss']
})
export class AdminMainComponent {
  /** Contiene los elementos de la barra de navegación */
  navbarItems: INavbarItem[];

  constructor() {
    this.navbarItems = [
      {
        description: 'Cartas', // Descripción del elemento
        link: '/admin/cards' // Ruta a la que redirige
      },
      {
        description: 'Planetas',
        link: '/admin/planets'
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
