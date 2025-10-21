import { Component } from '@angular/core';
import { TeamsService } from '../../../core/services/teams/teams.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-view-teams',
  templateUrl: './view-teams.component.html',
  styleUrl: './view-teams.component.css'
})
export class ViewTeamsComponent {
  equipos: any[] = [];
  equiposFiltrados: any[] = [];
  nombresEquipos: string[] = [];
  mensaje: string = '';
  isLoading = true;
  currentPage = 1;
  totalPages = 1;
  baseUrl = environment.apiUrl;
  isFiltered = false;
  mostrarFiltro = false;
  regionSeleccionada?: number | null;
  comunaSeleccionada?: number | null;
  searchTermActual: string | null = null;

  constructor(private teamsService: TeamsService) { }

  ngOnInit(): void {
    this.loadTeamsWithParams(1);
  }

  abrirFiltro(): void {
    this.mostrarFiltro = true;
  }

  filtrarEquipos(filtro: { regionId?: number, comunaId?: number }): void {
    this.regionSeleccionada = filtro.regionId ?? null;
    this.comunaSeleccionada = filtro.comunaId ?? null;
    this.currentPage = 1; 
    this.loadTeamsWithParams(this.currentPage, this.searchTermActual);
    this.mostrarFiltro = false;
  }

  // Centraliza la llamada al backend con todos los parámetros
  loadTeamsWithParams(page: number = 1, searchTerm?: string | null): void {
    this.isLoading = true;

    this.teamsService.getTeams(
      page,
      this.regionSeleccionada,
      this.comunaSeleccionada,
      searchTerm ?? null
    ).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.equipos = res.equipos || [];
        this.equiposFiltrados = [...this.equipos];
        this.totalPages = res.totalPages || 1;
        this.mensaje = res.message || '';
        this.isFiltered = !!(searchTerm && searchTerm.trim() !== '');

        // Actualizar nombres para sugerencias (todos los nombres del filtro actual)
        this.teamsService.getTeamNames(this.regionSeleccionada, this.comunaSeleccionada)
          .subscribe({
            next: (data) => {
              this.nombresEquipos = data.nombres || [];
            },
            error: (err) => console.error('Error al cargar nombres de equipos:', err)
          });
      },
      error: (err) => {
        this.isLoading = false;
        this.mensaje = err.error?.message || 'Error al obtener equipos.';
      }
    });
  }

  reiniciarFiltros(): void {
    this.regionSeleccionada = null;
    this.comunaSeleccionada = null;
    this.mostrarFiltro = false;
    this.totalPages = 1;
    this.currentPage = 1;
    this.searchTermActual = null;
    this.loadTeamsWithParams(1);
  }

  // al seleccionar una sugerencia -> guardamos término y buscamos desde la página 1
  onSuggestionSelected(term: string): void {
    this.searchTermActual = term;
    this.currentPage = 1;
    this.loadTeamsWithParams(this.currentPage, this.searchTermActual);
  }

  onCleared(): void {
    // limpiar búsqueda y recargar
    this.searchTermActual = null;
    this.currentPage = 1;
    this.loadTeamsWithParams(this.currentPage, null);
  }

  // cuando el usuario presiona buscar (Enter o botón)
  onSearch(term: string): void {
    this.searchTermActual = term;
    this.currentPage = 1;
    this.loadTeamsWithParams(this.currentPage, this.searchTermActual);
  }

  // Al cambiar de página, llamamos al servicio con el newPage y con el searchTermActual (si existe)
  onPageChange(newPage: number): void {
    this.currentPage = newPage;
    this.loadTeamsWithParams(this.currentPage, this.searchTermActual);
  }

  getLogoUrl(equipo: any): string {
    return equipo.rutaLogo
      ? `${this.baseUrl}${equipo.rutaLogo}`
      : 'assets/img/default-logo.png';
  }
}
