import { Component } from '@angular/core';
import { TeamsService } from '../../../core/services/teams/teams.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth.service';
import Swal from 'sweetalert2';
import { LocationService } from '../../../core/services/location/location.service';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrl: './create-team.component.css'
})
export class CreateTeamComponent {
  nombre = '';
  comuna = 0;
  selectedFile: File | null = null;
  errorMessage = '';

  regiones: any[] = [];
  comunas: any[] = [];
  regionSeleccionada: number | null = null;
  comunaSeleccionada: number | null = null;

  constructor(private teamsService: TeamsService, private authService: AuthService, private router: Router, private locationService: LocationService) { };

  isLoading = false;

  auth = this.authService.isAuthenticated();

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
    //this.isLoading = true;
    this.cargarRegiones();
    //this.isLoading = false;
  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  onSubmit(): void {
    this.isLoading = true;

    if (this.nombre && this.comunaSeleccionada) {

      if (!this.selectedFile) {
        const blobVacio = new Blob([], { type: 'text/plain' });
        this.selectedFile = new File([blobVacio], 'vacio.txt');
      }

      this.teamsService.uploadLogo(this.nombre, this.selectedFile, this.comunaSeleccionada)
        .subscribe({
          next: (res) => {
            this.isLoading = false;
            Swal.fire({
              title: 'Éxito',
              text: res.message || 'Equipo creado correctamente!',
              icon: 'success',
              confirmButtonText: 'Aceptar',
              allowOutsideClick: false,
              allowEscapeKey: false
            }).then(() => {
              this.router.navigate(['/view-teams']);
            });
          },
          error: (err) => {
            this.isLoading = false;

            if (err.error && err.error.message) {
              // Si el backend envió un mensaje específico
              this.errorMessage = err.error.message;
            } else if (typeof err.error === 'string') {
              // Si el backend devolvió un string plano
              this.errorMessage = err.error;
            } else {
              // Si no hay mensaje claro, mostramos algo genérico
              this.errorMessage = 'Error al hacer el registro';
            }

            Swal.fire({
              title: 'Error',
              text: this.errorMessage,
              icon: 'error',
              confirmButtonText: 'Aceptar'
            });
          }

        });

    } else {
      this.errorMessage = 'Debe ingresar un nombre y seleccionar una comuna.';
    }
  }


  cargarRegiones(): void {
    this.isLoading = true;
    this.locationService.getRegiones().subscribe(data => {
      this.regiones = data;
      if (this.regiones.length > 0) {
        this.regionSeleccionada = this.regiones[0].id;
        if (this.regionSeleccionada !== null) {
          this.cargarComunas(this.regionSeleccionada);
        }
      }
      this.isLoading = false;
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


}
