import * as Phaser from "phaser";

import { COLORS } from "../Constants";
import { toRGBString } from "../Helpers";

export default class Button extends Phaser.GameObjects.Container {
  private label: string;
  private color: number;
  private fontSize?: number;

  constructor(
    scene: Phaser.Scene,
    x: number,
    y: number,
    width: number,
    height: number,
    label: string,
    color: number,
    fontSize?: number
  ) {
    super(scene, x, y);

    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height;

    this.label = label;
    this.fontSize = fontSize;
    this.color = color;

    this.setSize(this.width, this.height);
    this.setPosition(this.x, this.y);

    const buttonBackground = this.scene.add.rectangle(
      0, 0, this.width, this.height, this.color, 1
    );

    const buttonLabel = this.scene.add.text(0, 0, this.label, {
      fontFamily: 'Exo, sans-serif',
      fontSize: `${this.fontSize ? this.fontSize : this.width / 10}px`,
      fontStyle: 'bold',
      color: toRGBString(COLORS.WHITE)
    });
    buttonLabel.setOrigin(0.5, 0.5);

    this.add(buttonBackground);
    this.add(buttonLabel);
  }
}