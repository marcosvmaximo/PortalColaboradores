using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PortalColaboradores.Core.NotificationPattern;

namespace PortalColaboradores.API.Controllers.Common;

public class BaseController : ControllerBase
{
    protected readonly INotifyHandler _notifyHandler;

    public BaseController(INotifyHandler notifyHandler)
    {
        _notifyHandler = notifyHandler;
    }

    protected async Task<ActionResult> CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
            await NotifyModelState(modelState);

        return await CustomResponse();
    }

    protected async Task<ActionResult> CustomResponse(object result = null)
    {
        if (await _notifyHandler.AnyNotifications())
        {
            var notifications = await _notifyHandler.GetNotifications();

            return BadRequest(new
            {
                HttpCode = 400,
                Success = false,
                Message = "Ocorreu um erro ao enviar a requisição.",
                Errors = _notifyHandler.GetNotifications().Result.Select(x => new { Key = x.Property, Value = x.Message })
            }); ;
        }

        return Ok(new
        {
            HttpCode = 200,
            Success = true,
            Message = "Requisição enviada com sucesso.",
            Result = result
        });
    }

    protected async Task NotifyModelState(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        
        foreach (var erro in erros)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            await Notify(erroMsg);
        }
    }

    protected async Task Notify(string message)
    {
        await _notifyHandler.PublicarNotificacao(new Notification(null, message));
    }

    protected async Task Notify(string key, string message)
    {
        await _notifyHandler.PublicarNotificacao(new Notification(key, message));
    }
}