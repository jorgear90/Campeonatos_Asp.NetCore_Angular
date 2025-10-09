import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  correo = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router:Router) { }

  login() {
    this.authService.login(this.correo, this.password).subscribe({
      next: () => this.router.navigate(['/main']),
      error: () => this.errorMessage = 'Correo o contraseña incorrecta'
    });
  }
}
