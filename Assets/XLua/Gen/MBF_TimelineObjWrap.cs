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
    public class MBFTimelineObjWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MBF.TimelineObj);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 5, 5);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "model", _g_get_model);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "caster", _g_get_caster);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "source", _g_get_source);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tickElapsed", _g_get_tickElapsed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "timescale", _g_get_timescale);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "model", _s_set_model);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "caster", _s_set_caster);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "source", _s_set_source);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tickElapsed", _s_set_tickElapsed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "timescale", _s_set_timescale);
            
			
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
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<MBF.TimelineModel>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && translator.Assignable<object>(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					MBF.TimelineModel _model;translator.Get(L, 2, out _model);
					UnityEngine.GameObject _caster = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					object _source = translator.GetObject(L, 4, typeof(object));
					float _timescale = (float)LuaAPI.lua_tonumber(L, 5);
					
					var gen_ret = new MBF.TimelineObj(_model, _caster, _source, _timescale);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<MBF.TimelineModel>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && translator.Assignable<object>(L, 4))
				{
					MBF.TimelineModel _model;translator.Get(L, 2, out _model);
					UnityEngine.GameObject _caster = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					object _source = translator.GetObject(L, 4, typeof(object));
					
					var gen_ret = new MBF.TimelineObj(_model, _caster, _source);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MBF.TimelineObj constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_model(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.model);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_caster(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.caster);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_source(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.source);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tickElapsed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.tickElapsed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timescale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.timescale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_model(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                MBF.TimelineModel gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.model = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_caster(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.caster = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_source(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.source = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tickElapsed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tickElapsed = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timescale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.TimelineObj gen_to_be_invoked = (MBF.TimelineObj)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.timescale = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
