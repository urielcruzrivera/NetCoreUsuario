import { Component, OnInit } from '@angular/core';
import { Usuarios } from './Usuarios/usuarios';
import { PerfilGeneral } from './CatalogosTemporales/PerfilGeneral';
import { GeneralServiceService } from './services/general-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'UserRegisterCrud';
  usuario: Usuarios = {
    Id: 0,
    NombreCompleto: '',
    Direccion: '',
    PerfilGeneral: 0,
    PerfilDescripcion:'',
    FechaCreacion: ''
  }
  perfiles: PerfilGeneral[] = [];
  todosLosUsuarios: Usuarios[] = [];

  constructor(private servicio: GeneralServiceService) {
    this.ObtenerPerfiles();
    this.ObtenerUsuarios();
  }

  public ObtenerPerfiles() {
    this.servicio.ObtenerPerfiles().subscribe((data: any) => {
      this.perfiles = data.data;
    }, error => {
      console.log('Error ObtenerPerfiles:' + error);
    });
  }

  public AgregarUsuario() {
    this.servicio.GuardarUsuario(this.usuario).subscribe(async (data) => {

    }, (error) => {
      console.log('Error agregarUsuario: ', error);
    });
  }

  public ObtenerUsuarios() {
    this.servicio.ObtenerUsuarios().subscribe(async (data: any) => {
      this.todosLosUsuarios = data.data;
    }, (error) => {
      console.log('Error agregarUsuario: ', error);
    });
  }

  ngOnInit() {
  }

}



