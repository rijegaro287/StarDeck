type IRace = ''

// Se pueden cambiar por otros (?)
type IType = 'UltraRare' | 'VeryRare' | 'Rare' | 'Normal' | 'Basic'

interface ICard {
  id: string;
  name: string;
  image: File;
  energy: number;
  cost: number;
  type: IType;
  race: IRace;
  active: boolean;
  skillID: number;
  description: string;
}

export {
  IRace,
  IType,
  ICard
}