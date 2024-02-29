import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {HttpClient} from "@angular/common/http";
import { Observable, tap} from 'rxjs';
import {ILogin} from "../interfaces/ILogin";
import {apiUrl} from "../enviroment/apiUrl";
import {IRegistrar} from "../interfaces/IRegistrar";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient,
              private router: Router) { }
  logar(usuario: ILogin) : Observable<any> {
    return this.httpClient.post<any>(apiUrl + "/Usuario/login", usuario).pipe(
      tap((resposta) => {
        if(!resposta.success) return;
          localStorage.setItem('token', btoa(JSON.stringify(resposta.result.token)));
          localStorage.setItem('user', btoa(JSON.stringify(resposta.result.user)));
        this.router.navigate(['']);
      }));
  }

  registrar(register: IRegistrar): Observable<any> {
    return this.httpClient.post<any>(apiUrl + "/Usuario/cadastrar", register).pipe(
        tap((resposta) => {
          if(!resposta.success) return;
            localStorage.setItem('token', btoa(JSON.stringify(resposta.result.token)));
            localStorage.setItem('user', btoa(JSON.stringify(resposta.result.user)));
          this.router.navigate(['']);
        }));
  }
  deslogar() {
    localStorage.clear();
    this.router.navigate(['']);
  }

  get obterUsuarioLogado(): ILogin {
    // @ts-ignore
    return (JSON.parse(atob(localStorage.getItem('user'))) as ILogin);
  }
  get obterIdUsuarioLogado(): string {
    // @ts-ignore
    return (JSON.parse(atob(localStorage.getItem('user'))) as ILogin).id;
  }

  get obterNomeUsuarioLogado(): string {
    // @ts-ignore
    return (JSON.parse(atob(localStorage.getItem('user'))) as ILogin).nome;
  }

  get obterTokenUsuario(): string {
    // @ts-ignore
    return JSON.parse(atob(localStorage.getItem('token')));
  }
  get logado(): boolean {
    return !!localStorage.getItem('token');
  }
}
