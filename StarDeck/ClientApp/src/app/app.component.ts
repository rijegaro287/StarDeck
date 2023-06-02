import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ParametersService} from "./Services/parameters.service";
import {KV} from "./Interfaces/Parameter";
import {RequestService} from "./Services/request.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [ParametersService, HttpClient, RequestService]
})
export class AppComponent {
  title = 'StarDeck';
}

var CARD_TYPES:KV

CARD_TYPES={
  BASIC: 0,
  NORMAL: 1,
  RARE: 2,
  VERY_RARE: 3,
  ULTRA_RARE: 4
}
const apiURL = 'https://localhost:7212'
function updateCardTypes() {
  const xmlHttp = new XMLHttpRequest();
  xmlHttp.onreadystatechange = async function () {
    if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
      console.log(xmlHttp.responseText)
      CARD_TYPES = JSON.parse(xmlHttp.responseText);
      await new Promise(r => setTimeout(r, 20000));
      console.log(CARD_TYPES)
    }
  }
  xmlHttp.open("GET", apiURL+'/api/Parameters/CardsType', true); // true for asynchronous
  xmlHttp.send(null);

}

updateCardTypes();
const PLANET_TYPES = {
}



export default AppComponent
export { apiURL, CARD_TYPES, PLANET_TYPES }
