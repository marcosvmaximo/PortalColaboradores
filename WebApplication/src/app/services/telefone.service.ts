import { Injectable } from '@angular/core';
import {AuthService} from "./auth.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {apiUrl} from "../enviroment/apiUrl";
import {ITelefone} from "../interfaces/ITelefone";

@Injectable({
  providedIn: 'root'
})
export class TelefoneService {

  constructor(private authService: AuthService,
              private http: HttpClient) { }

  cadastrarTelefone(telefone: ITelefone, idColaborador: string){
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    const data = {
      pessoaFisicaId: idColaborador,
      tipo: parseInt(telefone.tipo),
      numero: telefone.numero
    };

    return this.http.post(`${apiUrl}/colaboradores/telefone`, data,{headers});
  }

  apagar(idTelefone: string) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    return this.http.delete(`${apiUrl}/colaboradores/telefone/${idTelefone}`,{headers});
  }
}
