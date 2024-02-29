import { Injectable } from '@angular/core';
import {AuthService} from "./auth.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {apiUrl} from "../enviroment/apiUrl";
import {IEndereco} from "../interfaces/IEndereco";

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {

  constructor(private authService: AuthService,
              private http: HttpClient) { }

  cadastrarEndereco(endereco: IEndereco, idColaborador: string){
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    const data = {
      pessoaFisicaId: idColaborador,
      tipo: parseInt(endereco.tipo),
      cep: endereco.cep,
      logradouro: endereco.logradouro,
      numeroComplemento: String(endereco.numeroComplemento),
      bairro: endereco.bairro,
      cidade: endereco.cidade
    };

    return this.http.post(`${apiUrl}/colaboradores/endereco`, data,{headers});
  }

  apagar(idEndereco: string) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    return this.http.delete(`${apiUrl}/colaboradores/endereco/${idEndereco}`,{headers});
  }
}
