import { Component } from '@angular/core';

import { INavbarItem } from 'src/app/Interfaces/Helpers';

@Component({
  selector: 'app-admin-main',
  templateUrl: './admin-main.component.html',
  styleUrls: ['./admin-main.component.scss']
})
export class AdminMainComponent {
  navbarItems: INavbarItem[];

  constructor() {
    this.navbarItems = [
      {
        description: 'Cartas',
        link: '/'
      },
      {
        description: 'Territorios',
        link: '/'
      },
      {
        description: 'Configuración',
        link: '/'
      }
    ]
  }

  onPlayClicked() {
    console.log('Play clicked');

  }
}
