import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../services/auth.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {IError} from "../../../interfaces/IError";
import {ILogin} from "../../../interfaces/ILogin";
import {IRegistrar} from "../../../interfaces/IRegistrar";

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html'
})
export class RegistrarComponent implements OnInit{
  formRegister: FormGroup;
  errors: IError[] = [];
  constructor(private service: AuthService,
              private formBuilder: FormBuilder) {
    this.formRegister = formBuilder.group({});
  }

  ngOnInit(): void {
    this.formRegister = this.formBuilder.group({
      login: ['', [Validators.required]],
      nome: ['', [Validators.required]],
      senha: ['', [Validators.required]],
      confirmarSenha: ['', [Validators.required]]
    });
  }

  register(){
    this.limparErros();

    if(this.formRegister.invalid){
      this.errors.length++;
      return;
    }

    let register = this.formRegister.getRawValue() as IRegistrar;

    this.service.registrar(register).subscribe((response) => {
    }, error => {
      this.errors = error.error.errors;
    })
  }

  limparErros(){
    this.errors = [];
  }
}
