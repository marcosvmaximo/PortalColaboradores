using System.Dynamic;
using System.Net;
using System.Text.Json;
using PortalColaboradores.Core.ExternalServices;
using PortalColaboradores.Core.NotificationPattern;
using PortalColaboradores.Infra.ExternalService.Interfaces;
using PortalColaboradores.Infra.ExternalService.Models;

namespace PortalColaboradores.Infra.ExternalService.Rest;

public class ViaCepExternalService : IViaCepExternalService
{
    private readonly INotifyHandler _notify;

    public ViaCepExternalService(INotifyHandler notify)
    {
        _notify = notify;
    }
    public async Task<ResponseGeneric<ViaCepResponse>> Buscar(string cep)
    {
        var response = new ResponseGeneric<ViaCepResponse>();

        using (var client = new HttpClient())
        {
            var requestUri = $"https://viacep.com.br/ws/{cep}/json/";
            
            try
            {
                using (var request = await client.GetAsync(requestUri))
                {
                    request.EnsureSuccessStatusCode();
                    var contentBody = await request.Content.ReadAsStringAsync();
                    var objResponse = JsonSerializer.Deserialize<ViaCepResponse>(contentBody);

                    response.HttpCode = request.StatusCode;

                    if (request.IsSuccessStatusCode)
                    {
                        response.Data = objResponse;
                    }
                    else
                    {
                        response.Errors = JsonSerializer.Deserialize<ExpandoObject>(contentBody);
                        
                        foreach (var error in response.Errors)
                        {
                            await _notify.PublicarNotificacao(new Notification(error.Key, nameof(error.Value)));
                        }
                    }
                    
                }
            }
            catch (HttpRequestException ex)
            {
                response.HttpCode = HttpStatusCode.InternalServerError;
                response.Errors = JsonSerializer.Deserialize<ExpandoObject>(ex.Message);
                await _notify.PublicarNotificacao(new Notification("", "Ocorreu um erro ao realizar a requisição externa ao Via Cep."));
            }
            catch (Exception ex)
            {
                response.HttpCode = HttpStatusCode.InternalServerError;
                response.Errors = JsonSerializer.Deserialize<ExpandoObject>(ex.Message);
                await _notify.PublicarNotificacao(new Notification("", $"Ocorreu um erro ao realizar a requisição externa ao Via Cep: {response.Errors}"));
            }
        }

        return response;
    }
}