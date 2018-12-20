using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EmployeeManagementContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(EmployeeManagementContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public T Get(long id)
        {
  
            return null;
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public T InsertWithReturn(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();

        }


        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
        }


    }
}
