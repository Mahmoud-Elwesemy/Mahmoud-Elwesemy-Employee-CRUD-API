using Employee.Core.Domin.Pramter_Helper;
using Employee.Core.Domin.Repositories.contract;
using Employee.Core.Domin.Specifications;
using Employee.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Presistence.Repositories;
// This Is A Generic Repository Class That Implements The IGenericRepository Interface
public class GenericRepository<T, Tkey>:IGenericRepository<T,Tkey> where T : class where Tkey : IEquatable<Tkey>
{
    private readonly EmployeeContext _context;

    public GenericRepository(EmployeeContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetWithSpecAsync(ISpecification<T> specification)
    {
        return await _context.Set<T>()
                             .Where(specification.Criteria)
                             .ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllAsync(Pramter pramter)
    {
        if(pramter.PageSize != null && pramter.PageNumber != null)
        {
            return await _context.Set<T>()
                .Skip((pramter.PageNumber.Value - 1) * pramter.PageSize.Value)
                .Take(pramter.PageSize.Value)
                .ToListAsync();
        }
        else
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
    public async Task<T?> GetByIdAsync(Tkey id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
    public void UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public async Task DeleteAsync(Tkey id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if(entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
