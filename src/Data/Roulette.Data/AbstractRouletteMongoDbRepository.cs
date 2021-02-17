﻿using Roulette.Model;
using System;
using System.Threading.Tasks;
namespace Roulette.Data
{
    public abstract class AbstractRouletteMongoDbRepository : IRepository<Model.Roulette>
    {
        public StorageProvider StorageProvider => StorageProvider.MongoDb;
        public abstract Task<Model.Roulette> CreateAsync(Model.Roulette entity);
        public abstract Task<Model.Roulette> DeleteByIdAsync(Guid id);
        public abstract Task<Model.Roulette> FindByIdAsync(Guid id);
        public abstract Task<Model.Roulette> UpdateAsync(Model.Roulette entity);
    }
}