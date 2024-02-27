using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalColaboradores.API.Controllers.Common;
using PortalColaboradores.Business.Interfaces;
using PortalColaboradores.Business.Models.Colaborador;
using PortalColaboradores.Core.NotificationPattern;
using PortalColaboradores.Infra.ExternalService.Interfaces;

namespace PortalColaboradores.API.Controllers;

[Route("api/v1/colaboradores")]
// [Authorize]
public class ColaboradorController : BaseController
{
    private readonly IColaboradorRepository _repository;
    private readonly IColaboradorService _service;
    private readonly IViaCepExternalService _cepService;

    public ColaboradorController(INotifyHandler notifyHandler, IColaboradorRepository repository, IColaboradorService service, IViaCepExternalService cepService) : base(notifyHandler)
    {
        _repository = repository;
        _service = service;
        _cepService = cepService;
    }

    [HttpGet]
    public async Task<ActionResult> ObterTodosColaboradores()
    {
        var colaboradores = await _repository.GetAll();
        if (colaboradores == null || !colaboradores.Any())
        {
            return NotFound(new
            {
                HttpCode = 404,
                Success = false,
                Message = "Colaboradores não encontrados.",
            });
        }
        
        return await CustomResponse(colaboradores);
    }
    
    // Boas praticas diria para o identificador de busca de um colaborador não fosse o ID do banco, e sim alguma outra chave única
    [HttpGet("{id:int}")]
    public async Task<ActionResult> ObterColaboradorPorId([FromRoute]int id)
    {
        var colaborador = await _repository.GetById(id);
        if (colaborador == null)
        {
            return NotFound(new
            {
                HttpCode = 404,
                Success = false,
                Message = "Colaboradores não encontrados.",
            });
        }
        
        return await CustomResponse(colaborador);
    }

    [HttpPost]
    public async Task<ActionResult> AdicionarColaborador([FromBody]ColaboradorCommand request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        await _service.AdicionarColaborador(request);
        
        return await CustomResponse();
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> AtualizarColaborador([FromRoute]int id, [FromBody]ColaboradorCommand request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        await _service.AtualizarColaborador(id, request);
        
        return await CustomResponse();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoverColaborador([FromRoute]int id)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        await _service.RemoverColaborador(id);
        
        return await CustomResponse();
    }
    
    [HttpPost("endereco")]
    public async Task<ActionResult> AdicionarEnderecoAoColaborador([FromBody]EnderecoDto request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);
        
        await _service.AdicionarEndereco(request);
        
        return await CustomResponse();
    }   
    
    [HttpPost("telefone")]
    public async Task<ActionResult> AdicionarTelefoneAoColaborador([FromBody]TelefoneDto request)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        await _service.AdicionarTelefone(request);
        
        return await CustomResponse();
    }   
    
    [HttpDelete("endereco/{id:int}")]
    public async Task<ActionResult> RemoverEndereco([FromRoute]int id)
    {
        await _service.RemoverEndereco(id);

        return await CustomResponse();
    }
    
    [HttpDelete("telefone/{id:int}")]
    public async Task<ActionResult> RemoverTelefone([FromRoute]int id)
    {
        await _service.RemoverTelefone(id);

        return await CustomResponse();
    }

    [HttpGet("endereco/{cep}")]
    public async Task<ActionResult<EnderecoDto>> BuscarCep([FromRoute]string cep)
    {
        var result = await _cepService.Buscar(cep);
        
        if (result.HttpCode != HttpStatusCode.OK)
            return await CustomResponse();
        
        return await CustomResponse(result.Data!);
    }
}