type ICardRace = 'Raza'

type ICardType = 4 | 3 | 2 | 1 | 0

interface ICard {
  id: string;
  name: string;
  image: string;
  energy: number;
  battlecost: number;
  type: ICardType;
  race?: ICardRace;
  active?: boolean;
  ability?: number;
  description: string;
}

export {
  ICardRace,
  ICardType,
  ICard
}