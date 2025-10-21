import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeamsRoutingModule } from './teams-routing.module';
import { SharedModule } from '../../shared/shared.module';
import { CreateTeamComponent } from './create-team/create-team.component';
import { MyTeamsComponent } from './my-teams/my-teams.component';
import { ViewTeamsComponent } from './view-teams/view-teams.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [CreateTeamComponent, MyTeamsComponent, ViewTeamsComponent],
  imports: [
    CommonModule, FormsModule,
    TeamsRoutingModule, SharedModule
  ]
})
export class TeamsModule { }
