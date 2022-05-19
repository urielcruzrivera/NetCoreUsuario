import { Component, OnInit } from '@angular/core';
import { Usuarios } from './Usuarios/usuarios';
import { PerfilGeneral } from './CatalogosTemporales/PerfilGeneral';
import { GeneralServiceService } from './services/general-service.service';
import swal from 'sweetalert';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'UserRegisterCrud';
  usuario: Usuarios = {
    id: 0,
    NombreCompleto: '',
    Direccion: '',
    PerfilGeneral: 0,
    PerfilDescripcion: '',
    FechaCreacion: ''
  }
  usuarioEditar:Usuarios= {
    id: 0,
    NombreCompleto: '',
    Direccion: '',
    PerfilGeneral: 0,
    PerfilDescripcion: '',
    FechaCreacion: ''
  };
  perfiles: PerfilGeneral[] = [];
  todosLosUsuarios: Usuarios[] = [];
  IdUsuario: number = 0;

  constructor(private servicio: GeneralServiceService) {
    this.ObtenerPerfiles();
    this.ObtenerUsuarios();
  }

  public ObtenerPerfiles() {
    this.servicio.ObtenerPerfiles().subscribe((data: any) => {
      this.perfiles = data.data;
    }, error => {
      swal('Error al obtener perfiles', 'Ocurrio un error al obtener perfiles!!!', 'error');
    });
  }

  public AgregarUsuario() {
    this.servicio.GuardarUsuario(this.usuario).subscribe(async (data) => {
      swal('Registro exitoso', 'Usuario registrado con éxito!!!', 'success').then(() => {
        this.ObtenerUsuarios();
        document.getElementById("btnCerrarAgregar").click();
      }, (error: any) => console.log(error));
    }, (error) => {
      swal('Error al guardar usuario', 'Ocurrio un error al registrar usuario!!!', 'error');
    });
  }

  public ObtenerUsuarios() {
    this.servicio.ObtenerUsuarios().subscribe(async (data: any) => {
      this.usuario = {
        id: 0,
        NombreCompleto: '',
        Direccion: '',
        PerfilGeneral: 0,
        PerfilDescripcion: '',
        FechaCreacion: ''
      };
      this.todosLosUsuarios = data.data;
    }, (error) => {
      swal('Error al consultar usuarios', 'Ocurrio un error al consultar usuarios!!!', 'error');
    });
  }

  public EstablecerIdEliminar(Id: number): void {
    this.IdUsuario = Id;
  }

  public EstablecerIdEditar(Id: number): void {
    this.usuarioEditar = this.todosLosUsuarios.find(x=>x.id == Id);
  }

  public EliminarUsuario() {
    this.servicio.EliminarUsuario(this.IdUsuario).subscribe(async (data: any) => {
      swal('Eliminación exitosa', 'Usuario eliminado con éxito!!!', 'success').then(() => {
        this.ObtenerUsuarios();
        document.getElementById("btnCerrarEliminar").click();
      }, (error: any) => console.log(error));
    }, (error) => {
      swal('Error al eliminar usuario', 'Ocurrio un error al eliminar usuario!!!', 'error');
    });
  }

  public ActualizarUsuario(){
    this.servicio.ActualizarUsuario(this.usuarioEditar).subscribe(async (data: any) => {
      swal('Actualización exitosa', 'Usuario actualizado con éxito!!!', 'success').then(() => {
        this.ObtenerUsuarios();
        document.getElementById("btnCerrarEditar").click();
      }, (error: any) => console.log(error));
    }, (error) => {
      swal('Error al actualizar usuarios', 'Ocurrio un error al actualizar usuario!!!', 'error');
    });
  }

  ngOnInit() {
  }

}



