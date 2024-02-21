using System.Text.Json;
using PortalColaboradores.Business.Enum;
using PortalColaboradores.Business.Models.Colaborador;
using PortalColaboradores.Business.Validations;
using PortalColaboradores.Core;

namespace PortalColaboradores.Business.Entities;

public sealed class Colaborador : PessoaFisica
{
    public Colaborador(string nome, DateTime dataNascimento, string cpf, string rg, string matricula, ETipoColaborador tipo, DateTime? dataAdmissao, decimal? valorContribuicao)
        : base(nome, dataNascimento, cpf, rg)
    {
        Matricula = matricula;
        Tipo = tipo;
        DataAdmissao = dataAdmissao;
        ValorContribuicao = valorContribuicao;

        Validate<ColaboradorValidation, Colaborador>();
    }

    protected Colaborador(){}
    
    public string Matricula { get; private set; }
    public ETipoColaborador Tipo { get; private set; }
    public DateTime? DataAdmissao { get; private set; }
    public decimal? ValorContribuicao { get; private set; }

    public void Atualizar(ColaboradorCommand command)
    {
        Nome = command.Nome;
        DataNascimento = command.DataNascimento;
        Cpf = command.Cpf;
        Rg = command.Rg;
        Matricula = command.Matricula;
        Tipo = command.Tipo;
        DataAdmissao = command.DataAdmissao;
        ValorContribuicao = command.ValorContribuicao;
        
        Validate<ColaboradorValidation, Colaborador>();
    }
}