import { ICard } from "./Card";

interface IDeck {
  id: string;
  name: string;
  cards: ICard[];
  cardsIDs?: string[];
}

interface IDeckNames {
  id: string;
  name: string;
}

export {
  IDeck,
  IDeckNames
}
