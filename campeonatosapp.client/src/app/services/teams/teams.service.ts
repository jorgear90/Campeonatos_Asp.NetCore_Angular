import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {
  private apiUrl = environment.apiEquiposUrl;

  constructor(private http: HttpClient) { }

  uploadLogo(nombre: string, logo: File): Observable<any> {
    const formData = new FormData();
    formData.append('nombre', nombre);
    formData.append('logo', logo);  

    // Obtener el token del localStorage
    const token = localStorage.getItem('token');

    // Crear headers con el token
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post(`${this.apiUrl}/createTeams`, formData, { headers });

  }


  getTeams(page: number = 1, regionId?: number | null, comunaId?: number | null, searchTerm?: string | null
  ): Observable<any> {
    let params: any = { page };

    if (regionId) params.regionId = regionId;
    if (comunaId != null) params.comunaId = Number(comunaId);
    if (searchTerm) params.searchTerm = searchTerm;

    return this.http.get(`${this.apiUrl}/getTeams`, { params });
  }

  getTeamNames(regionId?: number | null, comunaId?: number | null): Observable<any> {
    let params: any = {};

    if (regionId) params.regionId = regionId;
    if (comunaId != null) params.comunaId = Number(comunaId);

    return this.http.get(`${this.apiUrl}/teamNames`, { params });
  }


  buscarPorNombre(nombre: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/search`, { params: { nombre } });
  }

  getMyTeams(): Observable<any> {
    return this.http.get(`${this.apiUrl}/getMyTeams`);
  }

}
