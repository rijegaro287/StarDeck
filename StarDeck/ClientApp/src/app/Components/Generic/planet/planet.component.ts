import { Component, Input, OnChanges } from '@angular/core';

import { HelpersService } from 'src/app/Services/helpers.service';

import { IPlanet } from 'src/app/Interfaces/Planet';

@Component({
  selector: 'app-planet',
  templateUrl: './planet.component.html',
  styleUrls: ['./planet.component.scss']
})
export class PlanetComponent implements OnChanges {
  /** Recibe el color del borde del planeta como entrada */
  @Input() planetBorderColor: string
  /** Recibe un string en base 64 para la imagen del planeta */
  @Input() base64PlanetImage: string
  /** Recibe la información del planeta como entrada */
  @Input() planet: IPlanet

  imageURL: string;

  constructor(private helpers: HelpersService) {
    this.planetBorderColor = 'black';
    this.base64PlanetImage = '';
    this.planet = {} as IPlanet;
    this.imageURL = '../../../../assets/images/planet.png';
  }

  /** Se ejecuta cuando se detecta que la entrada cambió */
  ngOnChanges(): void {
    if (this.base64PlanetImage !== '') {
      this.imageURL = this.helpers.base64ToImageURL(this.base64PlanetImage);
    }
  }
}
