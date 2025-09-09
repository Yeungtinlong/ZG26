using DG.Tweening;
using TheGame.ResourceManagement;
using UnityEngine;

namespace MBF
{
    public class UnitViewController : MonoBehaviour
    {
        private GameObject _view;
        public GameObject View => _view;
        
        private SpriteRenderer _spriteRenderer;

        private static readonly int _flashId = Shader.PropertyToID("_Flash");

        private void Awake()
        {
            _view = transform.GetChild(0).gameObject;
            _spriteRenderer = _view.GetComponent<SpriteRenderer>();
            _spriteRenderer.material = ResLoader.LoadAsset<Material>("Materials/FlashMaterial.mat");
        }

        public void ShowHitEffect()
        {
            _spriteRenderer.material.DOKill();
            _spriteRenderer.material.SetFloat(_flashId, 1f);
            _spriteRenderer.material.DOFloat(0f, _flashId, 0.25f).SetEase(Ease.InCubic);
        }
    }
}