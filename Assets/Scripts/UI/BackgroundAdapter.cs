using UnityEngine;
using UnityEngine.UI;

namespace TheGame.CoreModule
{
    public class BackgroundAdapter : MonoBehaviour
    {
        private void Start()
        {
            Adapt();
        }

        private void Adapt()
        {
            Image image = GetComponent<Image>();
            if (image.sprite == null)
                return;

            CanvasScaler canvasScaler = GetComponentInParent<Canvas>().rootCanvas.GetComponent<CanvasScaler>();
            Vector2 screen = new Vector2(Screen.width, Screen.height);

            RectTransform rt = image.rectTransform;
            rt.anchorMax = rt.anchorMin = Vector2.one * 0.5f;
            Rect spriteRect = image.sprite.rect;

            float spriteAspect = spriteRect.width / spriteRect.height;
            float screenAspect = (float)screen.x / screen.y;
            float referenceResolutionAspect = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;

            float canvasScale = 0f;
            if (screenAspect > referenceResolutionAspect)
            {
                canvasScale = screen.y / canvasScaler.referenceResolution.y;
            }
            else
            {
                canvasScale = screen.x / canvasScaler.referenceResolution.x;
            }

            if (screenAspect > spriteAspect)
            {
                rt.sizeDelta = new Vector2(screen.x, screen.x / spriteAspect) / canvasScale;
            }
            else
            {
                rt.sizeDelta = new Vector2(screen.y * spriteAspect, screen.y) / canvasScale;
            }
        }
    }
}