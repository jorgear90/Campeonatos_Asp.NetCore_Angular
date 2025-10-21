import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateChampionshipComponent } from './create-championship/create-championship.component';

const routes: Routes = [
  { path: '', redirectTo: 'main', pathMatch: 'full' },
  {
    path: '',
    loadChildren: () =>
      import('./features/auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: '',
    loadChildren: () =>
      import('./features/teams/teams.module').then(m => m.TeamsModule)
  },
  {
    path: '',
    loadChildren: () =>
      import('./features/main/main.module').then(m => m.MainModule)
  },
  { path: 'create-championship', component: CreateChampionshipComponent },
  { path: '**', redirectTo: 'main' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
