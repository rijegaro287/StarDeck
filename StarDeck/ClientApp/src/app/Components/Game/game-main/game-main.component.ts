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

  onCardClicked(card: ICard) {
    if (this.playingTurn) {
      if (this.selectedCard === null) {
        card.borderColor = 'white';
        this.selectedCard = card;
      }
      else if (this.selectedCard.id !== card.id) {
        this.selectedCard.borderColor = this.helpers.getCardBorderColor(this.selectedCard.type);
        card.borderColor = 'white';

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
      if (this.playerInfo.energy >= this.selectedCard.energy) {
        this.gameService.placeCard(this.gameRoom.roomid, this.playerID, this.selectedCard.id, planet.index)
          .then((response) => {
            this.playerInfo.energy -= this.selectedCard!.energy;
            this.playerInfo.hand!.splice(this.playerInfo.hand!.indexOf(this.selectedCard!), 1);

            planet.playerCards.push(JSON.parse(JSON.stringify(this.selectedCard)));
            this.selectedCard = null;
          })
          .catch((error) => alert(error));
      }
      else {
        alert('No tiene suficiente energóa para jugar esta carta')
      }
    }
  }

  onEndTurnClicked() { }

  onSurrenderClicked() { }

}