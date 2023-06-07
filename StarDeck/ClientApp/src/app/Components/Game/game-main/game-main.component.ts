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

  status: string;
  currentTurn: number;
  playingTurn: boolean;

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

    this.status = 'Iniciando partida...'
    this.currentTurn = 0;
    this.playingTurn = false;

    this.selectedCard = null;
  }

  async ngOnInit(): Promise<void> {
    this.gameRoom = JSON.parse(sessionStorage.getItem('GameRoomData')!);
    this.playerID = sessionStorage.getItem('ID')!;

    console.log(this.gameRoom);
    console.log(this.playerID);

    await this.updateGameData();

    while (this.currentTurn < 8) {
      // this.status = 'Iniciando turno...'
      // this.playingTurn = false;

      this.playingTurn = true;
      this.status = `Jugando turno ${this.currentTurn + 1}...`;

      await this.gameService.initTurn(this.gameRoom.roomid, this.playerID)
        .then(async (player) => { console.log('turn ended'); })
        .catch((error) => alert(error.message));

      this.currentTurn++;
      this.playingTurn = false;
      this.status = 'Revelando cartas...';

      await this.updateGameData();
      this.revealCards();
    }

    await this.updateGameData();
  }

  async updateGameData() {
    await this.gameService.getGameRoomData(this.gameRoom.roomid)
      .then((gameRoomInfo) => { this.gameRoom = gameRoomInfo; })
      .catch((error) => alert(error.message));

    this.setPlayersData();

    await this.gameService.getUserGameRoomData(this.playerID, this.gameRoom.roomid)
      .then((playerInfo) => {
        this.playerInfo = playerInfo;
        this.playerInfo.hand = this.playerInfo.hand!.slice();
      })
      .catch((error) => alert(error.message));

    console.log(this.gameRoom);
    console.log(this.playerInfo);
  }

  setPlayersData() {
    if (this.gameRoom.player1.id === this.playerID) {
      this.playerInfo = this.gameRoom.player1;
      this.opponentName = this.gameRoom.player2.nickname;
    }
    else {
      this.playerInfo = this.gameRoom.player2;
      this.opponentName = this.gameRoom.player1.nickname;
    }
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

  onPlanetClicked(planet: IPlanetCards) {
    if (this.playingTurn && this.selectedCard) {
      if (this.playerInfo.energy >= this.selectedCard.energy) {
        this.gameService.placeCard(this.gameRoom.roomid, this.playerID, this.selectedCard.id, planet.index)
          .then((response) => {
            this.playerInfo.energy -= this.selectedCard!.energy;
            this.playerInfo.hand!.splice(this.playerInfo.hand!.indexOf(this.selectedCard!), 1);

            planet.playerCards.push(JSON.parse(JSON.stringify(this.selectedCard)));
            this.selectedCard = null;
            console.log('Card placed');
          })
          .catch((error) => alert(error));
      }
      else {
        alert('No tiene suficiente energía para jugar esta carta')
      }
    }
  }

  onEndTurnClicked() {
    // this.gameService.endTurn(this.gameRoom.roomid, this.playerID)
    //   .then((response) => {
    //     this.playingTurn = false;
    //     this.status = 'Esperando a que el oponente termine su turno...';
    //   })
    //   .catch((error) => alert(error));
  }

  onSurrenderClicked() { }

  revealCards() {
    this.planetsInfo = [];

    if (this.gameRoom.firstToShow.id === this.playerID) {
      setTimeout(() => {
        for (let index = 0; index < this.gameRoom.territories.length; index++) {
          this.planetsInfo.push({
            index: index + 1,
            name: this.gameRoom.territories[index].name,
            opponentCards: [],
            playerCards: this.gameRoom.territories[index].player1Cards!
          });
        }
      }, 2000);

      setTimeout(() => {
        for (let index = 0; index < this.gameRoom.territories.length; index++) {
          this.planetsInfo[index].opponentCards = this.gameRoom.territories[index].player2Cards!;
        }
      }, 4000)
    }
    else {
      setTimeout(() => {
        for (let index = 0; index < this.gameRoom.territories.length; index++) {
          this.planetsInfo.push({
            index: index + 1,
            name: this.gameRoom.territories[index].name,
            opponentCards: this.gameRoom.territories[index].player2Cards!,
            playerCards: []
          });
        }
      }, 2000);

      setTimeout(() => {
        for (let index = 0; index < this.gameRoom.territories.length; index++) {
          this.planetsInfo[index].playerCards = this.gameRoom.territories[index].player1Cards!;
        }
      }, 4000)
    }
  }
}
