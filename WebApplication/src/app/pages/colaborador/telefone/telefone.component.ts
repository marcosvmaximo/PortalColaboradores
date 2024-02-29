import {Component, OnInit} from '@angular/core';
import { IError } from 'src/app/interfaces/IError';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {TelefoneService} from "../../../services/telefone.service";
import {IEndereco} from "../../../interfaces/IEndereco";
import {ITelefone} from "../../../interfaces/ITelefone";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-telefone',
  templateUrl: './telefone.component.html'
})
export class TelefoneComponent implements OnInit{
  errors: IError[] = [];
  formTelefone: FormGroup;
  private idColaborador: string = '';

  constructor(private service: TelefoneService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute) {
    this.formTelefone = this.formBuilder.group({});
  }

  ngOnInit(): void {
    this.formTelefone = this.formBuilder.group({
      numero: ['', [Validators.required]],
      tipo: ['1', [Validators.required]]
    });

    this.route.params.subscribe(params => {
      this.idColaborador = params['id'];
    });
  }

  cadastrarTelefone() {
    this.limparErros();

    if(this.formTelefone.invalid){
      this.errors.push({key: "", value: "Preencha todos campos obrigatórios (*)."});
      return;
    }

    const telefone: ITelefone = this.formTelefone.value as ITelefone;
console.log(telefone)
    this.service.cadastrarTelefone(telefone, this.idColaborador).subscribe(
        response => {
          alert('Telefone adicionado com sucesso.');
          this.formTelefone.reset();
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
