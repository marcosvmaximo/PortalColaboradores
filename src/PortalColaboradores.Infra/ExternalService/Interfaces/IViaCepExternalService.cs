using PortalColaboradores.Core.ExternalServices;
using PortalColaboradores.Infra.ExternalService.Models;

namespace PortalColaboradores.Infra.ExternalService.Interfaces;

public interface IViaCepExternalService
{
    Task<ResponseGeneric<ViaCepResponse>> Buscar(string cep);
}