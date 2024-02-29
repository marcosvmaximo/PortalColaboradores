import {Routes} from "@angular/router";
import {HomeComponent} from "./pages/navegacao/home/home.component";
import {ListaUsuarioComponent} from "./pages/usuario/lista/lista.component";
import {ListaColaboradorComponent} from "./pages/colaborador/lista/lista.component";
import {RegistrarComponent} from "./pages/autenticacao/registrar/registrar.component";
import {EntrarComponent} from "./pages/autenticacao/entrar/entrar.component";
import {CadastroColaboradorComponent} from "./pages/colaborador/cadastro/cadastro.component";
import {CadastroUsuarioComponent} from "./pages/usuario/cadastro/cadastro.component";
import {UsuarioNaoAutenticadoGuard} from "./services/guards/usuario-nao-autenticado.guard";
import {UsuarioAutenticadoGuard} from "./services/guards/usuario-autenticado.guard";
import {EnderecoComponent} from "./pages/colaborador/endereco/endereco.component";
import {TelefoneComponent} from "./pages/colaborador/telefone/telefone.component";
import {EditarColaboradorComponent} from "./pages/colaborador/editar/editar.component";

export const rootRouterConfig: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    {path: 'home', component: HomeComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'entrar', component: EntrarComponent, canActivate: [UsuarioNaoAutenticadoGuard]},
    {path: 'registrar', component: RegistrarComponent, canActivate: [UsuarioNaoAutenticadoGuard]},
    {path: 'usuarios/lista', component: ListaUsuarioComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'usuarios/cadastrar', component: CadastroUsuarioComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'usuarios/editar/:id', component: CadastroUsuarioComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'colaboradores/lista', component: ListaColaboradorComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'colaboradores/cadastrar', component: CadastroColaboradorComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'colaboradores/editar/:id', component: EditarColaboradorComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'colaboradores/editar/:id/endereco', component: EnderecoComponent, canActivate: [UsuarioAutenticadoGuard]},
    {path: 'colaboradores/editar/:id/telefone', component: TelefoneComponent, canActivate: [UsuarioAutenticadoGuard]},
]