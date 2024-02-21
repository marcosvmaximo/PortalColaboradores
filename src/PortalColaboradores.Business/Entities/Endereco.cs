using PortalColaboradores.Business.Entities;
using PortalColaboradores.Business.Enum;
using PortalColaboradores.Business.Validations;
using PortalColaboradores.Core;

namespace PortalColaboradores.Business.Entities;

public sealed class Endereco : Entity
{
    public Endereco(ETipoEndereco tipo, string cep, string logradouro, string numeroComplemento, string bairro, string cidade, PessoaFisica pessoaFisica)
    {
        Tipo = tipo;
        Cep = cep;
        Logradouro = logradouro;
        NumeroComplemento = numeroComplemento;
        Bairro = bairro;
        Cidade = cidade;
        PessoaFisica = pessoaFisica;
        PessoaFisicaId = pessoaFisica.Id;

        Validate<EnderecoValidation, Endereco>();
    }

    protected Endereco(){}
    
    public ETipoEndereco Tipo { get; private set; }
    public string Cep { get; private set; }
    public string Logradouro { get; private set; }
    public string NumeroComplemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public int PessoaFisicaId { get; private set; }
    public PessoaFisica PessoaFisica { get; private set; }
}