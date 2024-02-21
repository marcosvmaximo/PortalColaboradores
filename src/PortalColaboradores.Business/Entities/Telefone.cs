using PortalColaboradores.Business.Entities;
using PortalColaboradores.Business.Enum;
using PortalColaboradores.Business.Validations;
using PortalColaboradores.Core;

namespace PortalColaboradores.Business.Entities;

public sealed class Telefone : Entity
{
    public Telefone(ETipoTelefone tipo, string numero, PessoaFisica pessoaFisica)
    {
        Tipo = tipo;
        Numero = numero;
        PessoaFisica = pessoaFisica;
        PessoaFisicaId = pessoaFisica.Id;

        Validate<TelefoneValidation, Telefone>();
    }
    
    protected Telefone(){}

    public ETipoTelefone Tipo { get; private set; }
    public string Numero { get; private set; }
    public int PessoaFisicaId { get; private set; }
    
    // Ef
    public PessoaFisica PessoaFisica { get; private set; }
}