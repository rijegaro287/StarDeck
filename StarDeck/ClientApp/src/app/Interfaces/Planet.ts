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
  player1Cards?: any[];
  player2Cards?: any[];
}

export {
  IPlanetType,
  IPlanet
}
