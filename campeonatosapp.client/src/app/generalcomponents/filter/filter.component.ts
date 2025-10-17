import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { environment } from '../../../environments/environment';
import { LocationService } from '../../services/location/location.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css'
})
export class FilterComponent implements OnInit {
  @Output() aplicarFiltros = new EventEmitter<{ regionId?: number, comunaId?: number }>();
  @Output() cerrarFiltro = new EventEmitter<void>();

  regiones: any[] = [];
  comunas: any[] = [];
  regionSeleccionada: number | null = null;
  comunaSeleccionada: number | null = null;
  connectedOverlayPosition = {
    originX: 'start',
    originY: 'bottom',
    overlayX: 'start',
    overlayY: 'top'
  };

  constructor(private locationService: LocationService) { }

  ngOnInit(): void {
    this.cargarRegiones();
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
    });
  }

  onRegionChange(): void {
    if (this.regionSeleccionada)
      this.cargarComunas(this.regionSeleccionada);
  }

  aplicar(): void {
    const filtro = {
      regionId: this.regionSeleccionada ?? undefined,
      comunaId: this.comunaSeleccionada && this.comunaSeleccionada !== 0 ? this.comunaSeleccionada : undefined
    };
    this.aplicarFiltros.emit(filtro);
  }

  cerrar(): void {
    this.cerrarFiltro.emit();
  }

}
