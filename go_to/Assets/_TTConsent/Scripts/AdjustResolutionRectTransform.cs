using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace TT.Consent
{
    [Serializable]
    public struct ViewAreaOffset
    {
        public int top;
        public int bottom;
        public int left;
        public int right;

        public ViewAreaOffset(int top, int bottom, int left, int right)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }

        public static ViewAreaOffset operator -(ViewAreaOffset a, ViewAreaOffset b)
        {
            return new ViewAreaOffset(a.top - b.top, a.bottom - b.bottom, a.left - b.left, a.right - b.right);
        }
    }

    public class AdjustResolutionRectTransform : MonoBehaviour
    {
        [SerializeField]
        ViewAreaOffset NormalOffset;
        [SerializeField]
        ViewAreaOffset iPhoneXPortraitOffset;
        [SerializeField]
        ViewAreaOffset iPhoneXLandscapeOffset;

        private void Start()
        {
            ScreenOrientation currentOrientation = Screen.orientation;
            bool isPortrait = true;
            switch (currentOrientation)
            {
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    {
                        isPortrait = true;
                    }
                    break;
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    {
                        isPortrait = false;
                    }
                    break;
            }

            var rectTransform = this.GetComponent<RectTransform>();

            Vector2 offsetMax = rectTransform.offsetMax;
            Vector2 offsetMin = rectTransform.offsetMin;
            var viewAreaOffset = NormalOffset;
#if UNITY_IOS
			var safeAreaRect = Screen.safeArea;
			if((int)safeAreaRect.height != Screen.height)
			{
				if (isPortrait)
				{
					viewAreaOffset = iPhoneXPortraitOffset;
				}
				else
				{
					viewAreaOffset = iPhoneXLandscapeOffset;
				}
			}
#endif
			offsetMax.y -= viewAreaOffset.top;
            offsetMin.y += viewAreaOffset.bottom;
            rectTransform.offsetMax = offsetMax;
            rectTransform.offsetMin = offsetMin;
        }
    }

}
