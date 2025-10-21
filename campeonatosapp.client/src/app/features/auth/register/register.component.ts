import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { LocationService } from '../../../core/services/location/location.service';

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
  regiones: any[] = [];
  comunas: any[] = [];
  regionSeleccionada: number | null = null;
  comunaSeleccionada: number = 0;
  isLoggedIn = false;

  constructor(private authService: AuthService, private router: Router, private locationService: LocationService) { }

  isLoading = false;

  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status;
    });
    if (this.isLoggedIn) {
      this.router.navigate(['/main']);
    }
    this.isLoading = true;
    this.cargarRegiones();
    this.isLoading = false;
  }

  cargarRegiones(): void {
    this.locationService.getRegiones().subscribe(data => {
      this.regiones = data;
      if (this.regiones.length > 0) {
        this.regionSeleccionada = this.regiones[0].id;
        if (this.regionSeleccionada !== null) {
          this.cargarComunas(this.regionSeleccionada);
        }
      }
    });
  }

  cargarComunas(regionId: number): void {
    this.locationService.getComunas(regionId).subscribe(data => {
      this.comunas = [...data];
      if (this.comunas.length > 0) {
        this.comunaSeleccionada = this.comunas[0].id;
      }
    });
  }

  onRegionChange(): void {
    if (this.regionSeleccionada)
      this.cargarComunas(this.regionSeleccionada);
  }


  register() {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Las contraseñas no coinciden';
      return;
    }

    this.isLoading = true;

    this.authService.register(this.correo, this.password, this.comunaSeleccionada).subscribe({
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
        this.isLoading = false; 
        this.errorMessage = err.error?.message || err.error || 'Error desconocido';
      }
    });
  }
}
