import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { CoreModule } from './core/core.module';
import { TeamsModule } from './features/teams/teams.module';
import { MainModule } from './features/main/main.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule, BrowserAnimationsModule, MatSelectModule, MatFormFieldModule,
    MatOptionModule, CoreModule, AppRoutingModule, TeamsModule, MainModule
  ],
  providers: [provideAnimationsAsync('noop')],
  bootstrap: [AppComponent]
})
export class AppModule { }
