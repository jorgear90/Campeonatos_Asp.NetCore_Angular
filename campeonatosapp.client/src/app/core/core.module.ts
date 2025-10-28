import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth/auth.service';
import { LocationService } from './services/location/location.service';
import { TeamsService } from './services/teams/teams.service';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { EncryptionService } from './services/encryption/encryption.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [AuthService, LocationService, TeamsService, { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, EncryptionService],
  exports: []
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule ya fue cargado. Solo debe importarse en AppModule.');
    }
  }
}
