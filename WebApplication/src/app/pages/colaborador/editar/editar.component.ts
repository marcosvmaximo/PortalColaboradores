import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IError} from "../../../interfaces/IError";
import {ColaboradorService} from "../../../services/colaborador.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IColaborador} from "../../../interfaces/IColaborador";
import {IEndereco} from "../../../interfaces/IEndereco";
import {ITelefone} from "../../../interfaces/ITelefone";
import {ETipoEndereco} from "../../../enum/ETipoEndereco";
import {EnderecoService} from "../../../services/endereco.service";
import {ETipoTelefone} from "../../../enum/ETipoTelefone";

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html'
})
export class EditarColaboradorComponent implements OnInit{
  protected readonly ETipoEndereco = ETipoEndereco;
  protected readonly ETipoTelefone = ETipoTelefone;

  formEditarColaborador: FormGroup;
  errors: IError[] = [];

  enderecos: IEndereco[] = [];
  telefones: ITelefone[] = [];

  idColaboradorEditar: string = "";

  constructor(private service: ColaboradorService,
              private enderecoService: EnderecoService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) {
    this.formEditarColaborador = formBuilder.group({});
  }

  ngOnInit(): void {
    this.formEditarColaborador = this.formBuilder.group({
      matricula: ['', [Validators.required]],
      nome: ['', [Validators.required]],
      cpf: ['', [Validators.required]],
      dataNascimento: ['', [Validators.required]],
      rg: ['', [Validators.required]],
      tipo: ['', [Validators.required]],
      valorContribuicao: ['', []],
      dataAdmissao: ['', []]
    });

    this.route.params.subscribe(params => {
      this.buscarColaborador(parseInt(params['id']));
    });
  }

  buscarColaborador(idColaborador: number): void{
    this.service.obterColaborador(idColaborador).subscribe(
        response => {
          this.formEditarColaborador = this.formBuilder.group({
            matricula: [response.result.matricula, [Validators.required]],
            nome: [response.result.nome, [Validators.required]],
            cpf: [response.result.cpf, [Validators.required]],
            dataNascimento: [new Date(response.result.dataNascimento).toISOString().split("T")[0], [Validators.required]],
            rg: [response.result.rg, [Validators.required]],
            tipo: [response.result.tipo, [Validators.required]],
            valorContribuicao: [response.result.valorContribuicao, []],
            dataAdmissao: [response.result.dataAdmissao != null ? new Date(response.result.dataAdmissao).toISOString().split("T")[0] : "", []]
          });

          this.idColaboradorEditar = response.result.id;
          this.enderecos = response.result.enderecos;
          this.telefones = response.result.telefones;

          console.log(this.enderecos);
        },
        error => {
          this.errors = error.error.errors;
          console.error('Erro ao adicionar colaborador:', error);
        }
    )
  }
  editarColaborador(){
    this.limparErros();

    if(this.formEditarColaborador.invalid){
      this.errors.push({key: "", value: "Preencha todos campos obrigatórios (*)."});
      return;
    }

    const novoColaborador: IColaborador = this.formEditarColaborador.value as IColaborador;
    novoColaborador.id = this.idColaboradorEditar;

    this.service.editarColaborador(novoColaborador).subscribe(
        response => {
          alert('Colaborador atualizado com sucesso.');
          this.formEditarColaborador.reset();
        },
        error => {
          this.errors = error.error.errors;
          console.error('Erro ao adicionar colaborador:', error);
        }
    )
  }

  adicionarEndereco(){
    this.router.navigate([`/colaboradores/editar/${this.idColaboradorEditar}/endereco`]);
  }
  adicionarTelefone(){
    this.router.navigate([`/colaboradores/editar/${this.idColaboradorEditar}/telefone`]);
  }

  limparErros(){
    this.errors = [];
  }

  apagarTelefone() {
  }

  apagarEndereco(endereco: IEndereco) {
    this.enderecoService.apagar(endereco.id).subscribe(
        response => {
          this.buscarColaborador(parseInt(this.idColaboradorEditar));
        },
        error => {
          console.error("Erro ao apagar o endereço: ", error);
        }
    );
  }

  protected readonly parseInt = parseInt;
}

