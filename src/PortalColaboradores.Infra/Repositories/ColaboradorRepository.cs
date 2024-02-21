using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PortalColaboradores.Business.Entities;
using PortalColaboradores.Business.Interfaces;
using PortalColaboradores.Core;
using PortalColaboradores.Infra.Data;

namespace PortalColaboradores.Infra.Repositories;

public class ColaboradorRepository : IColaboradorRepository
{
    private readonly DataContext _context;

    public ColaboradorRepository(DataContext context)
    {
        _context = context;
    }

    public IUnityOfWork UnityOfWork => _context;

    public async Task<IEnumerable<Colaborador>> GetAll()
    {
        return _context.Colaboradores
            .Include(x => x.Enderecos)
            .Include(x => x.Telefones)
            .ToList();
    }

    public async Task<IEnumerable<Colaborador>> GetByFilter(Expression<Func<Colaborador, bool>> filter)
    {
        return _context.Colaboradores 
            .Include(x => x.Enderecos)
            .Include(x => x.Telefones)
            .Where(filter).ToList();
    }

    public async Task<Colaborador?> GetById(int id)
    {
        return await _context.Colaboradores 
            .Include(x => x.Enderecos)
            .Include(x => x.Telefones)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(Colaborador entity)
    {
        await _context.Colaboradores.AddAsync(entity);
    }

    public async Task Update(Colaborador entity)
    {
        _context.Colaboradores.Update(entity);
    }

    public async Task Remove(Colaborador entity)
    {
        _context.Colaboradores.Remove(entity);
    }

    public async Task<Telefone?> ObterTelefonePorId(int id)
    {
        return await _context.Telefones.FindAsync(id);
    }

    public async Task<Endereco?> ObterEnderecoPorId(int id)
    {
        return await _context.Enderecos.FindAsync(id);
    }

    public async Task RemoverTelefone(Telefone telefone)
    {
        _context.Telefones.Remove(telefone);
    }

    public async Task RemoverEndereco(Endereco endereco)
    {
        _context.Enderecos.Remove(endereco);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}