import { ICard } from "./Card";
import { IPlanet } from "./Planet";

interface IServerResponse<T = any> {
  status: 'ok' | 'error';
  body?: T;
  message?: string;
  redirect?: string;
}

type ICardsResponse = IServerResponse<ICard[]>;
type IPlanetResponse = IServerResponse<IPlanet[]>;

export {
  IServerResponse,
  ICardsResponse,
  IPlanetResponse
}
