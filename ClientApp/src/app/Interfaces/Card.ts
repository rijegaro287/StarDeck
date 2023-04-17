type ICardRace = ''

// Se pueden cambiar por otros (?)
type ICardType = 'UltraRare' | 'VeryRare' | 'Rare' | 'Normal' | 'Basic'

interface ICard {
  id: string;
  name: string;
  image: File;
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