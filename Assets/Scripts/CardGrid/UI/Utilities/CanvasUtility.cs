using System;
using System.Collections;
using UnityEngine;

namespace CardGrid.UI.Utilities
{
    public static class CanvasUtility
    {
        public static IEnumerator ManageCanvasVisibility(this CanvasGroup canvas, bool show)
        {
            var alpha = show ? 1f : 0f;
            while (Math.Abs(canvas.alpha - alpha) > 0.001f)
            {
                canvas.alpha = Mathf.Lerp(canvas.alpha, alpha, 5f * Time.deltaTime);
                yield return null;
            }
        }
    }
}