#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    
    public class TutorialTestEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.TestEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.TestEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.TestEnum), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", Tutorial.TestEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", Tutorial.TestEnum.E2);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.TestEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialTestEnum(L, (Tutorial.TestEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushTutorialTestEnum(L, Tutorial.TestEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.TestEnum!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.TestEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class XLuaTestMyEnumWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(XLuaTest.MyEnum), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(XLuaTest.MyEnum), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(XLuaTest.MyEnum), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E1", XLuaTest.MyEnum.E1);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E2", XLuaTest.MyEnum.E2);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(XLuaTest.MyEnum), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushXLuaTestMyEnum(L, (XLuaTest.MyEnum)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E1"))
                {
                    translator.PushXLuaTestMyEnum(L, XLuaTest.MyEnum.E1);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E2"))
                {
                    translator.PushXLuaTestMyEnum(L, XLuaTest.MyEnum.E2);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for XLuaTest.MyEnum!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for XLuaTest.MyEnum! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class MBFDamageInfoTagWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(MBF.DamageInfoTag), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(MBF.DamageInfoTag), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(MBF.DamageInfoTag), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DirectHurt", MBF.DamageInfoTag.DirectHurt);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DirectHeal", MBF.DamageInfoTag.DirectHeal);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(MBF.DamageInfoTag), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushMBFDamageInfoTag(L, (MBF.DamageInfoTag)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "DirectHurt"))
                {
                    translator.PushMBFDamageInfoTag(L, MBF.DamageInfoTag.DirectHurt);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "DirectHeal"))
                {
                    translator.PushMBFDamageInfoTag(L, MBF.DamageInfoTag.DirectHeal);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for MBF.DamageInfoTag!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for MBF.DamageInfoTag! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class MBFEquipmentSlotWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(MBF.EquipmentSlot), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(MBF.EquipmentSlot), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(MBF.EquipmentSlot), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Weapon", MBF.EquipmentSlot.Weapon);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Helmet", MBF.EquipmentSlot.Helmet);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Armor", MBF.EquipmentSlot.Armor);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Shoe", MBF.EquipmentSlot.Shoe);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Relic", MBF.EquipmentSlot.Relic);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(MBF.EquipmentSlot), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushMBFEquipmentSlot(L, (MBF.EquipmentSlot)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Weapon"))
                {
                    translator.PushMBFEquipmentSlot(L, MBF.EquipmentSlot.Weapon);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Helmet"))
                {
                    translator.PushMBFEquipmentSlot(L, MBF.EquipmentSlot.Helmet);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Armor"))
                {
                    translator.PushMBFEquipmentSlot(L, MBF.EquipmentSlot.Armor);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Shoe"))
                {
                    translator.PushMBFEquipmentSlot(L, MBF.EquipmentSlot.Shoe);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Relic"))
                {
                    translator.PushMBFEquipmentSlot(L, MBF.EquipmentSlot.Relic);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for MBF.EquipmentSlot!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for MBF.EquipmentSlot! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TheGameItemTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(TheGame.ItemType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(TheGame.ItemType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(TheGame.ItemType), L, null, 8, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Currency", TheGame.ItemType.Currency);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Equipment", TheGame.ItemType.Equipment);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Consumable", TheGame.ItemType.Consumable);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Shard", TheGame.ItemType.Shard);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GameAsset", TheGame.ItemType.GameAsset);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Material", TheGame.ItemType.Material);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Trap", TheGame.ItemType.Trap);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(TheGame.ItemType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTheGameItemType(L, (TheGame.ItemType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Currency"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.Currency);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Equipment"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.Equipment);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Consumable"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.Consumable);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Shard"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.Shard);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "GameAsset"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.GameAsset);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Material"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.Material);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Trap"))
                {
                    translator.PushTheGameItemType(L, TheGame.ItemType.Trap);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for TheGame.ItemType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for TheGame.ItemType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TheGameGMCharacterTypeWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(TheGame.GM.CharacterType), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(TheGame.GM.CharacterType), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(TheGame.GM.CharacterType), L, null, 6, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Tank", TheGame.GM.CharacterType.Tank);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Warrior", TheGame.GM.CharacterType.Warrior);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Carry", TheGame.GM.CharacterType.Carry);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Support", TheGame.GM.CharacterType.Support);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Assassin", TheGame.GM.CharacterType.Assassin);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(TheGame.GM.CharacterType), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTheGameGMCharacterType(L, (TheGame.GM.CharacterType)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "Tank"))
                {
                    translator.PushTheGameGMCharacterType(L, TheGame.GM.CharacterType.Tank);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Warrior"))
                {
                    translator.PushTheGameGMCharacterType(L, TheGame.GM.CharacterType.Warrior);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Carry"))
                {
                    translator.PushTheGameGMCharacterType(L, TheGame.GM.CharacterType.Carry);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Support"))
                {
                    translator.PushTheGameGMCharacterType(L, TheGame.GM.CharacterType.Support);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "Assassin"))
                {
                    translator.PushTheGameGMCharacterType(L, TheGame.GM.CharacterType.Assassin);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for TheGame.GM.CharacterType!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for TheGame.GM.CharacterType! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
    public class TutorialDerivedClassTestEnumInnerWrap
    {
		public static void __Register(RealStatePtr L)
        {
		    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
		    Utils.BeginObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, 0, 0, 0, 0);
			Utils.EndObjectRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator, null, null, null, null, null);
			
			Utils.BeginClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, null, 3, 0, 0);

            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E3", Tutorial.DerivedClass.TestEnumInner.E3);
            
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "E4", Tutorial.DerivedClass.TestEnumInner.E4);
            

			Utils.RegisterFunc(L, Utils.CLS_IDX, "__CastFrom", __CastFrom);
            
            Utils.EndClassRegister(typeof(Tutorial.DerivedClass.TestEnumInner), L, translator);
        }
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CastFrom(RealStatePtr L)
		{
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			LuaTypes lua_type = LuaAPI.lua_type(L, 1);
            if (lua_type == LuaTypes.LUA_TNUMBER)
            {
                translator.PushTutorialDerivedClassTestEnumInner(L, (Tutorial.DerivedClass.TestEnumInner)LuaAPI.xlua_tointeger(L, 1));
            }
			
            else if(lua_type == LuaTypes.LUA_TSTRING)
            {

			    if (LuaAPI.xlua_is_eq_str(L, 1, "E3"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E3);
                }
				else if (LuaAPI.xlua_is_eq_str(L, 1, "E4"))
                {
                    translator.PushTutorialDerivedClassTestEnumInner(L, Tutorial.DerivedClass.TestEnumInner.E4);
                }
				else
                {
                    return LuaAPI.luaL_error(L, "invalid string for Tutorial.DerivedClass.TestEnumInner!");
                }

            }
			
            else
            {
                return LuaAPI.luaL_error(L, "invalid lua type for Tutorial.DerivedClass.TestEnumInner! Expect number or string, got + " + lua_type);
            }

            return 1;
		}
	}
    
}