import { ICard } from "./Card";

interface IServerResponse<T = any> {
  status: 'ok' | 'error';
  body?: T;
  message?: string;
  redirect?: string;
}

type ICardsResponse = IServerResponse<ICard[]>;

export {
  IServerResponse,
  ICardsResponse
}