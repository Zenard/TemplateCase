using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace DB.Repository.Abstraction
{
    public interface IRepositoryService
    {
        public UniTask<TEntity> GetAsync<TEntity>(
            Func<TEntity, bool>? filter = null,
            Func<TEntity, object>? orderBy = null,
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new();
        public UniTask<IEnumerable<TEntity>> GetMultipleAsync<TEntity>(
            Func<TEntity, bool>? filter = null,
            Func<TEntity, object>? orderBy = null,
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new();

        public UniTask UpdateAsync<TEntity>(
            TEntity obj,
            string correlationId = "",
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new();


        public UniTask InsertAsync<TEntity>(
            TEntity obj,
            string correlationId = "",
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new();
    }
}