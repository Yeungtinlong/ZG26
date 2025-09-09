using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheGame
{
    public static class InputHelpers
    {
        public static bool IsPointerOverUIObject(Vector2 mousePosition)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = mousePosition };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        
        public static bool IsPointerOverUIObject(Vector2 mousePosition, out List<RaycastResult> results)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = mousePosition };
            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}