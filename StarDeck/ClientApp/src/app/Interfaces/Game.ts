export interface Welcome {
  roomid: string;
  player1: Player;
  player2: Player;
  winner: null;
  bet: null;
  turn: null;
  territories: Territory[];
  gamelog: Gamelog;
}

export interface Gamelog {
  gameid: string;
  log: null;
}

export interface Player {
  id: string;
  name: string;
  nickname: string;
  avatar: number;
  config: null;
  points: number;
  coins: number;
}

export interface Territory {
  id: string;
  name: string;
  type: number;
  active: boolean;
  ability: Ability;
  player1Cards: any[];
  player2Cards: any[];
}

export interface Ability {
}
