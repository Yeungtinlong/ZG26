using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheGame.GM
{
    public class Map : MonoBehaviour
    {
        private SpriteRenderer _groundSpriteRenderer;

        private Vector2 _size;
        public Vector2 Size => _size;
        
        public List<MapGrid> Grids { get; private set; } 
        
        public void Set()
        {
            _groundSpriteRenderer = transform.Find("Ground").GetComponent<SpriteRenderer>();
            _size = _groundSpriteRenderer.sprite.rect.size / _groundSpriteRenderer.sprite.pixelsPerUnit;

            Grids = GetComponentsInChildren<MapGrid>().ToList();
        }
    }
}