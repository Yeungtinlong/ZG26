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
    public class MBFAoeLauncherWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MBF.AoeLauncher);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 9, 9);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "model", _g_get_model);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "caster", _g_get_caster);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "side", _g_get_side);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetPos", _g_get_targetPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tickTime", _g_get_tickTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "radius", _g_get_radius);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "propWhileCast", _g_get_propWhileCast);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "parameters", _g_get_parameters);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "model", _s_set_model);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "caster", _s_set_caster);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "side", _s_set_side);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetPos", _s_set_targetPos);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "duration", _s_set_duration);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tickTime", _s_set_tickTime);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "radius", _s_set_radius);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "propWhileCast", _s_set_propWhileCast);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "parameters", _s_set_parameters);
            
			
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
				if(LuaAPI.lua_gettop(L) == 10 && translator.Assignable<MBF.AoeModel>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.Vector3>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && translator.Assignable<MBF.ChaProp>(L, 9) && translator.Assignable<System.Collections.Generic.Dictionary<string, object>>(L, 10))
				{
					MBF.AoeModel _model;translator.Get(L, 2, out _model);
					UnityEngine.GameObject _caster = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					int _side = LuaAPI.xlua_tointeger(L, 4);
					UnityEngine.Vector3 _targetPos;translator.Get(L, 5, out _targetPos);
					int _duration = LuaAPI.xlua_tointeger(L, 6);
					int _tickTime = LuaAPI.xlua_tointeger(L, 7);
					float _radius = (float)LuaAPI.lua_tonumber(L, 8);
					MBF.ChaProp _propWhileCast;translator.Get(L, 9, out _propWhileCast);
					System.Collections.Generic.Dictionary<string, object> _parameters = (System.Collections.Generic.Dictionary<string, object>)translator.GetObject(L, 10, typeof(System.Collections.Generic.Dictionary<string, object>));
					
					var gen_ret = new MBF.AoeLauncher(_model, _caster, _side, _targetPos, _duration, _tickTime, _radius, _propWhileCast, _parameters);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 9 && translator.Assignable<MBF.AoeModel>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.Vector3>(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && translator.Assignable<MBF.ChaProp>(L, 9))
				{
					MBF.AoeModel _model;translator.Get(L, 2, out _model);
					UnityEngine.GameObject _caster = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					int _side = LuaAPI.xlua_tointeger(L, 4);
					UnityEngine.Vector3 _targetPos;translator.Get(L, 5, out _targetPos);
					int _duration = LuaAPI.xlua_tointeger(L, 6);
					int _tickTime = LuaAPI.xlua_tointeger(L, 7);
					float _radius = (float)LuaAPI.lua_tonumber(L, 8);
					MBF.ChaProp _propWhileCast;translator.Get(L, 9, out _propWhileCast);
					
					var gen_ret = new MBF.AoeLauncher(_model, _caster, _side, _targetPos, _duration, _tickTime, _radius, _propWhileCast);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MBF.AoeLauncher constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_model(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.caster);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_side(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.side);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.targetPos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.duration);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tickTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.tickTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_radius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.radius);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_propWhileCast(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.propWhileCast);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_parameters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.parameters);
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
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                MBF.AoeModel gen_value;translator.Get(L, 2, out gen_value);
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
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.caster = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_side(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.side = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.targetPos = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.duration = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tickTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tickTime = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_radius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.radius = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_propWhileCast(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                MBF.ChaProp gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.propWhileCast = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_parameters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.AoeLauncher gen_to_be_invoked = (MBF.AoeLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.parameters = (System.Collections.Generic.Dictionary<string, object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, object>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
