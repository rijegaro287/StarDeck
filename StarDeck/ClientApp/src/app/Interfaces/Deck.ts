import { ICard } from "./Card";

interface IDeck {
  id: string;
  idAccount?: string;
  deckName: string;
  cardlist: string[];
  cards?: ICard[];
}

export {
  IDeck
}