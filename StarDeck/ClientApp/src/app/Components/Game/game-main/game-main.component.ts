import { Component, OnInit } from '@angular/core';

import * as Phaser from 'phaser';

import Card from '../GameObjects/Card';
import CardList from '../GameObjects/CardList';
import HiddenCard from '../GameObjects/HiddenCard';

@Component({
  selector: 'app-game-main',
  templateUrl: './game-main.component.html',
  styleUrls: ['./game-main.component.scss']
})
export class GameMainComponent {
  game: Phaser.Game;
  config: Phaser.Types.Core.GameConfig;

  playableWidth: number;
  playableHeight: number;

  constructor() {
    this.playableWidth = window.innerWidth;
    this.playableHeight = window.innerHeight;

    this.config = {
      type: Phaser.AUTO,
      width: this.playableWidth,
      height: this.playableHeight,
      scene: [MainScene]
    }

    this.game = new Phaser.Game(this.config);
  }
}

class MainScene extends Phaser.Scene {
  margin: number;
  playableWidth: number;
  playableHeight: number;

  constructor() {
    super({ key: 'MainScene' });

    this.margin = 0;
    this.playableWidth = 0;
    this.playableHeight = 0;
  }

  preload() {
    this.margin = 10;
    this.playableWidth = Number(this.sys.game.canvas.width) - 2 * this.margin;
    this.playableHeight = Number(this.sys.game.canvas.height) - 2 * this.margin;

    this.load.image('default-card', '../../../assets/images/card.png');
    this.load.image('hidden-card', '../../../assets/images/logo.png');
    this.load.image('energy-icon', '../../../assets/svg/energy-icon.svg');
    this.load.image('battle-cost-icon', '../../../assets/svg/battle-cost-icon.svg');
  }

  create() {
    const card1 = new Card(this, 0, 0, 'Carta 1', 'Raza 1', 50, 75);
    const card2 = new Card(this, 0, 0, 'Carta 1', 'Raza 1', 50, 75);
    const card3 = new Card(this, 0, 0, 'Carta 1', 'Raza 1', 50, 75);
    const card4 = new Card(this, 0, 0, 'Carta 1', 'Raza 1', 50, 75);
    const card5 = new Card(this, 0, 0, 'Carta 1', 'Raza 1', 50, 75);
    const card6 = new Card(this, 0, 0, 'Carta 1', 'Raza 1', 50, 75);

    const playerHand = [card1, card2, card3, card4, card5, card6];

    const playerHandWidth = this.playableWidth * 0.92;
    const playerHandHeight = this.playableHeight * 0.25;
    const playerHandY = this.margin + this.playableHeight - playerHandHeight / 2;
    const playerHandX = playerHandWidth / 2 + this.margin;
    const playerHandContainer = new CardList(
      this, playerHandX, playerHandY, playerHandWidth, playerHandHeight, playerHand
    );

    const playerDeck = new HiddenCard(this, 0, 0);
    const playerDeckHeight = playerDeck.height + 2 * this.margin;
    const playerDeckY = playerHandY
    const playerDeckX = playerHandWidth + 2 * this.margin + this.playableWidth * 0.08 / 2;

    playerDeck.scale = playerHandHeight / playerDeckHeight
    playerDeck.setPosition(playerDeckX, playerDeckY)

    this.add.existing(playerHandContainer);
    this.add.existing(playerDeck);
  }

  update() {
  }
}