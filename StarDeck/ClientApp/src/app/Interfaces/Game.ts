import { ICard } from "./Card";
import { IPlanet } from "./Planet";

interface IGameRoom {
  roomid: string;
  player1: IPlayer;
  player2: IPlayer;
  winner: null;
  bet: null;
  turn: null;
  territories: IPlanet[];
  gamelog: IGameLog;
}

interface IGameLog {
  gameID: string;
  log: null;
}

interface IPlayer {
  id: string;
  Name: string;
  nickname: string;
  Avatar: number;
  config: null;
  points: number;
  coins: number;
  energy: number;
  hand?: ICard[];
  deck?: ICard[];
  tmpTerritories?: IPlanet[][];
}

export {
  IGameRoom,
  IGameLog,
  IPlayer
}