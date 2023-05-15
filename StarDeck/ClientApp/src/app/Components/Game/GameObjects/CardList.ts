import * as Phaser from "phaser";

import Card from "./Card";

import { COLORS } from "../Constants";

export default class CardList extends Phaser.GameObjects.Container {
  private cardList: Card[];
  private margin: number;

  constructor(
    scene: Phaser.Scene,
    x: number,
    y: number,
    width: number,
    height: number,
    cardList: Card[]
  ) {
    super(scene, x, y);

    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;
    this.cardList = cardList;

    this.margin = 6;

    this.setSize(this.width, this.height);
    this.setPosition(this.x, this.y);

    const cardBackground = this.scene.add.rectangle(
      0, 0, this.width, this.height, COLORS.BACKGROUND_0, 1
    );

    const gridHeight = this.height - 2 * this.margin;
    const gridWidth = this.width - 2 * this.margin;

    this.cardList.forEach((card, index) => {
      const scaleRatio = gridHeight / card.height;
      const scaledCardWidth = card.width * scaleRatio;

      const cardsWidth = scaledCardWidth * this.cardList.length + this.margin * (this.cardList.length - 1);
      const cardsPositionX = (cardsWidth - scaledCardWidth) / 2 - index * (scaledCardWidth + this.margin);

      card.setPosition(-cardsPositionX, 0);
      card.scale = scaleRatio;
    });

    this.add(cardBackground);
    this.add(cardList);
  }
}