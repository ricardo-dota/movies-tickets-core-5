﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, IEntityBase , new()
    {
        Task<IEnumerable<T>> Getall();

        Task<IEnumerable<T>> Getall(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(int id);

        Task Add(T entity);

        Task Update(int id, T entity);

        Task Delete(int id);
    }
}
