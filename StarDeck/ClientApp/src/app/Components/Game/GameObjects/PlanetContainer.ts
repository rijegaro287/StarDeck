import * as Phaser from "phaser";

import Planet from "./Planet";

import { COLORS } from "../Constants";
import Card from "./Card";

export default class PlanetContainer extends Phaser.GameObjects.Container {
  private planetList: any[];
  private margin: number;

  constructor(
    scene: Phaser.Scene,
    x: number,
    y: number,
    width: number,
    height: number,
    planetList: any[]
  ) {
    super(scene, x, y);

    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.margin = 6;

    this.planetList = planetList;

    this.setSize(this.width, this.height);
    this.setPosition(this.x, this.y);

    const planetListBackground = this.scene.add.rectangle(
      0, 0, this.width, this.height, COLORS.BACKGROUND_1, 1
    );

    const gridHeight = this.height - 2 * this.margin;
    const gridWidth = this.width - 2 * this.margin;
    const cellWidth = gridWidth / this.planetList.length;

    const planetComponents = this.planetList.map((planet, index) => {
      const planetPositionX = -gridWidth / 2 + cellWidth / 2 + index * cellWidth;
      const planetWidth = cellWidth - this.margin;

      return new Planet(
        this.scene,
        planetPositionX,
        0,
        planetWidth,
        gridHeight,
        planet.planetName,
        planet.playerCards,
        planet.opponentCards
      );
    });

    this.add(planetListBackground);
    this.add(planetComponents);
  }
}