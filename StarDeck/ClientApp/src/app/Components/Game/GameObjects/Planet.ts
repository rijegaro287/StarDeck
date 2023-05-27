import * as Phaser from "phaser";

import Card from "./Card";
import CardList from "./CardList";

import { COLORS } from "../Constants";
import { toRGBString } from "../Helpers";

export default class Planet extends Phaser.GameObjects.Container {
  private margin: number;
  private planetName: string;
  private playerCards: Card[];
  private opponentCards: Card[];

  constructor(
    scene: Phaser.Scene,
    x: number,
    y: number,
    width: number,
    height: number,
    planetName: string,
    playerCards: Card[],
    opponentCards: Card[]
  ) {
    super(scene, x, y);

    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.margin = 5

    this.planetName = planetName;
    this.playerCards = playerCards;
    this.opponentCards = opponentCards;

    this.setSize(this.width, this.height);
    this.setPosition(this.x, this.y);

    const cardBackground = this.scene.add.rectangle(
      0, 0, this.width, this.height, COLORS.BACKGROUND_0, 1
    );

    const playerCardsList = new CardList(
      this.scene, 0, 0, this.width - 2 * this.margin, this.height * 0.45, this.playerCards
    );
    const playerCardsListPositionY = this.height * 0.40 / 2 + this.height * 0.05;
    playerCardsList.setPosition(0, playerCardsListPositionY);

    const opponentCardsList = new CardList(
      this.scene, 0, 0, this.width - 2 * this.margin, this.height * 0.40, this.opponentCards
    );
    opponentCardsList.setPosition(0, -playerCardsListPositionY + this.height * 0.05);

    const planetNameLabel = this.scene.add.text(0, 0, this.planetName, {
      fontFamily: 'Exo, sans-serif',
      fontSize: `20px`,
      fontStyle: 'bold',
      color: toRGBString(COLORS.WHITE)
    });
    const planetNameLabelPositionX = -this.width / 2 + this.margin + planetNameLabel.width / 2;
    const planetNameLabelPositionY = -this.height / 2 + 3 * this.margin;
    planetNameLabel.setPosition(planetNameLabelPositionX, planetNameLabelPositionY);
    planetNameLabel.setOrigin(0.5, 0.5);

    const separationLine = this.scene.add.line(
      0, this.margin, 0, 0, this.width, 0, COLORS.BACKGROUND_2, 1
    );

    this.add(cardBackground);
    this.add(playerCardsList);
    this.add(opponentCardsList);
    this.add(planetNameLabel);
    this.add(separationLine);
  }
}