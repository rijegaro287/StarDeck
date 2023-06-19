import { Component, OnInit } from '@angular/core';


import { GameService } from 'src/app/Services/game.service';
import { ParametersService } from 'src/app/Services/parameters.service';
import { HelpersService } from 'src/app/Services/helpers.service';

import { ICard } from 'src/app/Interfaces/Card';
import { IGameRoom, IPlayer } from 'src/app/Interfaces/Game';
import { IPlanetCards } from 'src/app/Interfaces/Planet';

@Component({
  selector: 'app-game-main',
  templateUrl: './game-main.component.html',
  styleUrls: ['./game-main.component.scss']
})
export class GameMainComponent implements OnInit {
  gameRoom: IGameRoom;

  playerID: string;
  playerInfo: IPlayer;
  playerCardsID: 'player1Cards' | 'player2Cards';

  planetsInfo: IPlanetCards[];

  opponentName: string;
  opponentCardsID: 'player1Cards' | 'player2Cards';

  status: string;
  maxTurns: number;
  currentTurn: number;
  playingTurn: boolean;

  selectedCard: ICard | null;

  constructor(
    private gameService: GameService,
    private params: ParametersService,
    protected helpers: HelpersService
  ) {
    this.gameRoom = {} as IGameRoom;

    this.playerID = '';
    this.playerInfo = {} as IPlayer;
    this.playerCardsID = 'player1Cards';

    this.opponentName = '';
    this.opponentCardsID = 'player2Cards';

    this.planetsInfo = [];

    this.status = 'Iniciando partida...'
    this.maxTurns = 0;
    this.currentTurn = 0;
    this.playingTurn = false;

    this.selectedCard = null;
  }

  async ngOnInit(): Promise<void> {
    this.gameRoom = JSON.parse(sessionStorage.getItem('GameRoomData')!);
    this.playerID = sessionStorage.getItem('ID')!;

    console.log(this.gameRoom);
    console.log(this.playerID);

    await this.sleep(2000);

    await this.updateGameData();
    this.setPlanetsData();
    await this.revealCards();

    while (this.currentTurn < 8) {
      this.status = 'Iniciando turno...'
      this.playingTurn = false;
      console.log(this.maxTurns);


      await this.sleep(2000);

      this.playingTurn = true;
      this.status = `Jugando turno ${this.currentTurn + 1}...`;

      await this.gameService.initTurn(this.gameRoom.roomid, this.playerID)
        .then(async (player) => { console.log('Turn ended'); })
        .catch((error) => alert(error.message));

      this.playingTurn = false;
      this.status = 'Revelando cartas...';
      this.currentTurn++;

      await this.updateGameData();
      await this.revealCards();
    }

    await this.sleep(1000);

    this.playingTurn = false;
    this.status = `Determinando ganador...`;
    await this.updateGameData();

    this.showWinner();

    await this.sleep(2000);

    window.location.href = '/winner';
  }

  async updateGameData() {
    await this.params.getParameter('maxTurn')
      .then((maxTurn) => { this.maxTurns = Number(maxTurn.value); })
      .catch((error) => alert(error.message));

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

    this.currentTurn = this.gameRoom.turn!;

    console.log(this.gameRoom);
    console.log(this.playerInfo);
  }

  setPlayersData() {
    if (this.gameRoom.player1.id === this.playerID) {
      this.playerInfo = this.gameRoom.player1;
      this.playerCardsID = 'player1Cards';

      this.opponentName = this.gameRoom.player2.nickname;
      this.opponentCardsID = 'player2Cards';
    }
    else {
      this.playerInfo = this.gameRoom.player2;
      this.playerCardsID = 'player2Cards';

      this.opponentName = this.gameRoom.player1.nickname;
      this.opponentCardsID = 'player1Cards';
    }
  }

  setPlanetsData() {
    for (let index = 0; index < this.gameRoom.territories.length; index++) {
      const planet = this.gameRoom.territories[index];
      this.planetsInfo.push({
        index: index + 1,
        name: planet.name,
        playerCards: [],
        opponentCards: []
      });
    }
  }

  async revealCards() {
    for (let index = 0; index < this.planetsInfo.length; index++) {
      const planet = this.planetsInfo[index];
      planet.playerCards = [];
      planet.opponentCards = [];
    }

    for (let index = 0; index < this.planetsInfo.length; index++) {
      const planet = this.planetsInfo[index];

      const gameRoomPlanet = this.gameRoom.territories[index];
      planet.name = this.gameRoom.territories[index].name;

      if (this.gameRoom.firstToShow.id === this.playerID) {
        await this.sleep(1000);
        planet.playerCards = gameRoomPlanet[this.playerCardsID]!;

        await this.sleep(1000);
        planet.opponentCards = gameRoomPlanet[this.opponentCardsID]!;
      }
      else {
        await this.sleep(1000);
        planet.opponentCards = gameRoomPlanet[this.opponentCardsID]!;

        await this.sleep(1000);
        planet.playerCards = gameRoomPlanet[this.playerCardsID]!;
      }
    }
  }

  showWinner() {
    let winnerName = '';

    if (this.gameRoom.winner === 'Draw') {
      winnerName = 'Empate';
    }
    else {
      winnerName = this.gameRoom.winner === this.playerID ? this.playerInfo.nickname : this.opponentName;
    }

    const planetWinners = this.gameRoom.territories.map((planet, index) => {
      const playerPoints = planet[this.playerCardsID]!.reduce((total, card) => total + card.battlecost, 0);
      const opponentPoints = planet[this.opponentCardsID]!.reduce((total, card) => total + card.battlecost, 0);

      let result = {
        planetName: planet.name,
        winner: '',
        winnerPoints: 0,
      }

      if (playerPoints === opponentPoints) {
        result.winner = 'Empate';
        result.winnerPoints = playerPoints;
      }
      else if (playerPoints > opponentPoints) {
        result.winner = this.playerInfo.nickname;
        result.winnerPoints = playerPoints;
      }
      else {
        result.winner = this.opponentName;
        result.winnerPoints = opponentPoints;
      }

      return result;
    });

    sessionStorage.setItem('gameWinner', JSON.stringify({ winner: winnerName, planetWinners: planetWinners }));
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

  async onPlanetClicked(planet: IPlanetCards) {
    if (this.playingTurn && this.selectedCard) {
      if (this.playerInfo.energy >= this.selectedCard.energy) {
        this.playingTurn = false;

        await this.gameService.placeCard(this.gameRoom.roomid, this.playerID, this.selectedCard.id, planet.index)
          .then((response) => {
            console.log(response);

            this.playerInfo.energy -= this.selectedCard!.energy;
            this.playerInfo.hand!.splice(this.playerInfo.hand!.indexOf(this.selectedCard!), 1);

            planet.playerCards.push(JSON.parse(JSON.stringify(this.selectedCard)));
            this.selectedCard = null;

            this.playingTurn = true;
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
    this.gameService.endTurn(this.gameRoom.roomid, this.playerID)
      .then((response) => { console.log(response); })
      .catch((error) => alert(error));

    this.playingTurn = false;
    this.status = 'Esperando a que el oponente termine su turno...';
  }

  async onSurrenderClicked() {
    this.gameService.surrender(this.gameRoom.roomid, this.playerID)
      .then((response) => { console.log(response); })
      .catch((error) => alert(error));

    await this.sleep(400);
  }

  sleep(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}