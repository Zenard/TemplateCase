using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Utility
{
    public static class TaskUtilties
    {
        private static CancellationTokenSource _lifetimeCts;

        public static void Initialize()
        {
            _lifetimeCts?.Cancel();
            _lifetimeCts = new CancellationTokenSource();
        }

        public static async UniTask RunTask(Action action, CancellationToken cancellationToken = default,
            bool deepLog = false)
        {
            try
            {
                var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(_lifetimeCts.Token, cancellationToken)
                    .Token;
                await UniTask.SwitchToThreadPool();
                linkedToken.ThrowIfCancellationRequested();
                action();
                await UniTask.Yield();
                linkedToken.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException e)
            {
                if (deepLog) Debug.LogError($"TaskPool cancel exception: {e} ");
            }
            catch (Exception e)
            {
                Debug.LogError($"TaskPool eception: {e}");
                throw;
            }
        }
        public static async UniTask<T> RunTask<T>(Func<T> action, CancellationToken cancellationToken = default,
            bool deepLog = false)
        {
            try
            {
                var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(_lifetimeCts.Token, cancellationToken)
                    .Token;
                await UniTask.SwitchToThreadPool();
                linkedToken.ThrowIfCancellationRequested();
                var result = action();
                await UniTask.Yield();
                return result;
            }
            catch (OperationCanceledException e)
            {
                if (deepLog) Debug.LogError($"TaskPool cancel exception: {e} ");
            }
            catch (Exception e)
            {
                Debug.LogError($"TaskPool eception: {e}");
                throw;
            }
            return default;
        }
    }
}