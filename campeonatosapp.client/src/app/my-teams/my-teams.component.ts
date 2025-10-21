import { Component } from '@angular/core';
import { TeamsService } from '../services/teams/teams.service';
import { environment } from '../../environments/environment';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-teams',
  templateUrl: './my-teams.component.html',
  styleUrl: './my-teams.component.css'
})
export class MyTeamsComponent {
  equipos: any[] = [];
  mensaje: string = '';
  isLoading = true;
  apiUrl = environment.apiUrl;

  constructor(private teamsService: TeamsService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
    }
    this.teamsService.getMyTeams().subscribe({
      next: (res) => {
        this.isLoading = false;
        if (res.equipos && res.equipos.length > 0) {
          this.equipos = res.equipos;
        } else {
          this.mensaje = res.message || 'No hay equipos para mostrar.';
        }
      },
      error: (err) => {
        this.isLoading = false;
        this.mensaje = err.error?.message || 'Error al obtener equipos.';
      }
    });
  }

  getLogoUrl(equipo: any): string {
    return equipo.rutaLogo
      ? `${this.apiUrl}${equipo.rutaLogo}`
      : 'assets/img/default-logo.png';
  }
}
