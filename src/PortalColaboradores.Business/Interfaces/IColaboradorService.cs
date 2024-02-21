using PortalColaboradores.Business.Models.Colaborador;

namespace PortalColaboradores.Business.Interfaces;

public interface IColaboradorService
{
    Task AdicionarColaborador(ColaboradorCommand command);
    Task AtualizarColaborador(int id, ColaboradorCommand command);
    Task RemoverColaborador(int id);
    Task AdicionarEndereco(EnderecoDto request);
    Task AdicionarTelefone(TelefoneDto request);
    Task RemoverEndereco(int id);
    Task RemoverTelefone(int id);

}