namespace MBF
{
    [XLua.LuaCallCSharp]
    public struct AddEquipmentInfo
    {
        public EquipmentModel model;

        public AddEquipmentInfo(EquipmentModel model)
        {
            this.model = model;
        }
    }
}