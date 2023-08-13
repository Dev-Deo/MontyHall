import { IApplicationUser } from './applicationUser';

export interface IGameRequest {
  id: number;
  userId: string;
  user: IApplicationUser;
  totalGameRequests: number;
}
