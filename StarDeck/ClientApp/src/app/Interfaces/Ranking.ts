interface IIndividualRanking {
  position: number;
  nickname: string;
  points: number;
}

interface IGlobalRanking {
  nickname: string;
  points: number;
}

export {
  IIndividualRanking,
  IGlobalRanking
}
