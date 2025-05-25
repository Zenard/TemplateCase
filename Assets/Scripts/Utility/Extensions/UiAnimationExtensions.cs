using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Util.Extensions
{
    public static class UiAnimationExtensions
    {
        #region Rotate Animations

        public static void RotateFull(this RectTransform rectTransform, Action onComplete = null)
        {
            rectTransform.DORotate(new Vector3(0, 0, 360), .2f, RotateMode.FastBeyond360).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    DOTween.Kill(rectTransform);
                    onComplete?.Invoke();
                });
        }

        #endregion

        #region Move Animations

        public static void MoveTo(this RectTransform rectTransform, Vector3 position, float duration,
            Action onComplete = null, Ease ease = Ease.OutBack)
        {
            rectTransform.DOLocalMove(position, duration).SetEase(ease).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        #endregion

        #region Scale Animations

        public static void ScaleDownUp(this RectTransform rectTransform, Action onComplete = null)
        {
            var scale = rectTransform.localScale;
            rectTransform.DOScale(scale * .91f, .13f).SetEase(Ease.OutBack);
            rectTransform.DOScale(scale, .13f).SetEase(Ease.OutBack).SetDelay(.13f).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        public static void ScaleTo(this RectTransform rectTransform, Vector3 scale, float duration,
            Action onComplete = null)
        {
            rectTransform.DOScale(scale, duration).SetEase(Ease.OutBack).OnComplete(() => { onComplete?.Invoke(); });
        }

        public static void ScaleUpDown(this RectTransform rectTransform, Action onComplete = null)
        {
            var scale = rectTransform.localScale;
            rectTransform.DOScale(scale * 1.08f, .05f).SetEase(Ease.OutBack);
            rectTransform.DOScale(scale, .05f).SetEase(Ease.OutBack).SetDelay(.05f).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        public static void ScaleUp(this RectTransform rectTransform, Action onComplete = null)
        {
            var scale = rectTransform.localScale;
            rectTransform.DOScale(scale * 1.1f, .05f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        public static void ScaleDown(this RectTransform rectTransform, Action onComplete = null)
        {
            var scale = rectTransform.localScale;
            rectTransform.DOScale(scale * .9f, .05f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        public static void ScaleFlex(this RectTransform rectTransform, Vector3 scale, float duration,
            Action onComplete = null)
        {
            rectTransform.DOScale(scale, duration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        public static void ScaleNormal(this RectTransform rectTransform, Action onComplete = null)
        {
            var scale = Vector3.one;
            rectTransform.DOScale(scale, .05f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                DOTween.Kill(rectTransform);
                onComplete?.Invoke();
            });
        }

        #endregion
    }
}