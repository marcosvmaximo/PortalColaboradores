import { Component } from '@angular/core';
import {AuthService} from "../../../services/auth.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  nome: string = this.authService.obterNomeUsuarioLogado;
  constructor(private authService: AuthService) {
  }
}
