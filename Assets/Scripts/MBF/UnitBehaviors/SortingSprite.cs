using UnityEngine;

namespace MBF.UnitBehaviors
{
    public class SortingSprite : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Transform _logicTrans;
        private int _offset;
        
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _logicTrans = transform.parent;
            _offset = Random.Range(-10, 10);
        }

        private void Update()
        {
            _spriteRenderer.sortingOrder = -(int)(_logicTrans.position.y * 1000f) + _offset;
        }
    }
}