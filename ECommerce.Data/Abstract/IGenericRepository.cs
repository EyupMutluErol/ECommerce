using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Abstract;

public interface IGenericRepository<T>
{

    T GetById(int id);
    T GetOne(Expression<Func<T, bool>> predicate);
    List<T>GetAll(Expression<Func<T, bool>> predicate=null);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exists(Expression<Func<T, bool>> predicate);
    //bool exists =_genericRepository.Exists(p => p.Name == "Iphone 15"); true // false 
    IQueryable<T> Query();

}
