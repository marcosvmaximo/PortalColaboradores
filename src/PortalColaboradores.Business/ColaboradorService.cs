using PortalColaboradores.Business.Entities;
using PortalColaboradores.Business.Interfaces;
using PortalColaboradores.Business.Models.Colaborador;
using PortalColaboradores.Core.NotificationPattern;

namespace PortalColaboradores.Business;

public class ColaboradorService : IColaboradorService
{
    private readonly IColaboradorRepository _repository;
    private readonly INotifyHandler _notify;

    public ColaboradorService(IColaboradorRepository repository, INotifyHandler notify)
    {
        _repository = repository;
        _notify = notify;
    }

    public async Task AdicionarColaborador(ColaboradorCommand command)
    {
        var existeColaboradorCadastrado = await _repository.GetByFilter(x => x.Matricula == command.Matricula || x.Cpf == command.Cpf);
        if (existeColaboradorCadastrado.Any())
        {
            await _notify.PublicarNotificacao(new Notification(command.Nome,"Colaborador já está cadastrado."));
            return;
        }
        
        var colaborador = new Colaborador(command.Nome, command.DataNascimento, command.Cpf, command.Rg,
            command.Matricula, command.Tipo, command.DataAdmissao, command.ValorContribuicao);

        await _repository.Add(colaborador);
        await _repository.UnityOfWork.Commit();
    }

    public async Task AtualizarColaborador(int id, ColaboradorCommand command)
    {
        var colaborador = await _repository.GetById(id);
        if (colaborador is null)
        {
            await _notify.PublicarNotificacao(new Notification("Colaborador Id","Colaborador não encontrado."));
            return;
        }
        
        colaborador.Atualizar(command);
        
        await _repository.Update(colaborador);
        await _repository.UnityOfWork.Commit();
    }

    public async Task RemoverColaborador(int id)
    {
        var colaborador = await _repository.GetById(id);
        if (colaborador is null)
        {
            await _notify.PublicarNotificacao(new Notification("Colaborador Id","Colaborador não encontrado."));
            return;
        }

        await _repository.Remove(colaborador);
        await _repository.UnityOfWork.Commit();
    }

    public async Task AdicionarEndereco(EnderecoDto request)
    {
        var colaborador = await _repository.GetById(request.PessoaFisicaId);
        if (colaborador is null)
        {
            await _notify.PublicarNotificacao(new Notification("Colaborador Id","Colaborador não encontrado."));
            return;
        }
        
        var endereco = new Endereco(request.Tipo, request.Cep, request.Logradouro, request.NumeroComplemento, request.Bairro, request.Cidade, colaborador);
        colaborador.AdicionarEndereco(endereco);

        await _repository.Update(colaborador);
        await _repository.UnityOfWork.Commit();
    }

    public async Task AdicionarTelefone(TelefoneDto request)
    {
        var colaborador = await _repository.GetById(request.PessoaFisicaId);
        if (colaborador is null)
        {
            await _notify.PublicarNotificacao(new Notification("Colaborador Id","Colaborador não encontrado."));
            return;
        }

        var numeroExistente = colaborador.Telefones.Any(x => x.Numero == request.Numero);
        if(numeroExistente)
        {
            await _notify.PublicarNotificacao(new Notification(request.Numero,"Número de Telefone informado, já está cadastrado ao Colaborador informado."));
            return;
        }
        
        var telefone = new Telefone(request.Tipo, request.Numero, colaborador);
        colaborador.AdicionarTelefone(telefone);

        await _repository.Update(colaborador);
        await _repository.UnityOfWork.Commit();
    }

    public async Task RemoverEndereco(int id)
    {
        var endereco = await _repository.ObterEnderecoPorId(id);
        if (endereco == null)
        {
            await _notify.PublicarNotificacao(new Notification(nameof(id),"Endereco informado não encontrado."));
            return;
        }
        
        await _repository.RemoverEndereco(endereco);
        await _repository.UnityOfWork.Commit();
    }

    public async Task RemoverTelefone(int id)
    {
        var telefone = await _repository.ObterTelefonePorId(id);
        if (telefone == null)
        {
            await _notify.PublicarNotificacao(new Notification(nameof(id),"Telefone informado não encontrado."));
            return;
        }
        
        await _repository.RemoverTelefone(telefone);
        await _repository.UnityOfWork.Commit();
    }

    public Task<EnderecoDto> ObterEnderecoPorCep(string cep)
    {
        throw new NotImplementedException();
    }
}