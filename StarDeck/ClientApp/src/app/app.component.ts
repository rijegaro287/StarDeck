import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'StarDeck';
}

const apiURL = 'https://localhost:7212'

export default AppComponent
export { apiURL }
