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

@NgModule({
  declarations: [
    AppComponent, LoginComponent, RegisterComponent, MainPageComponent, CreateTeamComponent, CreateChampionshipComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule
  ],
  providers: [AuthService, { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
