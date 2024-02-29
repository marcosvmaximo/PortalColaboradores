import {Component, OnInit} from '@angular/core';
import {UsuarioService} from "../../../services/usuario.service";
import {IUsuario} from "../../../interfaces/IUsuario";
import {AuthService} from "../../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-lista',
  templateUrl: 'lista.component.html'
})
export class ListaUsuarioComponent implements OnInit{
  constructor(private service: UsuarioService,
              private authService: AuthService,
              private router: Router) {
  }
  public usuarios: IUsuario[] = [];

  ngOnInit(): void {
      this.obterUsuarios();
  }

  obterUsuarios(): void{
      this.service.obterUsuarios()
          .subscribe(
              response => {
                  this.usuarios = response.result as IUsuario[];
              },error => {
                  this.usuarios = [];
                  console.error(error);
              });
  }

    removerUsuario = (usuario: IUsuario): void => {
      if(this.authService.obterUsuarioLogado.id === usuario.id){
          alert("Não é possível remover você mesmo.");
          return;
      }

      this.service.removerUsuario(usuario)
          .subscribe(
              response => {
                  this.obterUsuarios();
              },error => {
                  console.error(error);
              });
  }
    editarUsuario = (usuario: IUsuario) => {
        this.router.navigate(['/usuarios/editar', usuario.login])
    }
}
