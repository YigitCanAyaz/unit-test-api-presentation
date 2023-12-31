﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTestApiPresentation.Api.Contexts;

namespace UnitTestApiPresentation.Api.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApiUnitTestDBContext _dbContext;
        private readonly DbSet<TEntity> _entity;

        public Repository(ApiUnitTestDBContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _entity.Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _entity.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            _dbContext.SaveChanges();
        }
    }
}
