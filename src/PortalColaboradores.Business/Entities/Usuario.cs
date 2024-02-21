using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PortalColaboradores.Business.Validations;
using PortalColaboradores.Core;

namespace PortalColaboradores.Business.Entities;

public sealed class Usuario : IdentityUser
{
    public Usuario(string login, string nome, bool administrador)
    {
        Administrador = administrador;
        UserName = login;
        Nome = nome;
    }
    
    protected Usuario(){}
    
    public string Nome { get; set; }
    public bool Administrador { get; set; }
}