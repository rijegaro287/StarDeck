import { Component, Input, OnChanges } from '@angular/core';

import { INavbarItem } from '../../../Interfaces/Helpers';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  @Input() items: INavbarItem[];

  constructor() {
    this.items = [];
  }
}
