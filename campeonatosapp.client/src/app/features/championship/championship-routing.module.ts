import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateChampionshipComponent } from './create-championship/create-championship.component';

const routes: Routes = [
  { path: 'create-championship', component: CreateChampionshipComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChampionshipRoutingModule { }
