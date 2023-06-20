import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameMainComponent } from './game-main.component';
import { IPlanetType } from 'src/app/Interfaces/Planet';
import { ICardType } from 'src/app/Interfaces/Card';

describe('GameMainComponent', () => {
  let component: GameMainComponent;
  let fixture: ComponentFixture<GameMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [GameMainComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(GameMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the component correctly', () => {
    expect(component.playerCardsID).toEqual('player1Cards');
    expect(component.opponentCardsID).toEqual('player2Cards');
    expect(component.planetsInfo).toEqual([]);
    expect(component.status).toEqual('Iniciando partida...');
    expect(component.currentTurn).toEqual(0);
    expect(component.playingTurn).toEqual(false);
    expect(component.selectedCard).toEqual(null);
  });

  let gameRoom = {
    "firstToShow": {
      "id": "U-BFXY9BRHXV2R",
      "name": "jugador2",
    },
    "gamelog": {
      "gameID": "GL-0",
      "log": null
    },
    "roomid": "G-047438f3fa10",
    "player1": {
      "id": "U-XFN47Y1D7W4I",
      "name": "jugador1",
      "nickname": "jugador1",
      "avatar": 0,
      "config": null,
      "points": 0,
      "coins": 20,
      "energy": 0
    },
    "player2": {
      "id": "U-BFXY9BRHXV2R",
      "name": "jugador2",
      "nickname": "jugador2",
      "avatar": 0,
      "config": null,
      "points": 2,
      "coins": 22,
      "energy": 0
    },
    "winner": null,
    "bet": null,
    "turn": 0,
    "territories": [
      {
        "id": "P-SQ1LX5AS3GLE",
        "name": "Planeta de la corporacion ORC",
        "type": 2 as IPlanetType,
        "active": true,
        "image": "",
        "ability": '',
        "description": '',
        "player1Cards": [],
        "player2Cards": []
      },
      {
        "id": "P-YGAOT2OW0J58",
        "name": "Planeta Habitable",
        "type": 0 as IPlanetType,
        "active": true,
        "image": "",
        "ability": '',
        "description": '',
        "player1Cards": [],
        "player2Cards": []
      },
      {
        "id": "0",
        "name": "Oculto",
        "type": 0 as IPlanetType,
        "active": false,
        "image": "",
        "ability": '',
        "description": '',
        "player1Cards": [],
        "player2Cards": []
      }
    ]
  };

  it('Should set the player1 data correctly', () => {
    component.gameRoom = gameRoom;
    component.playerID = gameRoom.player1.id;
    component.setPlayersData();

    expect(component.playerInfo).toEqual(gameRoom.player1);
    expect(component.playerCardsID).toEqual('player1Cards');

    expect(component.opponentName).toEqual(gameRoom.player2.nickname);
    expect(component.opponentCardsID).toEqual('player2Cards');
  });

  it('Should set the player2 data correctly', () => {
    component.gameRoom = gameRoom;
    component.playerID = gameRoom.player2.id;
    component.setPlayersData();

    expect(component.playerInfo).toEqual(gameRoom.player2);
    expect(component.playerCardsID).toEqual('player2Cards');

    expect(component.opponentName).toEqual(gameRoom.player1.nickname);
    expect(component.opponentCardsID).toEqual('player1Cards');
  });

  it('Should set the planets data correctly', () => {
    component.gameRoom = gameRoom;
    component.setPlanetsData();

    expect(component.planetsInfo).toEqual([
      {
        index: 1,
        name: 'Planeta de la corporacion ORC',
        playerCards: [],
        opponentCards: []
      },
      {
        index: 2,
        name: 'Planeta Habitable',
        playerCards: [],
        opponentCards: []
      },
      {
        index: 3,
        name: 'Oculto',
        playerCards: [],
        opponentCards: []
      }
    ]);
  });

  it('should reveal cards correctly', async () => {
    component.gameRoom = {
      "firstToShow": {
        "id": "U-XFN47Y1D7W4I",
        "name": "jugador1",
      },
      "gamelog": {
        "gameID": "GL-0",
        "log": null
      },
      "roomid": "G-047438f3fa10",
      "player1": {
        "id": "U-XFN47Y1D7W4I",
        "name": "jugador1",
        "nickname": "jugador1",
        "avatar": 0,
        "config": null,
        "points": 0,
        "coins": 20,
        "energy": 0
      },
      "player2": {
        "id": "U-BFXY9BRHXV2R",
        "name": "jugador2",
        "nickname": "jugador2",
        "avatar": 0,
        "config": null,
        "points": 2,
        "coins": 22,
        "energy": 0
      },
      "winner": null,
      "bet": null,
      "turn": 0,
      "territories": [
        {
          "id": "P-SQ1LX5AS3GLE",
          "name": "Planeta de la corporacion ORC",
          "type": 2 as IPlanetType,
          "active": true,
          "image": "",
          "ability": '',
          "description": '',
          "player1Cards": [
            {
              "id": "1",
              "name": "Carta 1",
              "description": "Descripción de la carta 1",
              "image": "https://example.com/carta1.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            },
            {
              "id": "2",
              "name": "Carta 2",
              "description": "Descripción de la carta 2",
              "image": "https://example.com/carta2.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            }
          ],
          "player2Cards": [
            {
              "id": "3",
              "name": "Carta 3",
              "description": "Descripción de la carta 3",
              "image": "https://example.com/carta3.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            },
            {
              "id": "4",
              "name": "Carta 4",
              "description": "Descripción de la carta 4",
              "image": "https://example.com/carta4.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            }
          ]
        },
        {
          "id": "P-YGAOT2OW0J58",
          "name": "Planeta Habitable",
          "type": 0 as IPlanetType,
          "active": true,
          "image": "",
          "ability": '',
          "description": '',
          "player1Cards": [
            {
              "id": "5",
              "name": "Carta 5",
              "description": "Descripción de la carta 5",
              "image": "https://example.com/carta5.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            },
            {
              "id": "6",
              "name": "Carta 6",
              "description": "Descripción de la carta 6",
              "image": "https://example.com/carta6.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            }
          ],
          "player2Cards": [
            {
              "id": "7",
              "name": "Carta 7",
              "description": "Descripción de la carta 7",
              "image": "https://example.com/carta7.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            },
            {
              "id": "8",
              "name": "Carta 8",
              "description": "Descripción de la carta 8",
              "image": "https://example.com/carta8.png",
              "energy": 1,
              "battlecost": 1,
              "type": 0
            }
          ]
        },
        {
          "id": "0",
          "name": "Oculto",
          "type": 0 as IPlanetType,
          "active": false,
          "image": "",
          "ability": '',
          "description": '',
          "player1Cards": [],
          "player2Cards": []
        }
      ]
    };

    component.setPlanetsData();
    await component.revealCards();

    expect(component.planetsInfo).toEqual([
      {
        index: 1,
        name: 'Planeta de la corporacion ORC',
        playerCards: [
          {
            "id": "1",
            "name": "Carta 1",
            "description": "Descripción de la carta 1",
            "image": "https://example.com/carta1.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          },
          {
            "id": "2",
            "name": "Carta 2",
            "description": "Descripción de la carta 2",
            "image": "https://example.com/carta2.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          }
        ],
        opponentCards: [
          {
            "id": "3",
            "name": "Carta 3",
            "description": "Descripción de la carta 3",
            "image": "https://example.com/carta3.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          },
          {
            "id": "4",
            "name": "Carta 4",
            "description": "Descripción de la carta 4",
            "image": "https://example.com/carta4.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          }
        ]
      },
      {
        index: 2,
        name: 'Planeta Habitable',
        playerCards: [
          {
            "id": "5",
            "name": "Carta 5",
            "description": "Descripción de la carta 5",
            "image": "https://example.com/carta5.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          },
          {
            "id": "6",
            "name": "Carta 6",
            "description": "Descripción de la carta 6",
            "image": "https://example.com/carta6.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          }
        ],
        opponentCards: [
          {
            "id": "7",
            "name": "Carta 7",
            "description": "Descripción de la carta 7",
            "image": "https://example.com/carta7.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          },
          {
            "id": "8",
            "name": "Carta 8",
            "description": "Descripción de la carta 8",
            "image": "https://example.com/carta8.png",
            "energy": 1,
            "battlecost": 1,
            "type": 0
          }
        ]
      },
      {
        index: 3,
        name: 'Oculto',
        playerCards: [],
        opponentCards: []
      }
    ]);
  });

  it('should set player1 as winner', () => {
    component.gameRoom = {
      "firstToShow": {
        "id": "U-BFXY9BRHXV2R",
        "name": "jugador2",
      },
      "gamelog": {
        "gameID": "GL-0",
        "log": null
      },
      "roomid": "G-047438f3fa10",
      "player1": {
        "id": "U-XFN47Y1D7W4I",
        "name": "jugador1",
        "nickname": "jugador1",
        "avatar": 0,
        "config": null,
        "points": 0,
        "coins": 20,
        "energy": 0
      },
      "player2": {
        "id": "U-BFXY9BRHXV2R",
        "name": "jugador2",
        "nickname": "jugador2",
        "avatar": 0,
        "config": null,
        "points": 2,
        "coins": 22,
        "energy": 0
      },
      "winner": 'U-XFN47Y1D7W4I',
      "bet": null,
      "turn": 0,
      territories: [
        {
          "id": "P-SQ1LX5AS3GLE",
          "name": "Planet 1",
          "type": 2 as IPlanetType,
          "active": true,
          "image": "",
          "ability": '',
          "description": '',
          player1Cards: [
            {
              id: '1',
              name: 'Carta 1',
              description: 'Descripción de la carta 1',
              image: 'https://example.com/carta1.png',
              energy: 1,
              battlecost: 4,
              type: 0,
            },
            {
              id: '2',
              name: 'Carta 2',
              description: 'Descripción de la carta 2',
              image: 'https://example.com/carta2.png',
              energy: 2,
              battlecost: 3,
              type: 1,
            },
          ],
          player2Cards: [
            {
              id: '3',
              name: 'Carta 3',
              description: 'Descripción de la carta 3',
              image: 'https://example.com/carta3.png',
              energy: 1,
              battlecost: 1,
              type: 0,
            },
            {
              id: '4',
              name: 'Carta 4',
              description: 'Descripción de la carta 4',
              image: 'https://example.com/carta4.png',
              energy: 2,
              battlecost: 2,
              type: 1,
            },
          ],
        },
        {
          "id": "P-YGAOT2OW0J58",
          "name": "Planet 2",
          "type": 0 as IPlanetType,
          "active": true,
          "image": "",
          "ability": '',
          "description": '',
          player1Cards: [
            {
              id: '5',
              name: 'Carta 5',
              description: 'Descripción de la carta 5',
              image: 'https://example.com/carta5.png',
              energy: 1,
              battlecost: 1,
              type: 0,
            },
            {
              id: '6',
              name: 'Carta 6',
              description: 'Descripción de la carta 6',
              image: 'https://example.com/carta6.png',
              energy: 2,
              battlecost: 2,
              type: 1,
            },
          ],
          player2Cards: [
            {
              id: '7',
              name: 'Carta 7',
              description: 'Descripción de la carta 7',
              image: 'https://example.com/carta7.png',
              energy: 1,
              battlecost: 1,
              type: 0,
            },
            {
              id: '8',
              name: 'Carta 8',
              description: 'Descripción de la carta 8',
              image: 'https://example.com/carta8.png',
              energy: 2,
              battlecost: 1,
              type: 1,
            },
          ],
        },
      ],
    };

    component.playerID = "U-XFN47Y1D7W4I";
    component.setPlayersData();
    component.setPlanetsData();

    component.showWinner();

    const winner = JSON.parse(sessionStorage.getItem('gameWinner')!);
    expect(winner.winner).toEqual('jugador1');
    expect(winner.planetWinners).toEqual([
      {
        planetName: 'Planet 1',
        winner: 'jugador1',
        winnerPoints: 7,
      },
      {
        planetName: 'Planet 2',
        winner: 'jugador1',
        winnerPoints: 3,
      },
    ]);
  });

  it('should set player2 as winner', () => {
    component.gameRoom = {
      "firstToShow": {
        "id": "U-BFXY9BRHXV2R",
        "name": "jugador2",
      },
      "gamelog": {
        "gameID": "GL-0",
        "log": null
      },
      "roomid": "G-047438f3fa10",
      "player1": {
        "id": "U-XFN47Y1D7W4I",
        "name": "jugador1",
        "nickname": "jugador1",
        "avatar": 0,
        "config": null,
        "points": 0,
        "coins": 20,
        "energy": 0
      },
      "player2": {
        "id": "U-BFXY9BRHXV2R",
        "name": "jugador2",
        "nickname": "jugador2",
        "avatar": 0,
        "config": null,
        "points": 2,
        "coins": 22,
        "energy": 0
      },
      "winner": 'U-BFXY9BRHXV2R',
      "bet": null,
      "turn": 0,
      territories: [
        {
          "id": "P-SQ1LX5AS3GLE",
          "name": "Planet 1",
          "type": 2 as IPlanetType,
          "active": true,
          "image": "",
          "ability": '',
          "description": '',
          player2Cards: [
            {
              id: '1',
              name: 'Carta 1',
              description: 'Descripción de la carta 1',
              image: 'https://example.com/carta1.png',
              energy: 1,
              battlecost: 4,
              type: 0,
            },
            {
              id: '2',
              name: 'Carta 2',
              description: 'Descripción de la carta 2',
              image: 'https://example.com/carta2.png',
              energy: 2,
              battlecost: 3,
              type: 1,
            },
          ],
          player1Cards: [
            {
              id: '3',
              name: 'Carta 3',
              description: 'Descripción de la carta 3',
              image: 'https://example.com/carta3.png',
              energy: 1,
              battlecost: 1,
              type: 0,
            },
            {
              id: '4',
              name: 'Carta 4',
              description: 'Descripción de la carta 4',
              image: 'https://example.com/carta4.png',
              energy: 2,
              battlecost: 2,
              type: 1,
            },
          ],
        },
        {
          "id": "P-YGAOT2OW0J58",
          "name": "Planet 2",
          "type": 0 as IPlanetType,
          "active": true,
          "image": "",
          "ability": '',
          "description": '',
          player2Cards: [
            {
              id: '5',
              name: 'Carta 5',
              description: 'Descripción de la carta 5',
              image: 'https://example.com/carta5.png',
              energy: 1,
              battlecost: 1,
              type: 0,
            },
            {
              id: '6',
              name: 'Carta 6',
              description: 'Descripción de la carta 6',
              image: 'https://example.com/carta6.png',
              energy: 2,
              battlecost: 2,
              type: 1,
            },
          ],
          player1Cards: [
            {
              id: '7',
              name: 'Carta 7',
              description: 'Descripción de la carta 7',
              image: 'https://example.com/carta7.png',
              energy: 1,
              battlecost: 1,
              type: 0,
            },
            {
              id: '8',
              name: 'Carta 8',
              description: 'Descripción de la carta 8',
              image: 'https://example.com/carta8.png',
              energy: 2,
              battlecost: 1,
              type: 1,
            },
          ],
        },
      ],
    };

    component.playerID = "U-XFN47Y1D7W4I";
    component.setPlayersData();
    component.setPlanetsData();

    component.showWinner();

    const winner = JSON.parse(sessionStorage.getItem('gameWinner')!);
    expect(winner.winner).toEqual('jugador2');
    expect(winner.planetWinners).toEqual([
      {
        planetName: 'Planet 1',
        winner: 'jugador2',
        winnerPoints: 7,
      },
      {
        planetName: 'Planet 2',
        winner: 'jugador2',
        winnerPoints: 3,
      },
    ]);
  });

  it('should select the clicked card', () => {
    const card1 = {
      id: '1',
      name: 'Card 1',
      description: 'Description of Card 1',
      image: 'https://example.com/card1.png',
      energy: 1,
      battlecost: 1,
      type: 0 as ICardType,
    };

    const card2 = {
      id: '2',
      name: 'Card 2',
      description: 'Description of Card 2',
      image: 'https://example.com/card2.png',
      energy: 2,
      battlecost: 2,
      type: 0 as ICardType,
    };

    component.playingTurn = true;
    // component.selectedCard = null;

    component.onCardClicked(card1);
    expect(component.selectedCard).toEqual(card1);

    component.onCardClicked(card1);
    expect(component.selectedCard).toEqual(null);

    component.onCardClicked(card2);
    expect(component.selectedCard).toEqual(card2);

    component.onCardClicked(card1);
    expect(component.selectedCard).toEqual(card1);
  });
});