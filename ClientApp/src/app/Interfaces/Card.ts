type ICardRace = 'Raza'

type ICardType = 4 | 3 | 2 | 1 | 0

interface ICard {
  id: string;
  name: string;
  image?: File;
  energy: number;
  cost: number;
  type: ICardType;
  race: ICardRace;
  active: boolean;
  skillID: number;
  description: string;
}

export {
  ICardRace,
  ICardType,
  ICard
}