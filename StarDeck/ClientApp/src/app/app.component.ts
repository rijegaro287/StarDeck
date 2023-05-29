import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'StarDeck';
}

const COLORS = {
  PRIMARY: 0x0366d6,
  SECONDARY: 0x69f0ae,
  WARNING: 0xF44336,
  BACKGROUND_0: 0x303030,
  BACKGROUND_1: 0x434343,
  BACKGROUND_2: 0x696969,
  ENERGY: 0x6988f0,
  COINS: 0xFFDF00,
  WHITE: 0xffffff
}

const CARD_TYPES = {
  BASIC: 0,
  NORMAL: 1,
  RARE: 2,
  VERY_RARE: 3,
  ULTRA_RARE: 4
}

const PLANET_TYPES = {
}

const apiURL = 'https://localhost:7212'

export default AppComponent
export { apiURL, COLORS, CARD_TYPES, PLANET_TYPES }
