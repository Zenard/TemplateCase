using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Util.Extensions
{
    public static class UIExtensions
    {
        public static Vector3 ClampToWorld(RectTransform rectTransform, Vector3 worldPosition, Canvas canvas)
        {
            try
            {
                if (Camera.main != null)
                {
                    var viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
                    Debug.Log($"ViewportPosition: {viewportPosition}");
                    var clampedViewportPosition = ClampToScreen(rectTransform, viewportPosition, canvas);

                    var clampedWorldPosition = Camera.main.ViewportToWorldPoint(clampedViewportPosition);
                    clampedWorldPosition.z = worldPosition.z;
                    return clampedWorldPosition;
                }

                Debug.LogError("Camera.main is null");
                return worldPosition;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in ClampToWorld: {e.GetType()} {e.Message} {e.StackTrace}");
                return worldPosition;
            }
        }


        public static Vector3 ClampToScreen(RectTransform rectTransform, Vector3 position, Canvas canvas)
        {
            var canvasRect = canvas.GetComponent<RectTransform>();
            var canvasSize = canvasRect.rect.size;

            var rect = rectTransform.rect;
            var halfRectWidth = rect.width / 2f;
            var halfRectHeight = rect.height / 2f;

            var minViewport = new Vector2(halfRectWidth / canvasSize.x, halfRectHeight / canvasSize.y);
            var maxViewport = new Vector2(1f - halfRectWidth / canvasSize.x, 1f - halfRectHeight / canvasSize.y);

            var clampedViewportX = Mathf.Clamp(position.x, minViewport.x, maxViewport.x);
            var clampedViewportY = Mathf.Clamp(position.y, minViewport.y, maxViewport.y);

            return new Vector3(clampedViewportX, clampedViewportY, position.z);
        }

        public static void EnsureRectTransformInScreenBounds(RectTransform rectTransform, Canvas canvas)
        {
            // Get the screen dimensions
            Rect canvasSize = canvas.GetComponent<RectTransform>().rect;
            Vector2 screenSize = new Vector2(canvasSize.width, canvasSize.height);

            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i] = Camera.main.WorldToScreenPoint(corners[i]);
            }
            

            // Calculate the offset needed to keep the RectTransform within screen bounds
            Vector3 position = rectTransform.position;

            float offsetX = 0f;
            float offsetY = 0f;

            float extraOffset = 40;

            if (corners[0].x < 0)
            {
                offsetX = -corners[0].x;
                offsetX += extraOffset;
            }
            else if (corners[2].x > screenSize.x)
            {
                offsetX = screenSize.x - corners[2].x;
                offsetX -= extraOffset;
            }

            if (corners[0].y < 0)
                offsetY = -corners[0].y;
            else if (corners[2].y > screenSize.y)
                offsetY = screenSize.y - corners[2].y;

            // Apply the offset to keep the RectTransform inside the screen
            
            
            
            Vector2 anchoredPositionWithOffset = rectTransform.anchoredPosition + 
                                                 new Vector2(offsetX, offsetY);

            rectTransform.anchoredPosition = anchoredPositionWithOffset;
        }
        public static Vector2 GetScreenPointWithSafeArea(Camera camera, RectTransform parentCanvas, Vector3 pos)
        {
            Vector2 viewportPosition = camera.WorldToViewportPoint(pos);

            var sizeDelta = parentCanvas.sizeDelta;
            var safeAreaRect = (RectTransform)parentCanvas.GetChild(0).transform;
            var safeArea = safeAreaRect.rect;
            var rect = parentCanvas.rect;
            var offset = new Vector2((rect.width - safeArea.width) / 2, (rect.height - safeArea.height) / 2);

            var worldObjectScreenPosition = new Vector2(
                viewportPosition.x * sizeDelta.x - sizeDelta.x * 0.5f,
                viewportPosition.y * sizeDelta.y - sizeDelta.y * 0.5f);

            worldObjectScreenPosition += offset;
            return worldObjectScreenPosition;
        }

        public static TweenerCore<Color, Color, ColorOptions> DOFade(this TextMesh target, float endValue,
            float duration)
        {
            var color = target.color;
            var targetColor = new Color(color.r, color.g, color.b, endValue);
            var tween = DOTween.To(() => color, x => color = x, targetColor, duration)
                .OnUpdate(() => target.color = color);
            ;
            return tween;
        }
        
        public static void ToRectTransform(float t, RectTransform targetNumbers, RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetNumbers.anchoredPosition, t);
            rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, targetNumbers.sizeDelta, t);
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetNumbers.localScale, t);
            rectTransform.localRotation = Quaternion.Lerp(rectTransform.localRotation, targetNumbers.localRotation, t);
            rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, targetNumbers.anchorMin, t);
            rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, targetNumbers.anchorMax, t);
            rectTransform.pivot = Vector2.Lerp(rectTransform.pivot, targetNumbers.pivot, t);
        }
    }
}