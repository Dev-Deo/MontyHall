import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameRoutingModule } from './game-routing.module';
import { SharedModule } from '../shared/shared.module';
import { GameComponent } from './game.component';

@NgModule({
  declarations: [GameComponent],
  imports: [CommonModule, GameRoutingModule, SharedModule],
})
export class GameModule {}
