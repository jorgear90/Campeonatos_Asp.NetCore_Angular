export interface Team {
  id: number;
  nombre: string;
  rutaLogo: string;
  usuarioID: number;
  comunaID: number;
  region?: string;   
  comuna?: string;   
}
