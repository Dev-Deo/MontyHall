import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IGameRequestCreate } from '../models/gameRequestCreate';
import { IApiResponse } from '../models/apiResponse';
import { IGameSetupCreate } from '../models/gameSetupCreate';
import { IGameSetup } from '../models/gameSetup';
import { IGameRequest } from '../models/gameRequest';
import { IGameResultCreate } from '../models/gameResultCreate';
import { IGameResult } from '../models/gameResult';
import { IGameResultUpdate } from '../models/gameResultUpdate';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createGameRequest(attempt: IGameRequestCreate) {
    return this.http.post<IApiResponse<IGameRequest>>(
      this.baseUrl + 'gameRequest/createGameRequest',
      attempt
    );
  }

  createGameSetup(gameSetup: IGameSetupCreate) {
    return this.http.post<IApiResponse<IGameSetup>>(
      this.baseUrl + 'gameSetup/createGameSetup',
      gameSetup
    );
  }

  createGameResult(gameResultCreate: IGameResultCreate) {
    return this.http.post<IApiResponse<IGameResult>>(
      this.baseUrl + 'gameResult',
      gameResultCreate
    );
  }

  updateGameResult(gameResultUpdate: IGameResultUpdate) {
    return this.http.put<IApiResponse<IGameResult>>(
      this.baseUrl + 'gameResult',
      gameResultUpdate
    );
  }
}
