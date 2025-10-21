import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private apiUrlRegiones = environment.apiRegionesUrl;
  private apiUrlComunas = environment.apiComunasUrl;

  constructor(private http: HttpClient) { }

  getRegiones(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrlRegiones}/getRegiones`);
  }

  getComunas(regionId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrlComunas}/getComunas/${regionId}`);
  }

  
}
