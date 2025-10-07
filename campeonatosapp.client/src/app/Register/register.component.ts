import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  correo = '';
  password = '';
  confirmPassword = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  isLoading = false;

  register() {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Las contraseñas no coinciden';
      return;
    }

    this.isLoading = true;

    this.authService.register(this.correo, this.password).subscribe({
      next: (res) => {

        this.isLoading = false;

        Swal.fire({
          title: 'Éxito',
          text: res.message || 'Usuario creado correctamente!',
          icon: 'success',
          confirmButtonText: 'Aceptar',
          allowOutsideClick: false,
          allowEscapeKey: false
        }).then(() => {
          this.router.navigate(['/login']);
        });
      },
      error: (err) => {
        //console.error(err); // 👈 te muestra el error real en la consola
        this.isLoading = false; 
        this.errorMessage = err.error?.message || err.error || 'Error desconocido';
      }
    });
  }

  /*register() {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Las contraseñas no coinciden';
      return;
    }

    this.authService.register(this.correo, this.password).subscribe({
      next: () => alert('Usuario creado correctamente!'),
      error: (err) => {
        console.error(err);
        if (err.error && typeof err.error === 'object' && err.error.message) {
          this.errorMessage = err.error.message;
        } else if (typeof err.error === 'string') {
          this.errorMessage = err.error;
        } else {
          this.errorMessage = 'Error al conectar con el servidor';
        }
      }
    });

  }*/
}
