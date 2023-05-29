import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'StarDeck';
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
export { apiURL, CARD_TYPES, PLANET_TYPES }
