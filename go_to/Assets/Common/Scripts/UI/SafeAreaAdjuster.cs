using System;
using UnityEngine;


public class SafeAreaAdjuster : MonoBehaviour
{
    private void Start()
    {
        var rectTransform = this.GetComponent<RectTransform>();

        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 offsetMin = rectTransform.offsetMin;

        var safeAreaRect = Screen.safeArea;
        var topOffset = (int)safeAreaRect.y;
        var bottomOffset = Screen.height - (int)(safeAreaRect.y + safeAreaRect.height);
        var leftOffset = (int)safeAreaRect.x;
        var rightOffset = Screen.width - (int)(safeAreaRect.x + safeAreaRect.width);

        offsetMax.x -= rightOffset;
        offsetMax.y -= topOffset;
        offsetMin.x += leftOffset;
        offsetMin.y += bottomOffset;
        rectTransform.offsetMax = offsetMax;
        rectTransform.offsetMin = offsetMin;
    }
}
