import { IGameSetup } from './gameSetup';

export interface IGameResult {
  id: number;
  gameSetupId: number;
  gameSetup: IGameSetup;
  firstChoice: number;
  openedDoorNo: number;
  secondChoice: number;
  isWin: boolean;
}
