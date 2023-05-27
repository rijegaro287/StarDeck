import { Component, Input, OnChanges } from '@angular/core';

import { INavbarItem } from '../../../Interfaces/Helpers';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  /** Recibe los elementos de la barra como entrada */
  @Input() items: INavbarItem[];
  @Input() logoHref: string;

  constructor() {
    this.items = [];
    this.logoHref = '/';
  }
}
