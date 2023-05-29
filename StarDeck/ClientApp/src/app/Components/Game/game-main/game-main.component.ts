import { Component, OnInit } from '@angular/core';

import * as Phaser from 'phaser';

import { IGameRoom, IPlayer } from 'src/app/Interfaces/Game';
import { ICard } from 'src/app/Interfaces/Card';


@Component({
  selector: 'app-game-main',
  templateUrl: './game-main.component.html',
  styleUrls: ['./game-main.component.scss']
})
export class GameMainComponent implements OnInit {
  gameRoom: IGameRoom;

  playerID: string;
  playerInfo: IPlayer;

  opponentName: string;

  constructor() {
    this.gameRoom = {} as IGameRoom;

    this.playerID = '';
    this.playerInfo = {} as IPlayer;

    this.opponentName = '';
  }

  ngOnInit(): void {
    this.gameRoom = JSON.parse(sessionStorage.getItem('GameRoomData')!);
    this.playerID = sessionStorage.getItem('ID')!;

    console.log(this.gameRoom);
    console.log(this.playerID);

    if (this.gameRoom.player1.id === this.playerID) {
      this.playerInfo = this.gameRoom.player1;
      this.opponentName = this.gameRoom.player2.nickname;
    }
    else {
      this.playerInfo = this.gameRoom.player2;
      this.opponentName = this.gameRoom.player1.nickname;
    }
  }

}

// class MainScene extends Phaser.Scene {
// gameRoom: IGameRoom;

// playerID: string;
// playerInfo: IPlayer;
// playerHand: Card[];

// opponentName: string;

// margin: number;
// playableWidth: number;
// playableHeight: number;

// constructor() {
//   super({ key: 'MainScene' });

//   this.gameRoom = {} as IGameRoom;

//   this.playerID = '';
//   this.playerInfo = {} as IPlayer;
//   this.playerHand = [];

//   this.opponentName = '';

//   this.margin = 0;
//   this.playableWidth = 0;
//   this.playableHeight = 0;
// }

// preload() {
// this.gameRoom = JSON.parse(sessionStorage.getItem('GameRoomData')!);

// this.playerID = sessionStorage.getItem('ID')!;
// if (this.gameRoom.player1.id === this.playerID) {
//   this.playerInfo = this.gameRoom.player1;
//   this.opponentName = this.gameRoom.player2.nickname;
// }
// else {
//   this.playerInfo = this.gameRoom.player2;
//   this.opponentName = this.gameRoom.player1.nickname;
// }

// this.margin = 10;
// this.playableWidth = Number(this.sys.game.canvas.width) - 2 * this.margin;
// this.playableHeight = Number(this.sys.game.canvas.height) - 2 * this.margin;

// this.load.image('main-bg', '../../../assets/images/main-bg.jpg');
// this.load.image('default-card', '../../../assets/images/card.png');
// this.load.image('hidden-card', '../../../assets/images/logo.png');
// this.load.image('energy-icon', '../../../assets/svg/energy-icon.svg');
// this.load.image('battle-cost-icon', '../../../assets/svg/battle-cost-icon.svg');
// this.load.image('coin-icon', '../../../assets/svg/coin-icon.svg');
// this.load.image('clock-icon', '../../../assets/svg/clock-icon.svg');
// }

// async create() {
// await getUserGameRoomData(this.playerID, this.gameRoom.roomid)
//   .then((response) => response.json())
//   .then((gameRoomData) => this.playerInfo = gameRoomData);

// this.playerHand = this.playerInfo.hand!.map((card: any) => {
//   return new Card(
//     this,
//     0,
//     0,
//     card.name,
//     'Raza',
//     card.energy,
//     card.battlecost,
//     card.image
//   );
// });

// console.log(this.gameRoom);
// console.log(this.playerInfo.hand);
// console.log(this.playerHand);

// const backgroundImage = this.add.image(0, 0, 'main-bg');
// backgroundImage.setDisplaySize(Number(this.sys.game.canvas.width), Number(this.sys.game.canvas.height));
// backgroundImage.setOrigin(0, 0);


// const playerHandWidth = this.playableWidth * 0.92;
// const playerHandHeight = this.playableHeight * 0.25;
// const playerHandY = this.margin + this.playableHeight - playerHandHeight / 2;
// const playerHandX = playerHandWidth / 2 + this.margin;
// const playerHandContainer = new CardList(
//   this,
//   playerHandX,
//   playerHandY,
//   playerHandWidth,
//   playerHandHeight,
//   this.playerHand
// );

