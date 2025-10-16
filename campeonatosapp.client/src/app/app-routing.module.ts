import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Login/login.component';
import { RegisterComponent } from './Register/register.component';
import { MainPageComponent } from './main-page/main-page.component';
import { CreateChampionshipComponent } from './create-championship/create-championship.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { ViewTeamsComponent } from './view-teams/view-teams.component';
import { MyTeamsComponent } from './my-teams/my-teams.component';

const routes: Routes = [
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main', component: MainPageComponent },
  { path: 'create-championship', component: CreateChampionshipComponent },
  { path: 'create-team', component: CreateTeamComponent },
  { path: 'view-teams', component: ViewTeamsComponent },
  { path: 'my-teams', component: MyTeamsComponent },
  { path: '**', redirectTo: 'main' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
