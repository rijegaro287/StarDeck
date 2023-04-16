import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'StarDeck';
}

const apiURL = 'http://localhost:8080'

export default AppComponent
export { apiURL }