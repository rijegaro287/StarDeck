import { Component, OnInit } from '@angular/core';


import { GameService } from 'src/app/Services/game.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';
import { IGameRoom, IPlayer } from 'src/app/Interfaces/Game';
import { IPlanet, IPlanetCards } from 'src/app/Interfaces/Planet';

@Component({
  selector: 'app-game-main',
  templateUrl: './game-main.component.html',
  styleUrls: ['./game-main.component.scss']
})
export class GameMainComponent implements OnInit {
  gameRoom: IGameRoom;

  playerID: string;
  playerInfo: IPlayer;

  planetsInfo: IPlanetCards[];

  opponentName: string;

  playingTurn: boolean;
  turnTime: number;

  selectedCard: ICard | null;

  constructor(
    private gameService: GameService,
    protected helpers: HelpersService
  ) {
    this.gameRoom = {} as IGameRoom;

    this.playerID = '';
    this.playerInfo = {} as IPlayer;

    this.opponentName = '';
    this.planetsInfo = [];

    this.turnTime = 20;
    this.playingTurn = true;

    this.selectedCard = null;
  }

  async ngOnInit(): Promise<void> {
    this.gameRoom = JSON.parse(sessionStorage.getItem('GameRoomData')!);
    this.playerID = sessionStorage.getItem('ID')!;

    console.log(this.gameRoom);
    console.log(this.playerID);

    this.setPlayersData();

    await this.gameService.getUserGameRoomData(this.playerID, this.gameRoom.roomid)
      .then((playerInfo) => {
        this.playerInfo = playerInfo;
        this.playerInfo.hand = this.playerInfo.hand!.slice();
      })
      .catch((error) => alert(error));

    console.log(this.playerInfo);
  }

    const playerDeck = new HiddenCard(this, 0, 0);
    const playerDeckHeight = playerDeck.height + 2 * this.margin;
    const playerDeckY = playerHandY
    const playerDeckX = playerHandWidth + 1.5 * this.margin + this.playableWidth * 0.08 / 2;

    playerDeck.scale = playerHandHeight / playerDeckHeight
    playerDeck.setPosition(playerDeckX, playerDeckY)

    const endTurnButton = new Button(
      this, 0, 0, this.playableWidth / 10, 35, 'Terminar turno', COLORS.PRIMARY
    );
    const endTurnButtonPositionX = this.playableWidth - endTurnButton.width / 2;
    const endTurnButtonPositionY = this.playableHeight + 2 * this.margin - playerHandHeight - endTurnButton.height;
    endTurnButton.setPosition(endTurnButtonPositionX, endTurnButtonPositionY)

    const surrenderButton = new Button(
      this, 0, 0, this.playableWidth / 10, 35, 'Rendición', COLORS.WARNING
    );
    const surrenderButtonPositionX = surrenderButton.width / 2 + this.margin;
    const surrenderButtonPositionY = this.playableHeight + 2 * this.margin - playerHandHeight - surrenderButton.height;
    surrenderButton.setPosition(surrenderButtonPositionX, surrenderButtonPositionY)

    const statusBar = new StatusBar(
      this,
      0,
      0,
      this.playableWidth,
      this.playableHeight * 0.08,
      10,
      this.playerInfo.coins,
      20,
      this.opponentName
    );
    const statusBarPositionX = statusBar.width / 2 + this.margin;
    const statusBarPositionY = statusBar.height / 2 + this.margin;
    statusBar.setPosition(statusBarPositionX, statusBarPositionY);

    const card7 = new Card(this, 0, 0, 'Carta 7', 'Raza 1', 50, 75);
    const card8 = new Card(this, 0, 0, 'Carta 8', 'Raza 1', 50, 75);
    const card9 = new Card(this, 0, 0, 'Carta 9', 'Raza 1', 50, 75);
    const card10 = new Card(this, 0, 0, 'Carta 10', 'Raza 1', 50, 75);
    const card11 = new Card(this, 0, 0, 'Carta 11', 'Raza 1', 50, 75);
    const card12 = new Card(this, 0, 0, 'Carta 12', 'Raza 1', 50, 75);

        this.selectedCard = card;
      }
      else {
        this.selectedCard.borderColor = this.helpers.getCardBorderColor(this.selectedCard.type);
        this.selectedCard = null;
      }
    }
  }

  setPlayersData() {
    if (this.gameRoom.player1.id === this.playerID) {
      this.playerInfo = this.gameRoom.player1;
      this.opponentName = this.gameRoom.player2.nickname;

      for (let index = 0; index < this.gameRoom.territories.length; index++) {
        this.planetsInfo.push({
          index: index,
          name: this.gameRoom.territories[index].name,
          opponentCards: this.gameRoom.territories[index].player2Cards!,
          playerCards: this.gameRoom.territories[index].player1Cards!
        });
      }
    }
    else {
      this.playerInfo = this.gameRoom.player2;
      this.opponentName = this.gameRoom.player1.nickname;

      for (let index = 0; index < this.gameRoom.territories.length; index++) {
        this.planetsInfo.push({
          index: index,
          name: this.gameRoom.territories[index].name,
          opponentCards: this.gameRoom.territories[index].player1Cards!,
          playerCards: this.gameRoom.territories[index].player2Cards!
        });
      }
    }

  }

  onPlanetClicked(planet: IPlanetCards) {
    if (this.playingTurn && this.selectedCard) {
      this.playerInfo.hand!.splice(this.playerInfo.hand!.indexOf(this.selectedCard), 1);

      planet.playerCards.push(JSON.parse(JSON.stringify(this.selectedCard)));
      this.selectedCard = null;
      console.log(planet);
    }
  }

  onEndTurnClicked() { }

  onSurrenderClicked() { }

}