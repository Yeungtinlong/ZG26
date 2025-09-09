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

        public CharacterState Character { get; set; }
    }
}