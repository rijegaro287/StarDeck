import * as Phaser from "phaser";

import { COLORS } from "../Constants";
import { toRGBString } from "../Helpers";

export default class StatusBar extends Phaser.GameObjects.Container {
  private margin: number;
  private energy: number;
  private coins: number;
  private time: number;
  private oponentName: string;
  private fontSize?: number;

  constructor(
    scene: Phaser.Scene,
    x: number,
    y: number,
    width: number,
    height: number,
    energy: number,
    coins: number,
    time: number,
    oponentName: string,
    fontSize?: number
  ) {
    super(scene, x, y);

    this.x = x;
    this.y = y;
    this.width = width;
    this.height = height
    this.margin = 20;

    this.energy = energy;
    this.coins = coins;
    this.time = time;
    this.oponentName = oponentName;
    this.fontSize = fontSize;

    this.setSize(this.width, this.height);
    this.setPosition(this.x, this.y);

    const oponentNameLabel = this.scene.add.text(0, 0, `Oponente: ${this.oponentName}`, {
      fontFamily: 'Exo, sans-serif',
      fontSize: `${this.fontSize ? this.fontSize : 22}px`,
      fontStyle: 'bold',
      color: toRGBString(COLORS.WARNING)
    });
    const oponentNameLabelPositionX = -(this.width - oponentNameLabel.width) / 2;
    oponentNameLabel.setOrigin(0.5, 0.5);
    oponentNameLabel.setPosition(oponentNameLabelPositionX, 0);

    const energyLabel = this.scene.add.text(0, 0, `${this.energy}`, {
      fontFamily: 'Exo, sans-serif',
      fontSize: `${this.fontSize ? this.fontSize : 30}px`,
      fontStyle: 'bold',
      color: toRGBString(COLORS.ENERGY)
    });
    const energyLabelPositionX = (this.width - energyLabel.width) / 2;
    energyLabel.setOrigin(0.5, 0.5);
    energyLabel.setPosition(energyLabelPositionX, 0);

    const energyIcon = this.scene.add.image(0, 0, 'energy-icon');
    const energyIconPositionX = energyLabelPositionX - (energyLabel.width / 2) - (energyIcon.width / 2);
    energyIcon.setPosition(energyIconPositionX, 0);

    const coinsLabel = this.scene.add.text(0, 0, `${this.coins}`, {
      fontFamily: 'Exo, sans-serif',
      fontSize: `${this.fontSize ? this.fontSize : 30}px`,
      fontStyle: 'bold',
      color: toRGBString(COLORS.COINS)
    });
    const coinsLabelPositionX = energyIconPositionX - (energyIcon.width / 2) - (coinsLabel.width / 2) - this.margin;
    coinsLabel.setOrigin(0.5, 0.5);
    coinsLabel.setPosition(coinsLabelPositionX, 0);

    const coinsIcon = this.scene.add.image(0, 0, 'coin-icon');
    const coinsIconPositionX = coinsLabelPositionX - (coinsLabel.width / 2) - (coinsIcon.width / 2) - this.margin / 2;
    coinsIcon.setPosition(coinsIconPositionX, 0);

    const timeLabel = this.scene.add.text(0, 0, `${this.time}`, {
      fontFamily: 'Exo, sans-serif',
      fontSize: `${this.fontSize ? this.fontSize : 30}px`,
      fontStyle: 'bold',
      color: toRGBString(COLORS.WHITE)
    });
    const timeLabelPositionX = coinsIconPositionX - (coinsIcon.width / 2) - (timeLabel.width / 2) - this.margin;
    timeLabel.setOrigin(0.5, 0.5);
    timeLabel.setPosition(timeLabelPositionX, 0);

    const timeIcon = this.scene.add.image(0, 0, 'clock-icon');
    const timeIconPositionX = timeLabelPositionX - (timeLabel.width / 2) - (timeIcon.width / 2) - this.margin / 2;
    timeIcon.setPosition(timeIconPositionX, 0);

    this.add(oponentNameLabel);
    this.add(energyLabel);
    this.add(energyIcon);
    this.add(coinsLabel);
    this.add(coinsIcon);

    this.add(timeLabel);
    this.add(timeIcon);
  }
}