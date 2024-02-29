import {Injectable, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {apiUrl} from "../enviroment/apiUrl";
import {AuthService} from "./auth.service";
import {IColaborador} from "../interfaces/IColaborador";

@Injectable({
  providedIn: 'root'
})
export class ColaboradorService {

  constructor(private authService: AuthService,
              private http: HttpClient) { }


  obterColaboradores(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    return this.http.get<any>(`${apiUrl}/colaboradores`, { headers });
  }

  removerColaborador(colaborador: IColaborador): Observable<any>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });
    console.log("objeto que está sendo enviado: ", colaborador);
    return this.http.delete(`${apiUrl}/colaboradores/${colaborador.id}`, {headers});
  }

  cadastrarColaborador(colaborador: IColaborador): Observable<any>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });
    const data = {
      nome: colaborador.nome,
      dataNascimento: new Date(colaborador.dataNascimento).toISOString(),
      cpf: colaborador.cpf,
      rg: colaborador.rg,
      matricula: colaborador.matricula,
      tipo: parseInt(String(colaborador.tipo)),
      dataAdmissao: colaborador.dataAdmissao ? new Date(colaborador.dataAdmissao).toISOString() : null,
      valorContribuicao: colaborador.valorContribuicao || null
    };

    return this.http.post(`${apiUrl}/colaboradores`, data,{headers});
  }

  obterColaborador(idColaborador: number) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    return this.http.get<any>(`${apiUrl}/colaboradores/${idColaborador}`, { headers });
  }

  editarColaborador(colaborador: IColaborador): Observable<any>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    const data = {
      nome: colaborador.nome,
      dataNascimento: new Date(colaborador.dataNascimento).toISOString(),
      cpf: colaborador.cpf,
      rg: colaborador.rg,
      matricula: colaborador.matricula,
      tipo: parseInt(String(colaborador.tipo)),
      dataAdmissao: colaborador.dataAdmissao ? new Date(colaborador.dataAdmissao).toISOString() : null,
      valorContribuicao: colaborador.valorContribuicao || null
    };

    return this.http.put(`${apiUrl}/colaboradores/${colaborador.id}`, data,{headers});
  }
}
