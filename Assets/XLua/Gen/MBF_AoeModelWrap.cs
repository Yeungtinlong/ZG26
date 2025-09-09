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
    public class MBFAoeModelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MBF.AoeModel);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 12, 12);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "id", _g_get_id);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "prefab", _g_get_prefab);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onTick", _g_get_onTick);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onTickParams", _g_get_onTickParams);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCreate", _g_get_onCreate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCreateParams", _g_get_onCreateParams);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onRemove", _g_get_onRemove);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onRemoveParams", _g_get_onRemoveParams);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCharacterEnter", _g_get_onCharacterEnter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCharacterEnterParams", _g_get_onCharacterEnterParams);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCharacterLeave", _g_get_onCharacterLeave);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCharacterLeaveParams", _g_get_onCharacterLeaveParams);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "id", _s_set_id);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "prefab", _s_set_prefab);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onTick", _s_set_onTick);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onTickParams", _s_set_onTickParams);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCreate", _s_set_onCreate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCreateParams", _s_set_onCreateParams);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onRemove", _s_set_onRemove);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onRemoveParams", _s_set_onRemoveParams);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCharacterEnter", _s_set_onCharacterEnter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCharacterEnterParams", _s_set_onCharacterEnterParams);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCharacterLeave", _s_set_onCharacterLeave);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCharacterLeaveParams", _s_set_onCharacterLeaveParams);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 13 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7) && translator.Assignable<MBF.AoeOnRemove>(L, 8) && translator.Assignable<object[]>(L, 9) && translator.Assignable<MBF.AoeOnCharacterEnter>(L, 10) && translator.Assignable<object[]>(L, 11) && translator.Assignable<MBF.AoeOnCharacterLeave>(L, 12) && translator.Assignable<object[]>(L, 13))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					MBF.AoeOnRemove _onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 8);
					object[] _onRemoveParams = (object[])translator.GetObject(L, 9, typeof(object[]));
					MBF.AoeOnCharacterEnter _onCharacterEnter = translator.GetDelegate<MBF.AoeOnCharacterEnter>(L, 10);
					object[] _onCharacterEnterParams = (object[])translator.GetObject(L, 11, typeof(object[]));
					MBF.AoeOnCharacterLeave _onCharacterLeave = translator.GetDelegate<MBF.AoeOnCharacterLeave>(L, 12);
					object[] _onCharacterLeaveParams = (object[])translator.GetObject(L, 13, typeof(object[]));
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams, _onRemove, _onRemoveParams, _onCharacterEnter, _onCharacterEnterParams, _onCharacterLeave, _onCharacterLeaveParams);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 12 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7) && translator.Assignable<MBF.AoeOnRemove>(L, 8) && translator.Assignable<object[]>(L, 9) && translator.Assignable<MBF.AoeOnCharacterEnter>(L, 10) && translator.Assignable<object[]>(L, 11) && translator.Assignable<MBF.AoeOnCharacterLeave>(L, 12))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					MBF.AoeOnRemove _onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 8);
					object[] _onRemoveParams = (object[])translator.GetObject(L, 9, typeof(object[]));
					MBF.AoeOnCharacterEnter _onCharacterEnter = translator.GetDelegate<MBF.AoeOnCharacterEnter>(L, 10);
					object[] _onCharacterEnterParams = (object[])translator.GetObject(L, 11, typeof(object[]));
					MBF.AoeOnCharacterLeave _onCharacterLeave = translator.GetDelegate<MBF.AoeOnCharacterLeave>(L, 12);
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams, _onRemove, _onRemoveParams, _onCharacterEnter, _onCharacterEnterParams, _onCharacterLeave);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 11 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7) && translator.Assignable<MBF.AoeOnRemove>(L, 8) && translator.Assignable<object[]>(L, 9) && translator.Assignable<MBF.AoeOnCharacterEnter>(L, 10) && translator.Assignable<object[]>(L, 11))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					MBF.AoeOnRemove _onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 8);
					object[] _onRemoveParams = (object[])translator.GetObject(L, 9, typeof(object[]));
					MBF.AoeOnCharacterEnter _onCharacterEnter = translator.GetDelegate<MBF.AoeOnCharacterEnter>(L, 10);
					object[] _onCharacterEnterParams = (object[])translator.GetObject(L, 11, typeof(object[]));
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams, _onRemove, _onRemoveParams, _onCharacterEnter, _onCharacterEnterParams);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 10 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7) && translator.Assignable<MBF.AoeOnRemove>(L, 8) && translator.Assignable<object[]>(L, 9) && translator.Assignable<MBF.AoeOnCharacterEnter>(L, 10))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					MBF.AoeOnRemove _onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 8);
					object[] _onRemoveParams = (object[])translator.GetObject(L, 9, typeof(object[]));
					MBF.AoeOnCharacterEnter _onCharacterEnter = translator.GetDelegate<MBF.AoeOnCharacterEnter>(L, 10);
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams, _onRemove, _onRemoveParams, _onCharacterEnter);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 9 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7) && translator.Assignable<MBF.AoeOnRemove>(L, 8) && translator.Assignable<object[]>(L, 9))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					MBF.AoeOnRemove _onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 8);
					object[] _onRemoveParams = (object[])translator.GetObject(L, 9, typeof(object[]));
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams, _onRemove, _onRemoveParams);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 8 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7) && translator.Assignable<MBF.AoeOnRemove>(L, 8))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					MBF.AoeOnRemove _onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 8);
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams, _onRemove);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 7 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6) && translator.Assignable<object[]>(L, 7))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					object[] _onCreateParams = (object[])translator.GetObject(L, 7, typeof(object[]));
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate, _onCreateParams);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 6 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5) && translator.Assignable<MBF.AoeOnCreate>(L, 6))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					MBF.AoeOnCreate _onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 6);
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams, _onCreate);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4) && translator.Assignable<object[]>(L, 5))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					object[] _onTickParams = (object[])translator.GetObject(L, 5, typeof(object[]));
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick, _onTickParams);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<MBF.AoeOnTick>(L, 4))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					MBF.AoeOnTick _onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 4);
					
					var gen_ret = new MBF.AoeModel(_id, _prefab, _onTick);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING))
				{
					string _id = LuaAPI.lua_tostring(L, 2);
					string _prefab = LuaAPI.lua_tostring(L, 3);
					
					var gen_ret = new MBF.AoeModel(_id, _prefab);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
				if (LuaAPI.lua_gettop(L) == 1)
				{
				    translator.Push(L, default(MBF.AoeModel));
			        return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MBF.AoeModel constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.id);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_prefab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.prefab);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onTick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onTick);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onTickParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onTickParams);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCreate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onCreate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCreateParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onCreateParams);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onRemove(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onRemove);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onRemoveParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onRemoveParams);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCharacterEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onCharacterEnter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCharacterEnterParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onCharacterEnterParams);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCharacterLeave(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onCharacterLeave);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCharacterLeaveParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                translator.Push(L, gen_to_be_invoked.onCharacterLeaveParams);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_id(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.id = LuaAPI.lua_tostring(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_prefab(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.prefab = LuaAPI.lua_tostring(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onTick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onTick = translator.GetDelegate<MBF.AoeOnTick>(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onTickParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onTickParams = (object[])translator.GetObject(L, 2, typeof(object[]));
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCreate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onCreate = translator.GetDelegate<MBF.AoeOnCreate>(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCreateParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onCreateParams = (object[])translator.GetObject(L, 2, typeof(object[]));
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onRemove(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onRemove = translator.GetDelegate<MBF.AoeOnRemove>(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onRemoveParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onRemoveParams = (object[])translator.GetObject(L, 2, typeof(object[]));
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCharacterEnter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onCharacterEnter = translator.GetDelegate<MBF.AoeOnCharacterEnter>(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCharacterEnterParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onCharacterEnterParams = (object[])translator.GetObject(L, 2, typeof(object[]));
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCharacterLeave(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onCharacterLeave = translator.GetDelegate<MBF.AoeOnCharacterLeave>(L, 2);
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCharacterLeaveParams(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeModel gen_to_be_invoked;translator.Get(L, 1, out gen_to_be_invoked);
                gen_to_be_invoked.onCharacterLeaveParams = (object[])translator.GetObject(L, 2, typeof(object[]));
            
                translator.Update(L, 1, gen_to_be_invoked);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
