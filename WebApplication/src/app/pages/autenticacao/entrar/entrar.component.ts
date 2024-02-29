import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../services/auth.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ILogin} from "../../../interfaces/ILogin";
import {IError} from "../../../interfaces/IError";

@Component({
  selector: 'app-entrar',
  templateUrl: './entrar.component.html'
})
export class EntrarComponent implements OnInit{
  formLogin: FormGroup;
  errors: IError[] = [];
  constructor(private service: AuthService,
              private formBuilder: FormBuilder) {
    this.formLogin = formBuilder.group({});
  }

  ngOnInit(): void {
    // @ts-ignore
    this.formLogin = this.formBuilder.group({
      login: ['', [Validators.required]],
      senha: ['', [Validators.required]]
    });
  }

  logar(){
    this.limparErros();

    if(this.formLogin.invalid){
      this.errors.length++;
      return;
    }

    let usuario = this.formLogin.getRawValue() as ILogin;

    this.service.logar(usuario).subscribe((response) => {
    }, error => {
      this.errors = error.error.errors;
    })
  }

  limparErros(){
    this.errors = [];
  }
}
