import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IUsuario} from "../../../interfaces/IUsuario";
import {ColaboradorService} from "../../../services/colaborador.service";
import {IColaborador} from "../../../interfaces/IColaborador";
import {ETipoColaborador} from "../../../enum/ETipoColaborador";
import {Router} from "@angular/router";

@Component({
  selector: 'app-lista',
  templateUrl: 'lista.component.html'
})
export class ListaColaboradorComponent implements OnInit{
  protected readonly ETipoColaborador = ETipoColaborador;
  public colaboradores: IColaborador[] = [];

  constructor(private service: ColaboradorService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.obterColaboradores();
  }

  obterColaboradores(){
    this.service.obterColaboradores()
        .subscribe(
            response => {
              this.colaboradores = response.result as IColaborador[];
            },error => {
              this.colaboradores = [];
              console.error(error);
            });
  }

  editarColaborador = (colaborador: IColaborador): void => {
      this.router.navigate(['/colaboradores/editar', colaborador.id])
  }

  removerColaborador = (colaborador: IColaborador): void => {
    this.service.removerColaborador(colaborador)
        .subscribe(
            response => {
              this.obterColaboradores();
            },error => {
              console.error(error);
            });
  }
}
