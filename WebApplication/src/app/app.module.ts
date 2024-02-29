import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/navegacao/home/home.component';
import { MenuComponent } from './pages/navegacao/menu/menu.component';
import { FooterComponent } from './pages/navegacao/footer/footer.component';
import {RouterModule, RouterOutlet} from "@angular/router";
import {rootRouterConfig} from "./app.route";
import {APP_BASE_HREF} from "@angular/common";
import { ListaUsuarioComponent } from './pages/usuario/lista/lista.component';
import { CadastroUsuarioComponent } from './pages/usuario/cadastro/cadastro.component';
import {ListaColaboradorComponent} from "./pages/colaborador/lista/lista.component";
import {CadastroColaboradorComponent} from "./pages/colaborador/cadastro/cadastro.component";
import { EntrarComponent } from './pages/autenticacao/entrar/entrar.component';
import { RegistrarComponent } from './pages/autenticacao/registrar/registrar.component';
import { EditRemoveTableComponent } from './components/edit-remove-table/edit-remove-table.component';
import {AuthService} from "./services/auth.service";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {UsuarioService} from "./services/usuario.service";
import {ColaboradorService} from "./services/colaborador.service";
import { EnderecoComponent } from './pages/colaborador/endereco/endereco.component';
import { TelefoneComponent } from './pages/colaborador/telefone/telefone.component';
import {EditarColaboradorComponent} from './pages/colaborador/editar/editar.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    FooterComponent,
    ListaUsuarioComponent,
    CadastroUsuarioComponent,
    ListaColaboradorComponent,
    CadastroColaboradorComponent,
    EntrarComponent,
    RegistrarComponent,
    EditRemoveTableComponent,
    EnderecoComponent,
    TelefoneComponent,
    EditarColaboradorComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    RouterOutlet,
    RouterModule.forRoot(rootRouterConfig, { useHash: false}),
  ],
  providers: [
    { provide: APP_BASE_HREF, useValue: '/' },
    AuthService,
    UsuarioService,
    ColaboradorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
