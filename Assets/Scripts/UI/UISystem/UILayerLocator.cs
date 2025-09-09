using UnityEngine;

namespace TheGame.UI
{
    public class UILayerLocator : MonoBehaviour
    {
        [SerializeField] private UILayer _layer;

        public UILayer Layer => _layer;
    }
}