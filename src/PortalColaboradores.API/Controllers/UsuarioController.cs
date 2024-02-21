using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalColaboradores.API.Configuration.Auth;
using PortalColaboradores.API.Controllers.Common;
using PortalColaboradores.Business.Entities;
using PortalColaboradores.Business.Models.Usuario;
using PortalColaboradores.Core.NotificationPattern;

namespace PortalColaboradores.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : BaseController
{
   private readonly SignInManager<Usuario> _signInUser;
   private readonly UserManager<Usuario> _userManager;
   private readonly IdentityConfig _identityConfig;
   
   public UsuarioController(
      INotifyHandler notify,
      SignInManager<Usuario> signInUser,
      UserManager<Usuario> userManager, 
      IOptions<IdentityConfig> identityConfig) : base(notify)
   {
      _signInUser = signInUser;
      _userManager = userManager;
      _identityConfig = identityConfig.Value;
   }
   
   [HttpGet]
   [Authorize]
   [ProducesResponseType<IEnumerable<Usuario>>(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuarios()
   {
      var users = _userManager.Users.ToList();

      if (users == null || !users.Any())
      {
         return NotFound(new
         {
            HttpCode = 404,
            Success = false,
            Message = "Usuários não encontrados.",
         });
      }

      var result = users.Select(x => new
      {
         Id = x.Id,
         Login = x.UserName,
         Nome = x.Nome,
         Administrador = x.Administrador
      });
      
      return await CustomResponse(result);
   } 
   
   [HttpGet("{login}")]
   [Authorize]
   [ProducesResponseType<Usuario>(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   public async Task<ActionResult> ObterUsuarioPorLogin([FromRoute]string login)
   {
      var user = await _userManager.FindByNameAsync(login);

      if (user == null)
      {
         return NotFound(new
         {
            HttpCode = 404,
            Success = false,
            Message = "Usuário informado não encontrado.",
         });
      }

      return await CustomResponse(new
      {
         Id = user.Id,
         Login = user.UserName,
         Nome = user.Nome,
         Administrador = user.Administrador
      });
   }
   
   [HttpPost("login")]
   [ProducesResponseType<Usuario>(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<Usuario>> Login([FromBody]LoginDto request)
   {
      if (!ModelState.IsValid)
         return await CustomResponse(ModelState);

      var result = await _signInUser.PasswordSignInAsync(request.Login, request.Senha, false, true);
        
      if (result.Succeeded)
      {
         var user = await _userManager.FindByNameAsync(request.Login);
         
         return await CustomResponse(new
         {
            User = new
            {
               Id = user.Id,
               Login = user.UserName,
               Nome = user.Nome,
               Administrador = user.Administrador
            },
            ExpiresIn = _identityConfig.ExpiracaoHoras,
            Token = GerarJwt(),
         });
      }
      
      if (result.IsLockedOut)
      {
         await Notify("Usuário bloqueado.");
         return await CustomResponse();
      }
        
      await Notify("Usuário ou senha inválido.");
      return await CustomResponse();
   }

   [HttpPost("cadastrar")]
   [ProducesResponseType<Usuario>(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult<Usuario>> CadastrarUsuario([FromBody]CadastroDto request)
   {
      if (!ModelState.IsValid)
         return await CustomResponse(ModelState);

      Usuario user = new(request.Login, request.Nome, request.Administrador);

      var result = await _userManager.CreateAsync(user, request.Senha);
        
      if (!result.Succeeded)
      {
         foreach (var error in result.Errors)
         {
            await Notify(error.Description);
         }
        
         return await CustomResponse(result.Errors);
      }

      await _signInUser.SignInAsync(user, false);
        
      return await CustomResponse(new
      {
         User = new
         {
            Id = user.Id,
            Login = user.UserName,
            Nome = user.Nome,
            Administrador = user.Administrador
         },
         ExpiresIn = _identityConfig.ExpiracaoHoras,
         Token = GerarJwt()
      });
   }
   
   [HttpDelete("{login}")]
   [Authorize]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult> DeletarUsuario([FromRoute]string login)
   {
      var user = await _userManager.FindByNameAsync(login);

      if (user == null)
      {
         return NotFound(new
         {
            HttpCode = 404,
            Success = false,
            Message = "Usuário não encontrado.",
         });
      }

      var result = await _userManager.DeleteAsync(user);
      
      if (!result.Succeeded)
      {
         foreach (var error in result.Errors)
         {
            await Notify(error.Description);
         }
        
         return await CustomResponse(result.Errors);
      }

      return NoContent();
   }
   
   [HttpPut("{login}")]
   [Authorize]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<ActionResult> AtualizarUsuario([FromRoute]string login, [FromBody]AtualizarUsuarioDto request)
   {
      if (!ModelState.IsValid)
         return await CustomResponse(ModelState);
      
      var user = await _userManager.FindByNameAsync(login);
      
      if (user == null)
      {
         return NotFound(new
         {
            HttpCode = 404,
            Success = false,
            Message = "Usuário não encontrado.",
         });
      }
      
      if (!await _userManager.CheckPasswordAsync(user, request.SenhaAntiga))
      {
         await Notify("Senha informada incorreta.");
         return await CustomResponse();
      }

      user.Nome = request.Nome;
      user.Administrador = request.Administrador;
      
      var resultChangePassword = await _userManager.ChangePasswordAsync(user, request.SenhaAntiga, request.NovaSenha);
      var resultUpdate = await _userManager.UpdateAsync(user);

      if (!resultUpdate.Succeeded || !resultChangePassword.Succeeded)
      {
         foreach (var error in resultUpdate.Errors)
         {
            await Notify(error.Description);
         } 
         
         foreach (var error in resultChangePassword.Errors)
         {
            await Notify(error.Description);
         }
        
         return await CustomResponse();
      }
      
      return NoContent();
   }
   
   private string GerarJwt()
   {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_identityConfig.Secret);

      var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
      {
         Issuer = _identityConfig.Emissor,
         Audience = _identityConfig.ValidoEm,
         Expires = DateTime.UtcNow.AddHours(_identityConfig.ExpiracaoHoras),
         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
      });

      var encondedToken = tokenHandler.WriteToken(token);
      return encondedToken;
   }
}