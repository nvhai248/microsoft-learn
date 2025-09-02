import {Game} from './game';

export interface Wish {
  gameId: number;
  userId: string;
  wishCount: number;
  game?: Game;
}
