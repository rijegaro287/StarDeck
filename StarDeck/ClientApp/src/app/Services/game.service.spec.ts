import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { GameService } from './game.service';
import { RequestService } from './request.service';
import { apiURL } from '../app.component';
import { IGameRoom, IPlayer } from '../Interfaces/Game';

describe('GameService', () => {
  let service: GameService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GameService, RequestService]
    });
    service = TestBed.inject(GameService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve user game room data', () => {
    const userID = 'user1';
    const gameRoomID = 'room1';
    const playerData: any = {
      id: userID,
      nickname: 'Player 1',
      hand: [],
      energy: 10
    };

    service.getUserGameRoomData(userID, gameRoomID).then((response) => {
      expect(response).toEqual(playerData);
    });

    const req = httpMock.expectOne(`${apiURL}/api/Game/getGameRoomData/${gameRoomID}/${userID}`);
    expect(req.request.method).toBe('GET');
    req.flush(playerData);
  });

  it('should place a card', () => {
    const gameRoomID = 'room1';
    const userID = 'user1';
    const cardID = 'card1';
    const planetIndex = 1;
    const playerData: any = {
      id: userID,
      nickname: 'Player 1',
      hand: [],
      energy: 5
    };

    service.placeCard(gameRoomID, userID, cardID, planetIndex).then((response) => {
      expect(response).toEqual(playerData);
    });

    const req = httpMock.expectOne(`${apiURL}/api/Game/getGameRoomData/${gameRoomID}/${userID}/${cardID}/${planetIndex}`);
    expect(req.request.method).toBe('POST');
    req.flush(playerData);
  });

  it('should retrieve game room data', () => {
    const gameRoomID = 'room1';
    const gameRoomData: any = {
      roomid: gameRoomID,
      player1:  {
        id: 'user1',
        nickname: 'Player 1',
        hand: [],
        energy: 10
      } as any,
      player2: {
        id: 'user2',
        nickname: 'Player 2',
        hand: [],
        energy: 8
      } as any
    };

    service.getGameRoomData(gameRoomID).then((response) => {
      expect(response).toEqual(gameRoomData);
    });

    const req = httpMock.expectOne(`${apiURL}/api/Game/getGameRoomData/${gameRoomID}`);
    expect(req.request.method).toBe('GET');
    req.flush(gameRoomData);
  });

  it('should initialize a turn', () => {
    const gameRoomID = 'room1';
    const playerID = 'user1';

    service.initTurn(gameRoomID, playerID).then((response) => {
      expect(response).toBeDefined();
    });

    const req = httpMock.expectOne(`${apiURL}/api/Game/${gameRoomID}/${playerID}/initTurn`);
    expect(req.request.method).toBe('GET');
    req.flush({});
  });

  it('should end a turn', () => {
    const gameRoomID = 'room1';
    const playerID = 'user1';

    service.endTurn(gameRoomID, playerID).then((response) => {
      expect(response).toBeDefined();
    });

    const req = httpMock.expectOne(`${apiURL}/api/Game/${gameRoomID}/${playerID}/endTurn`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(gameRoomID);
    req.flush({});
  });

});
