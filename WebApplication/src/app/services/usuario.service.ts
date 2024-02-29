import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {apiUrl} from "../enviroment/apiUrl";
import {IUsuario} from "../interfaces/IUsuario";
import {Observable} from "rxjs";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient,
              private authService: AuthService) { }

  obterUsuarios(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    return this.http.get<any>(`${apiUrl}/Usuario`, { headers });
  }

  removerUsuario(usuario: IUsuario): Observable<any>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.obterTokenUsuario}`
    });

    return this.http.delete(`${apiUrl}/Usuario/${usuario.login}`, {headers});
  }
}
