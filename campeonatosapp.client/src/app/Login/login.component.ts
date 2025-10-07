import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  correo = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService) { }

  login() {
    this.authService.login(this.correo, this.password).subscribe({
      next: () => alert('Login correcto!'),
      error: () => this.errorMessage = 'Correo o contraseña incorrecta'
    });
  }
}
