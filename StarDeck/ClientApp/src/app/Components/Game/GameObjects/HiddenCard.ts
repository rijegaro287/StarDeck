import * as Phaser from "phaser";

import { COLORS } from "../Constants";

export default class HiddenCard extends Phaser.GameObjects.Container {
  private margin: number = 15;

  constructor(scene: Phaser.Scene, x: number, y: number) {
    super(scene, x, y);

    this.x = x;
    this.y = y;
    this.width = 200;
    this.height = 350;

    this.setSize(this.width, this.height);
    this.setPosition(this.x, this.y);


    const cardBackground = this.scene.add.rectangle(
      0, 0, this.width, this.height, COLORS.BACKGROUND_1, 1
    );

    const imageSize = this.width - 2 * this.margin;
    const cardImage = this.scene.add.image(0, 0, 'hidden-card');
    cardImage.setDisplaySize(imageSize, imageSize);

    this.add(cardBackground);
    this.add(cardImage);
  }
}