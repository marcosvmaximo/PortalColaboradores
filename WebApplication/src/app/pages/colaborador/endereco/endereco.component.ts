import {Component, Input, OnInit} from '@angular/core';
import {IError} from "../../../interfaces/IError";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {EnderecoService} from "../../../services/endereco.service";
import {IColaborador} from "../../../interfaces/IColaborador";
import {IEndereco} from "../../../interfaces/IEndereco";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-endereco',
  templateUrl: './endereco.component.html'
})
export class EnderecoComponent implements OnInit{
  @Input()

  errors: IError[] = [];
  formEndereco: FormGroup;
  private idColaborador: string = "";

  constructor(private service: EnderecoService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) {
    this.formEndereco = formBuilder.group({});
  }

  ngOnInit(): void {
    this.formEndereco = this.formBuilder.group({
      cep: ['', [Validators.required]],
      logradouro: ['', [Validators.required]],
      numero: ['', [Validators.required]],
      bairro: ['', [Validators.required]],
      cidade: ['', [Validators.required]],
      tipo: ['1', [Validators.required]],
    });

    this.route.params.subscribe(params => {
      this.idColaborador = params['id'];
    });
  }

  cadastrarEndereco() {
    this.limparErros();

    if(this.formEndereco.invalid){
      this.errors.push({key: "", value: "Preencha todos campos obrigatórios (*)."});
      return;
    }

    const endereco: IEndereco = this.formEndereco.value as IEndereco;

    this.service.cadastrarEndereco(endereco, this.idColaborador).subscribe(
        response => {
          alert('Endereço adicionado com sucesso.');
          this.formEndereco.reset();
        },
        error => {
          this.errors = error.error.errors;
          console.error('Erro ao adicionar o endereço:', error);
        }
    )
  }
  limparErros(){
    this.errors = [];
  }
}
