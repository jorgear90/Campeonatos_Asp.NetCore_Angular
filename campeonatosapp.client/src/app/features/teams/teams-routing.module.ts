import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTeamComponent } from './create-team/create-team.component';
import { ViewTeamsComponent } from './view-teams/view-teams.component';
import { MyTeamsComponent } from './my-teams/my-teams.component';

const routes: Routes = [
  { path: 'create-team', component: CreateTeamComponent },
  { path: 'view-teams', component: ViewTeamsComponent },
  { path: 'my-teams', component: MyTeamsComponent },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamsRoutingModule { }
