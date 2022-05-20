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
    nombreCompleto: '',
    direccion: '',
    perfilGeneral: 0,
    perfilDescripcion: '',
    fechaCreacion: ''
  }
  usuarioEditar: Usuarios = {
    id: 0,
    nombreCompleto: '',
    direccion: '',
    perfilGeneral: 0,
    perfilDescripcion: '',
    fechaCreacion: ''
  };
  idUsuarioEditar: number = 0;
  nombreCompletoEditar: string = '';
  direccionEditar: string = '';
  perfilGeneralEditar: number = 0;
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
        nombreCompleto: '',
        direccion: '',
        perfilGeneral: 0,
        perfilDescripcion: '',
        fechaCreacion: ''
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
    this.idUsuarioEditar = this.todosLosUsuarios.find(x => x.id == Id).id;
    this.nombreCompletoEditar = this.todosLosUsuarios.find(x => x.id == Id).nombreCompleto;
    this.direccionEditar = this.todosLosUsuarios.find(x => x.id == Id).direccion;
    this.perfilGeneralEditar = this.todosLosUsuarios.find(x => x.id == Id).perfilGeneral;
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

  public ActualizarUsuario() {
    this.usuarioEditar.id = this.idUsuarioEditar;
    this.usuarioEditar.nombreCompleto = this.nombreCompletoEditar;
    this.usuarioEditar.direccion = this.direccionEditar;
    this.usuarioEditar.perfilGeneral = this.perfilGeneralEditar;
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



