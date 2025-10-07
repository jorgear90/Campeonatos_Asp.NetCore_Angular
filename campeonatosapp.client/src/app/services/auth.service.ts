import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7021/api/usuarios';

  constructor(private http: HttpClient) { }

  login(correo: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { correo, password });
  }

  register(correo: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { correo, password });
  }
}
