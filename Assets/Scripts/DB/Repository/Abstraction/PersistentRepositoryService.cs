using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using SQLite;
using UnityEngine;
using Utility;

namespace DB.Repository.Abstraction
{
    public class PersistentRepositoryService : IRepositoryService
    {
        private readonly string _dbPath;
        private readonly SemaphoreSlim _dbLock = new(1, 1);

        public PersistentRepositoryService(string dbPath, string dbName = "/persistent.db")
        {
            _dbPath = Application.persistentDataPath + dbName;
        }

        public async UniTask<TEntity> GetAsync<TEntity>(Func<TEntity, bool> filter = null, Func<TEntity, object> orderBy = null, CancellationToken token = default,
            bool sendToBackThread = false) where TEntity : class, new()
        {
            // ── 1.  acquire a slot  ────────────────────────────────

            var entered = false;
            try
            {
                await _dbLock.WaitAsync(token);
                entered = true;
                if (sendToBackThread)
                    return await TaskUtilties.RunTask(() => Read<TEntity>(filter, orderBy).FirstOrDefault(), token);

                return Read<TEntity>(filter, orderBy).FirstOrDefault();
            }
            finally
            {
                // ── 2.  release the slot  ───────────────────────────
                if(entered)
                    _dbLock.Release();
                else
                    Debug.LogWarning($"[RepositoryBase] Release called on a lock that is already released. " +
                                     $"Current count: {_dbLock.CurrentCount}");
            }
        }

        public async UniTask<IEnumerable<TEntity>> GetMultipleAsync<TEntity>(
            Func<TEntity, bool>? filter = null,
            Func<TEntity, object>? orderBy = null,
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new()
        {
            // ── 1.  acquire a slot  ────────────────────────────────

            var entered = false;
            
            try
            {
                await _dbLock.WaitAsync(token);
                entered = true;
                if (sendToBackThread)
                    return await TaskUtilties.RunTask(() => Read<TEntity>(filter, orderBy), token);

                return Read<TEntity>(filter, orderBy);
            }
            finally
            {
                // ── 2.  release the slot  ───────────────────────────
                if(entered)
                    _dbLock.Release();
                else
                    Debug.LogWarning($"[RepositoryBase] Release called on a lock that is already released. " +
                                     $"Current count: {_dbLock.CurrentCount}");
                
            }
        }

        private IEnumerable<TEntity> Read<TEntity>(
            Func<TEntity, bool>? filter,
            Func<TEntity, object>? orderBy)
            where TEntity : class, new()
        {
            IEnumerable<TEntity> result = Enumerable.Empty<TEntity>();
            
            try
            {
                using var db = new SQLiteConnection(_dbPath);
                var q = db.Table<TEntity>();

                if (filter != null && orderBy != null)
                    result = q.Where(filter).OrderBy(orderBy);
                else if (filter != null)
                    result = q.Where(filter);
                else if (orderBy != null)
                    result = q.OrderBy(orderBy);
                else
                    result = q;

                return result.ToList();
            }
            finally
            {
                
              
            }
        }

        public async UniTask UpdateAsync<TEntity>(
            TEntity obj,
            string correlationId = "",
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var entered = false;
            try
            {
                await _dbLock.WaitAsync(token);
                entered = true;
                token.ThrowIfCancellationRequested();

                if (sendToBackThread)
                {
                    await TaskUtilties.RunTask(() =>
                    {
                        using var db = new SQLiteConnection(_dbPath);
                        db.Update(obj);
                    }, token);
                }
                else
                {
                    using var db = new SQLiteConnection(_dbPath);
                    db.Update(obj);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UpdateAsync] Update failed for {typeof(TEntity).Name}: {ex.Message} " +
                               $"Stack: {ex.StackTrace} Correlation: {correlationId}");
                throw;
            }
            finally
            {
                if(entered)
                    _dbLock.Release();
            }
        }

        public async UniTask InsertAsync<TEntity>(
            TEntity obj,
            string correlationId = "",
            CancellationToken token = default,
            bool sendToBackThread = false)
            where TEntity : class, new()
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            await WithDbLock(db =>
            {
                db.Insert(obj);
                return UniTask.CompletedTask;
            }, sendToBackThread, token);
        }

        private async UniTask WithDbLock(Func<SQLiteConnection, UniTask> action, bool sendToBackThread,
            CancellationToken token)
        {
            var entered = false;
            
            try
            {
                await _dbLock.WaitAsync(token);
                entered = true;
                if (sendToBackThread)
                {
                    await TaskUtilties.RunTask(() =>
                    {
                        using var db = new SQLiteConnection(_dbPath);
                        action(db).Forget(); // fire and forget since we're inside RunTask
                    }, token);
                }
                else
                {
                    using var db = new SQLiteConnection(_dbPath);
                    await action(db);
                }
            }
            finally
            {
                if(entered)
                    _dbLock.Release();
            }
        }
    }
}