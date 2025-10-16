import { Component } from '@angular/core';
import { TeamsService } from '../services/teams/teams.service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrl: './create-team.component.css'
})
export class CreateTeamComponent {
  nombre = '';
  selectedFile: File | null = null;
  errorMessage = '';

  constructor(private teamsService: TeamsService, private authService: AuthService, private router: Router) { };

  isLoading = false;

  auth = this.authService.isAuthenticated();

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      console.log('Imagen seleccionada:', file.name);
    }
  }

  onSubmit(): void {
    this.isLoading = true;

    if (this.nombre) {

      if (!this.selectedFile) {
        const blobVacio = new Blob([], { type: 'text/plain' });
        this.selectedFile = new File([blobVacio], 'vacio.txt');
      }

      this.teamsService.uploadLogo(this.nombre, this.selectedFile)
        .subscribe({
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
              this.router.navigate(['/view-teams']);
            });
          },
          error: (err) => {
            this.isLoading = false;
            //console.error('Error al hacer el registro:', err)
            this.errorMessage = 'Error al hacer el registro';
          }
        });
    } else {
      this.errorMessage = 'Debe ingresar un nombre.'
    }
  }

}
