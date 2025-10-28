import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TeamsService } from '../../../core/services/teams/teams.service';
import { LocationService } from '../../../core/services/location/location.service';
import { Team } from '../../../models/team.interface';
import Swal from 'sweetalert2';
import { environment } from '../../../../environments/environment';
import { EncryptionService } from '../../../core/services/encryption/encryption.service';
import { AuthService } from '../../../core/services/auth/auth.service';

@Component({
  selector: 'app-edit-teams',
  templateUrl: './edit-teams.component.html',
  styleUrl: './edit-teams.component.css'
})
export class EditTeamsComponent {
  equipo: Team = {
    id: 0,
    nombre: '',
    rutaLogo: '',
    usuarioID: 0,
    comunaID: 0,
    region: '',
    comuna: ''
  };

  apiUrl = environment.apiUrl;

  equipoId: number = 0;
  errorMessage = '';
  selectedFile: File | null = null;

  regiones: any[] = [];
  comunas: any[] = [];
  regionSeleccionada: number | null = null;
  comunaSeleccionada: number | null = null;


  constructor(private route: ActivatedRoute, private teamService: TeamsService, private locationService: LocationService, private authService: AuthService, private encryptionService: EncryptionService, private router: Router) { }

  ngOnInit(): void {
    const encryptedId = sessionStorage.getItem('equipoId');

    if (encryptedId == null || !this.authService.isAuthenticated()) {
      this.router.navigate(['/my-teams']);
    
    }

    if (encryptedId) {
      const decrypted = this.encryptionService.decrypt(encryptedId);
      this.equipoId = +decrypted;

      this.teamService.getTeam(this.equipoId).subscribe({
        next: (data) => {
          this.equipo = data.equipo;
          this.cargarRegionesYSeleccionar();
        },
        error: (err) => {
          this.errorMessage = err.error?.message || 'Error al obtener equipo.';
        }
      });
    } else {
      this.errorMessage = 'No se encontró equipo.';
    }
  }

  cargarRegiones(): void {
    this.locationService.getRegiones().subscribe(data => {
      this.regiones = data;
      
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

  cargarRegionesYSeleccionar(): void {
    this.locationService.getRegiones().subscribe(data => {
      this.regiones = data;

      // Buscar la región correspondiente al equipo
      const regionEncontrada = this.regiones.find(
        (r) => r.nombre === this.equipo.region
      );

      if (regionEncontrada) {
        this.regionSeleccionada = regionEncontrada.id;

        // Cargar comunas de esa región
        this.locationService.getComunas(this.regionSeleccionada!).subscribe(dataComunas => {
          this.comunas = dataComunas;

          // Buscar la comuna correspondiente al equipo
          const comunaEncontrada = this.comunas.find(
            (c) => c.nombre === this.equipo.comuna
          );

          if (comunaEncontrada) {
            this.comunaSeleccionada = comunaEncontrada.id;
          }
        });
      } 
    });
  }

  onRegionChange(): void {
    if (this.regionSeleccionada)
      this.cargarComunas(this.regionSeleccionada);
  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  onSubmit(): void {
    //this.isLoading = true;

    if (this.equipo.nombre && this.comunaSeleccionada && this.regionSeleccionada) {

      if (!this.selectedFile) {
        const blobVacio = new Blob([], { type: 'text/plain' });
        this.selectedFile = new File([blobVacio], 'vacio.txt');
      }

      this.teamService.upDate(this.equipo.id, this.equipo.nombre, this.comunaSeleccionada, this.selectedFile)
        .subscribe({
          next: (res) => {
            //this.isLoading = false;
            Swal.fire({
              title: 'Éxito',
              text: res.message || 'Equipo creado correctamente!',
              icon: 'success',
              confirmButtonText: 'Aceptar',
              allowOutsideClick: false,
              allowEscapeKey: false
            }).then(() => {
              this.router.navigate(['/my-teams']);
            });
          },
          error: (err) => {
            //this.isLoading = false;

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

  getLogoUrl(equipo: any): string {
    return equipo.rutaLogo
      ? `${this.apiUrl}${equipo.rutaLogo}`
      : 'assets/img/default-logo.png';
  }

  restaurarEquipo() {
    const encryptedId = sessionStorage.getItem('equipoId');

    if (encryptedId) {
      const decrypted = this.encryptionService.decrypt(encryptedId);
      this.equipoId = +decrypted;

      this.teamService.getTeam(this.equipoId).subscribe({
        next: (data) => {
          this.equipo = data.equipo;
          this.cargarRegionesYSeleccionar();
          const inputFile = document.getElementById('inputFile') as HTMLInputElement;
          inputFile.value = '';
          this.selectedFile = null;
        },
        error: (err) => {
          this.errorMessage = err.error?.message || 'Error al obtener equipo.';
        }
      });
    } else {
      this.errorMessage = 'No se encontró equipo.';
    }
  }
}