// const playerDeck = new HiddenCard(this, 0, 0);
// const playerDeckHeight = playerDeck.height + 2 * this.margin;
// const playerDeckY = playerHandY
// const playerDeckX = playerHandWidth + 1.5 * this.margin + this.playableWidth * 0.08 / 2;

// playerDeck.scale = playerHandHeight / playerDeckHeight
// playerDeck.setPosition(playerDeckX, playerDeckY)

// const endTurnButton = new Button(
//   this, 0, 0, this.playableWidth / 10, 35, 'Terminar turno', COLORS.PRIMARY
// );
// const endTurnButtonPositionX = this.playableWidth - endTurnButton.width / 2;
// const endTurnButtonPositionY = this.playableHeight + 2 * this.margin - playerHandHeight - endTurnButton.height;
// endTurnButton.setPosition(endTurnButtonPositionX, endTurnButtonPositionY)

// const surrenderButton = new Button(
//   this, 0, 0, this.playableWidth / 10, 35, 'Rendición', COLORS.WARNING
// );
// const surrenderButtonPositionX = surrenderButton.width / 2 + this.margin;
// const surrenderButtonPositionY = this.playableHeight + 2 * this.margin - playerHandHeight - surrenderButton.height;
// surrenderButton.setPosition(surrenderButtonPositionX, surrenderButtonPositionY)

// const statusBar = new StatusBar(
//   this,
//   0,
//   0,
//   this.playableWidth,
//   this.playableHeight * 0.08,
//   10,
//   this.playerInfo.coins,
//   20,
//   this.opponentName
// );
// const statusBarPositionX = statusBar.width / 2 + this.margin;
// const statusBarPositionY = statusBar.height / 2 + this.margin;
// statusBar.setPosition(statusBarPositionX, statusBarPositionY);

// const card7 = new Card(this, 0, 0, 'Carta 7', 'Raza 1', 50, 75);
// const card8 = new Card(this, 0, 0, 'Carta 8', 'Raza 1', 50, 75);
// const card9 = new Card(this, 0, 0, 'Carta 9', 'Raza 1', 50, 75);
// const card10 = new Card(this, 0, 0, 'Carta 10', 'Raza 1', 50, 75);
// const card11 = new Card(this, 0, 0, 'Carta 11', 'Raza 1', 50, 75);
// const card12 = new Card(this, 0, 0, 'Carta 12', 'Raza 1', 50, 75);

// const card13 = new Card(this, 0, 0, 'Carta 13', 'Raza 1', 50, 75);
// const card14 = new Card(this, 0, 0, 'Carta 14', 'Raza 1', 50, 75);
// const card15 = new Card(this, 0, 0, 'Carta 15', 'Raza 1', 50, 75);
// const card16 = new Card(this, 0, 0, 'Carta 16', 'Raza 1', 50, 75);
// const card17 = new Card(this, 0, 0, 'Carta 17', 'Raza 1', 50, 75);

// const card18 = new Card(this, 0, 0, 'Carta 18', 'Raza 1', 50, 75);
// const card19 = new Card(this, 0, 0, 'Carta 19', 'Raza 1', 50, 75);
// const card20 = new Card(this, 0, 0, 'Carta 20', 'Raza 1', 50, 75);

// const planet1 = {
//   planetName: this.gameRoom.territories[0].name,
//   playerCards: [card7, card8, card9],
//   opponentCards: [card10, card11, card12]

// };

// const planet2 = {
//   planetName: this.gameRoom.territories[1].name,
//   playerCards: [card13, card14],
//   opponentCards: [card15, card16, card17]
// };

// const planet3 = {
//   planetName: this.gameRoom.territories[2].name,
//   playerCards: [card18, card19],
//   opponentCards: [card20]
// };

// const planetList = [planet1, planet2, planet3];

// const planetContainer = new PlanetContainer(
//   this, 0, 0, this.playableWidth - this.margin, this.playableHeight * 0.55, planetList
// );
// const planetContainerPositionX = planetContainer.width / 2 + this.margin;
// const planetContainerPositionY = planetContainer.height / 2 + this.margin + statusBar.height + this.playableHeight * 0.025;
// planetContainer.setPosition(planetContainerPositionX, planetContainerPositionY);

// this.add.existing(playerHandContainer);
// this.add.existing(playerDeck);
// this.add.existing(endTurnButton);
// this.add.existing(surrenderButton);
// this.add.existing(statusBar);
// this.add.existing(planetContainer);
// }

// update() {
// }
// }
