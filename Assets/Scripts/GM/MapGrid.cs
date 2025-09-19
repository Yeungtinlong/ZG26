using MBF;
using UnityEngine;

namespace TheGame.GM
{
    public class MapGrid : MonoBehaviour
    {
        [SerializeField] private CharacterType _type;
        public CharacterType Type => _type;

        [SerializeField] private int _side;
        public int Side => _side;

        [SerializeField] private bool _isReadyGrid;
        public bool IsReadyGrid => _isReadyGrid;

        [SerializeField] public Vector2Int _gridPosition;

        public Vector2Int GridPosition
        {
            get => _gridPosition;
            set => _gridPosition = value;
        }

        public CharacterState Character { get; set; }
    }
}