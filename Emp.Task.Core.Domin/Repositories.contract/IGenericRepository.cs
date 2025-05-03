using Employee.Core.Domin.Entities;
using Employee.Core.Domin.Pramter_Helper;
using Employee.Core.Domin.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Domin.Repositories.contract;
    // This Is A Generic Repository Interface That Contains The Basic CRUD Operations
    public interface IGenericRepository<T, Tkey> where T : class where Tkey : IEquatable<Tkey>
    {
    // This Is GRUD Operations Methods   
        Task<IEnumerable<T>> GetWithSpecAsync(ISpecification<T> specification);
        Task<IEnumerable<T>> GetAllAsync(Pramter pramter);
        Task<T?> GetByIdAsync(Tkey id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task DeleteAsync(Tkey id);
    }