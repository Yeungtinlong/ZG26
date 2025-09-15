using System.Collections.Generic;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public struct RoleDefaultEquipModel
    {
        public string chaId;
        public List<string> equipIds;
    }
}