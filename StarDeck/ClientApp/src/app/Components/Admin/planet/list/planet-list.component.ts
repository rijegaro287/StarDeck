import { Component, OnInit } from '@angular/core';
import { MatDialog } from "@angular/material/dialog";


import { PlanetService } from 'src/app/Services/planet.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { IPlanet } from 'src/app/Interfaces/Planet';
import { CreatePlanetComponent } from '../create/create-planet.component';

@Component({
  selector: 'app-planet-list',
  templateUrl: './planet-list.component.html',
  styleUrls: ['./planet-list.component.scss']
})
export class PlanetListComponent implements OnInit {
  /** Contiene todas las cartas recibidas del servidor */
  planets: IPlanet[]

  
  rarePlanets: IPlanet[]
  popularPlanets: IPlanet[]
  basicPlanets: IPlanet[]

  constructor(
    private dialog: MatDialog,
    private planetService: PlanetService,
    protected helpers: HelpersService
  ) {
    this.planets = []
    this.rarePlanets = []
    this.popularPlanets = []
    this.basicPlanets= []
  }

  /** 
   * Solicita todas las cartas al servidor 
   * y las filtra según su tipo
  */
  ngOnInit() {
    this.planetService.getAllPlanet()
      .then((planet) => {
        this.rarePlanets = planet.filter(planet => planet.type === 0);
        this.popularPlanets = planet.filter(planet => planet.type === 2);
        this.basicPlanets = planet.filter(planet => planet.type === 1);
      });
  }

  /** Abre el formulario de creación de carta*/
  createPlanet() {
    const dialogRef = this.dialog.open(CreatePlanetComponent);
  }
}
