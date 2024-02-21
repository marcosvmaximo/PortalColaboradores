using PortalColaboradores.Business.Entities;
using PortalColaboradores.Core;

namespace PortalColaboradores.Business.Interfaces;

public interface IColaboradorRepository : IRepository<Colaborador>, IDisposable
{
    Task<Telefone> ObterTelefonePorId(int id);
    Task<Endereco> ObterEnderecoPorId(int id);
    Task RemoverTelefone(Telefone telefone);
    Task RemoverEndereco(Endereco endereco);
}