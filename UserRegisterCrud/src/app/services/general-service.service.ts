import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Usuarios } from '../Usuarios/usuarios';

@Injectable({
  providedIn: 'root'
})
export class GeneralServiceService {
  API_ENDPOINT = 'http://localhost:21149/';

  constructor(private httpClient: HttpClient) { }

  ObtenerPerfiles() {
    return this.httpClient.get(this.API_ENDPOINT + "HomeApi/ObtenerPerfiles");
  }

  GuardarUsuario(usuario: Usuarios) {
    return this.httpClient.post<any>(this.API_ENDPOINT + 'HomeApi/InsertarUsuario', usuario);
  }

  ObtenerUsuarios() {
    return this.httpClient.get(this.API_ENDPOINT + "HomeApi/ObtenerUsuarios");
  }

  EliminarUsuario(Id:number){
    return this.httpClient.delete(this.API_ENDPOINT + 'HomeApi/Eliminar?id=' + Id);
  }

  ActualizarUsuario(usuario: Usuarios) {
    return this.httpClient.post<any>(this.API_ENDPOINT + "HomeApi/ActualizarUsuario", usuario);
}
}
