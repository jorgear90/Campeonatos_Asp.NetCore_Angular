import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Login/login.component';
import { RegisterComponent } from './Register/register.component';

import { AuthService } from './services/auth.service';
import { FormsModule } from '@angular/forms';
import { MainPageComponent } from './main-page/main-page.component';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptor/auth.interceptor';
import { CreateTeamComponent } from './create-team/create-team.component';
import { CreateChampionshipComponent } from './create-championship/create-championship.component';
import { TeamsService } from './services/teams/teams.service';
import { ViewTeamsComponent } from './view-teams/view-teams.component';
import { ReturnButtonComponent } from './generalcomponents/return-button/return-button.component';
import { IsLoadingComponent } from './generalcomponents/is-loading/is-loading.component';
import { MyTeamsComponent } from './my-teams/my-teams.component';
import { PaginationComponent } from './generalcomponents/pagination/pagination.component';
import { SearchInputComponent } from './generalcomponents/search-input/search-input.component';
import { FilterComponent } from './generalcomponents/filter/filter.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';


@NgModule({
  declarations: [
    AppComponent, LoginComponent, RegisterComponent, MainPageComponent, CreateTeamComponent, CreateChampionshipComponent, ViewTeamsComponent, ReturnButtonComponent, IsLoadingComponent, MyTeamsComponent, PaginationComponent, SearchInputComponent, FilterComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule, BrowserAnimationsModule, MatSelectModule, MatFormFieldModule,
    MatOptionModule
  ],
  providers: [AuthService, { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, TeamsService, provideAnimationsAsync('noop')],
  bootstrap: [AppComponent]
})
export class AppModule { }
