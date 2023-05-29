import { ICard } from "./Card";

type IPlanetType = 2 | 1 | 0;

interface IPlanet {
  id: string;
  name: string;
  type: IPlanetType;
  image: string;
  active?: boolean;
  ability?: string;
  description: string;
  borderColor?: string;
  player1Cards?: ICard[];
  player2Cards?: ICard[];
}

interface IPlanetCards {
  index: number;
  name: string;
  opponentCards: ICard[];
  playerCards: ICard[];
}

export {
  IPlanetType,
  IPlanet,
  IPlanetCards
}
