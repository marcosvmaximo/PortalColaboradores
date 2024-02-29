import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent{
  constructor(public service: AuthService,
              private router: Router) {
  }

    deslogar(){
      this.service.deslogar();
      this.router.navigate(['entrar'])
    }
}
