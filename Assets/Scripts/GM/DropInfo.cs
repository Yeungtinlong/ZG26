using UnityEngine;

namespace TheGame.GM
{
    public struct DropInfo
    {
        public ItemStack drop;
        public Vector3 worldPos;

        public DropInfo(ItemStack drop, Vector3 worldPos)
        {
            this.drop = drop;
            this.worldPos = worldPos;
        }
    }
}