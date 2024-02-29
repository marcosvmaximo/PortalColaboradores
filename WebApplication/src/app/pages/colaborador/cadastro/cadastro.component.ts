import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IError} from "../../../interfaces/IError";
import {ColaboradorService} from "../../../services/colaborador.service";
import {IColaborador} from "../../../interfaces/IColaborador";
import {ActivatedRoute, Router} from "@angular/router";
import {ETipoEndereco} from "../../../enum/ETipoEndereco";
import {ITelefone} from "../../../interfaces/ITelefone";
import {IEndereco} from "../../../interfaces/IEndereco";

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html'
})
export class CadastroColaboradorComponent implements OnInit{
  formColaborador: FormGroup;
  errors: IError[] = [];

  telefones: ITelefone[] = [];
  enderecos: IEndereco[] = [];

  constructor(private service: ColaboradorService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) {
    this.formColaborador = formBuilder.group({});
  }

  ngOnInit(): void {
    this.formColaborador = this.formBuilder.group({
      matricula: ['1', [Validators.required]],
      nome: ['Teste', [Validators.required]],
      cpf: ['11647322693', [Validators.required]],
      dataNascimento: ['11/02/2003', [Validators.required]],
      rg: ['20749999', [Validators.required]],
      tipo: ['2', [Validators.required]],
      valorContribuicao: ['', []],
      dataAdmissao: ['', []]
    });
  }

  cadastrarColaborador(){
    this.limparErros();

    if(this.formColaborador.invalid){
      this.errors.push({key: "", value: "Preencha todos campos obrigatórios (*)."});
      return;
    }

    const novoColaborador: IColaborador = this.formColaborador.value as IColaborador;

    this.service.cadastrarColaborador(novoColaborador).subscribe(
        response => {
          alert('Colaborador adicionado com sucesso.');
          this.formColaborador.reset();
        },
        error => {
          this.errors = error.error.errors;
          console.error('Erro ao adicionar colaborador:', error);
        }
    )
  }

  adicionarEndereco(){
    // Necessário criar novas rotas que aceitem o colaborador junto com uma lista de endereço
    // this.router.navigate([`/colaboradores/editar/${this.idColaborador}/endereco`]);
  }

  adicionarTelefone(){
    // this.router.navigate([`/colaboradores/editar/${this.idColaborador}/telefone`]);
  }

  limparErros(){
    this.errors = [];
  }

  protected readonly ETipoEndereco = ETipoEndereco;
  protected readonly parseInt = parseInt;

  apagarTelefone() {

  }

  apagarEndereco() {

  }

}
