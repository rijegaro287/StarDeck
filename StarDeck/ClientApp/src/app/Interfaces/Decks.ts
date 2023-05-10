import { ICard } from "./Card";

interface IDeck {
  id: string;
  name: string;
  cards: ICard[];
  cardsIDs?: string[];
}

export {
  IDeck
}