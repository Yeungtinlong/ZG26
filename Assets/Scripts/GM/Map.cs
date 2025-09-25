using System.Linq;
using UnityEngine;

namespace TheGame.GM
{
    public class Map : MonoBehaviour
    {
        private SpriteRenderer _groundSpriteRenderer;

        private Vector2 _size;
        public Vector2 Size => _size;

        private MapGrid[,] _grids;
        public MapGrid[,] Grids => _grids;

        private ReadyArea _readyArea;
        public ReadyArea ReadyArea => _readyArea;

        public void Set()
        {
            _readyArea = GetComponentInChildren<ReadyArea>();
            _groundSpriteRenderer = transform.Find("Ground").GetComponent<SpriteRenderer>();
            Vector2 sizeSprite = _groundSpriteRenderer.sprite.rect.size / _groundSpriteRenderer.sprite.pixelsPerUnit;
            Vector2 sizeTiling = _groundSpriteRenderer.size;
            _size = Vector2.Max(sizeSprite, sizeTiling);

            var allGrids = GetComponentsInChildren<MapGrid>().Where(g => !g.IsReadyGrid).ToList();

            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (var mapGrid in allGrids)
            {
                minX = Mathf.Min(minX, Mathf.RoundToInt(mapGrid.transform.position.x));
                minY = Mathf.Min(minY, Mathf.RoundToInt(mapGrid.transform.position.y));
                maxX = Mathf.Max(maxX, Mathf.RoundToInt(mapGrid.transform.position.x));
                maxY = Mathf.Max(maxY, Mathf.RoundToInt(mapGrid.transform.position.y));
            }

            _grids = new MapGrid[maxX - minX + 1, maxY - minY + 1];
            foreach (var mapGrid in allGrids)
            {
                int x = Mathf.RoundToInt(mapGrid.transform.position.x) - minX;
                int y = Mathf.RoundToInt(mapGrid.transform.position.y) - minY;
                _grids[x, y] = mapGrid;
                _grids[x, y].GridPosition = new Vector2Int(x, y);
            }
        }
    }
}