import { Component, OnInit } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { IGameRequestCreate } from '../shared/models/gameRequestCreate';
import { GameService } from '../shared/services/game.service';
import { IGameSetupCreate } from '../shared/models/gameSetupCreate';
import { IGameSetup } from '../shared/models/gameSetup';
import { IGameResultCreate } from '../shared/models/gameResultCreate';
import { IGameResult } from '../shared/models/gameResult';
import { IGameResultUpdate } from '../shared/models/gameResultUpdate';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
})
export class GameComponent implements OnInit {
  attempts: number = 0;
  attemptNo: number = 0;
  gameSetup: IGameSetup | undefined;
  gameResult: IGameResult | undefined;
  doorOneImg: string = 'DOOR1.png';
  doorTwoImg: string = 'DOOR2.png';
  doorThreeImg: string = 'DOOR3.png';
  gameRequestId: number = 0;
  gameSetupId: number = 0;
  selectedDoor: number = 0;
  isWin: boolean | undefined;

  constructor(
    private authService: AuthService,
    private gameService: GameService
  ) {}

  ngOnInit(): void {}

  onProceed(gameCount: HTMLInputElement) {
    this.attempts = +gameCount.value;
    let currentUser = this.authService.getCurrentUser();

    let att: IGameRequestCreate = {
      totalGameRequests: this.attempts,
      userId: currentUser.userId,
    };

    this.createGameRequest(att).subscribe({
      next: (res) => {
        this.gameRequestId = res.data.id;
        this.goToNextGame();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  goToNextGame() {
    this.attemptNo++;
    this.isWin = undefined;
    this.selectedDoor = 0;
    this.gameSetupId = 0;

    this.doorOneImg = 'DOOR1.png';
    this.doorTwoImg = 'DOOR2.png';
    this.doorThreeImg = 'DOOR3.png';

    this.gameSetup = undefined;

    let gameSetupCreate: IGameSetupCreate = {
      gameRequestId: this.gameRequestId,
      gameRequestNo: this.attemptNo,
    };
    this.createGameSetup(gameSetupCreate).subscribe({
      next: (res) => {
        this.gameSetup = res.data;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  createGameSetup(gameSetup: IGameSetupCreate) {
    return this.gameService.createGameSetup(gameSetup);
  }

  createGameRequest(att: IGameRequestCreate) {
    return this.gameService.createGameRequest(att);
  }

  onDoorSelect(door: number) {
    let gameResultCreate: IGameResultCreate = {
      gameSetupId: this.gameSetup?.id,
      firstChoice: door,
    };

    this.createGameResult(gameResultCreate).subscribe({
      next: (res) => {
        this.gameResult = res.data;
        this.selectedDoor = res.data.firstChoice;
        switch (this.gameResult.openedDoorNo) {
          case 1:
            this.doorOneImg = 'GOAT.png';
            break;
          case 2:
            this.doorTwoImg = 'GOAT.png';
            break;
          case 3:
            this.doorThreeImg = 'GOAT.png';
            break;
        }
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  createGameResult(gameResult: IGameResultCreate) {
    return this.gameService.createGameResult(gameResult);
  }

  onSwitch() {
    let gameResultUpdate: IGameResultUpdate = {
      gameSetupId: this.gameSetup?.id,
      id: this.gameResult?.id,
      isSwitch: true,
    };

    this.updateGameResult(gameResultUpdate).subscribe({
      next: (res) => {
        this.gameResult = res.data;
        this.isWin = this.gameResult?.isWin;
        this.selectedDoor = this.gameResult?.secondChoice;
        console.log(res);
        switch (this.gameResult.gameSetup.firstDoor) {
          case 'G':
            this.doorOneImg = 'GOAT.png';
            break;
          case 'C':
            this.doorOneImg = 'CAR.png';
            break;
        }

        switch (this.gameResult.gameSetup.secondDoor) {
          case 'G':
            this.doorTwoImg = 'GOAT.png';
            break;
          case 'C':
            this.doorTwoImg = 'CAR.png';
            break;
        }

        switch (this.gameResult.gameSetup.thirdDoor) {
          case 'G':
            this.doorThreeImg = 'GOAT.png';
            break;
          case 'C':
            this.doorThreeImg = 'CAR.png';
            break;
        }
      },
    });
  }

  onStay() {
    let gameResultUpdate: IGameResultUpdate = {
      gameSetupId: this.gameSetup?.id,
      id: this.gameResult?.id,
      isSwitch: true,
    };

    this.updateGameResult(gameResultUpdate).subscribe({
      next: (res) => {
        this.gameResult = res.data;
        this.isWin = this.gameResult?.isWin;
      },
    });
  }

  updateGameResult(gameResult: IGameResultUpdate) {
    return this.gameService.updateGameResult(gameResult);
  }
}
