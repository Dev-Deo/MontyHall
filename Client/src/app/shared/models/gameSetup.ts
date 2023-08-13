import { IGameRequest } from './gameRequest';

export interface IGameSetup {
  id: number;
  gameRequestId: number;
  gameRequest: IGameRequest;
  gameRequestNo: number;
  firstDoor: string;
  secondDoor: string;
  thirdDoor: string;
}
