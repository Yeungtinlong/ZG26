using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheGame.GM
{
    public class ReadyArea : MonoBehaviour
    {
        public List<MapGrid> Grids { get; private set; }

        private void Awake()
        {
            Grids = GetComponentsInChildren<MapGrid>().ToList();
        }
    }
}