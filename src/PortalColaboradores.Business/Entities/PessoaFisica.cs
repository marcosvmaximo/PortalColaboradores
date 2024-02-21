using PortalColaboradores.Core;

namespace PortalColaboradores.Business.Entities;

public abstract class PessoaFisica : Entity
{
    private List<Telefone> _telefones = new();
    private List<Endereco> _enderecos = new();
    
    protected PessoaFisica(string nome, DateTime dataNascimento, string cpf, string rg)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Cpf = cpf;
        Rg = rg;
    }
    
    protected PessoaFisica(){}

    public string Nome { get; protected set; }
    public DateTime DataNascimento { get; protected set; }
    public string Cpf { get; protected set; }
    public string Rg { get; protected set; }
    public IReadOnlyCollection<Telefone> Telefones => _telefones;
    public IReadOnlyCollection<Endereco> Enderecos => _enderecos;

    public void AdicionarEndereco(Endereco endereco)
    {
        if (endereco == null)
            throw new ArgumentNullException("Endereço informado incorreto.");

        _enderecos.Add(endereco);
    }
    
    public void AdicionarTelefone(Telefone telefone)
    {
        if (telefone == null)
            throw new ArgumentNullException("Telefone informado incorreto.");

        _telefones.Add(telefone);
    }
}