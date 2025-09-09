using System.Collections.Generic;

namespace MBF
{
    [XLua.CSharpCallLua]
    public delegate void AoeOnTick(AoeState aoe);

    [XLua.CSharpCallLua]
    public delegate void AoeOnCreate(AoeState aoe, List<CharacterState> charactersInRange);

    [XLua.CSharpCallLua]
    public delegate void AoeOnRemove(AoeState aoe);

    [XLua.CSharpCallLua]
    public delegate void AoeOnCharacterEnter(AoeState aoe, List<CharacterState> enterCharacters);

    [XLua.CSharpCallLua]
    public delegate void AoeOnCharacterLeave(AoeState aoe, List<CharacterState> leaveCharacters);

    [XLua.LuaCallCSharp]
    public struct AoeModel
    {
        public string id;
        public string prefab;

        public AoeOnTick onTick;
        public object[] onTickParams;
        public AoeOnCreate onCreate;
        public object[] onCreateParams;
        public AoeOnRemove onRemove;
        public object[] onRemoveParams;
        public AoeOnCharacterEnter onCharacterEnter;
        public object[] onCharacterEnterParams;
        public AoeOnCharacterLeave onCharacterLeave;
        public object[] onCharacterLeaveParams;

        public AoeModel(string id,
            string prefab,
            AoeOnTick onTick = null, object[] onTickParams = null,
            AoeOnCreate onCreate = null, object[] onCreateParams = null,
            AoeOnRemove onRemove = null, object[] onRemoveParams = null,
            AoeOnCharacterEnter onCharacterEnter = null, object[] onCharacterEnterParams = null,
            AoeOnCharacterLeave onCharacterLeave = null, object[] onCharacterLeaveParams = null)
        {
            this.id = id;
            this.prefab = prefab;
            this.onTick = onTick;
            this.onTickParams = onTickParams;
            this.onCreate = onCreate;
            this.onCreateParams = onCreateParams;
            this.onRemove = onRemove;
            this.onRemoveParams = onRemoveParams;
            this.onCharacterEnter = onCharacterEnter;
            this.onCharacterEnterParams = onCharacterEnterParams;
            this.onCharacterLeave = onCharacterLeave;
            this.onCharacterLeaveParams = onCharacterLeaveParams;
        }
    }
}