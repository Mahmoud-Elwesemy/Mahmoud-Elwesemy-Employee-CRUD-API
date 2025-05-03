using Employee.Core.Domin.Entities;
using Employee.Core.Domin.Repositories.contract;
using Employee.Core.Domin.UnitOfWork.Contract;
using Employee.Infrastructure.Presistence.Data;
using Employee.Infrastructure.Presistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Presistence.UnitOfWork;
// This Is A UnitOfWork Class That Implements The IUnitOfWork Interface
public class UnitOfWork:IUnitOfWork
{
    private readonly EmployeeContext _context;
    private readonly ConcurrentDictionary<string,object> _repositories;

    public UnitOfWork(EmployeeContext context)
    {
        _context = context;
        _repositories = new ConcurrentDictionary<string,object>();
    }
    // This Method Is Used To Get A Generic Repository For A Specific Entity Type
    public IGenericRepository<T,Tkey> GetRepository<T, Tkey>()
        where T : class
        where Tkey : IEquatable<Tkey>
    {
        // Check If The Repository Already Exists In The Dictionary Or Not
        return (IGenericRepository<T,Tkey>) _repositories.GetOrAdd(typeof(T).Name,new GenericRepository<T,Tkey>(_context));
    }
    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

}
