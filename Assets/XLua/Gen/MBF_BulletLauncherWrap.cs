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
    public class MBFBulletLauncherWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MBF.BulletLauncher);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 15, 15);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "model", _g_get_model);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "caster", _g_get_caster);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "side", _g_get_side);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "launchPos", _g_get_launchPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetPos", _g_get_targetPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "launchDir", _g_get_launchDir);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "radius", _g_get_radius);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "speed", _g_get_speed);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hp", _g_get_hp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "justHitTarget", _g_get_justHitTarget);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targeting", _g_get_targeting);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tween", _g_get_tween);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "propWhileCast", _g_get_propWhileCast);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "parameters", _g_get_parameters);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "model", _s_set_model);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "caster", _s_set_caster);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "side", _s_set_side);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "launchPos", _s_set_launchPos);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetPos", _s_set_targetPos);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "launchDir", _s_set_launchDir);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "radius", _s_set_radius);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "speed", _s_set_speed);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "duration", _s_set_duration);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hp", _s_set_hp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "justHitTarget", _s_set_justHitTarget);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targeting", _s_set_targeting);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tween", _s_set_tween);
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
				if(LuaAPI.lua_gettop(L) == 16 && translator.Assignable<MBF.BulletModel>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.Vector3>(L, 5) && translator.Assignable<UnityEngine.Vector3>(L, 6) && translator.Assignable<UnityEngine.Vector3>(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 12) && translator.Assignable<MBF.BulletTargeting>(L, 13) && translator.Assignable<MBF.BulletTween>(L, 14) && translator.Assignable<MBF.ChaProp>(L, 15) && translator.Assignable<System.Collections.Generic.Dictionary<string, object>>(L, 16))
				{
					MBF.BulletModel _model;translator.Get(L, 2, out _model);
					UnityEngine.GameObject _caster = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					int _side = LuaAPI.xlua_tointeger(L, 4);
					UnityEngine.Vector3 _launchPos;translator.Get(L, 5, out _launchPos);
					UnityEngine.Vector3 _targetPos;translator.Get(L, 6, out _targetPos);
					UnityEngine.Vector3 _launchDir;translator.Get(L, 7, out _launchDir);
					float _radius = (float)LuaAPI.lua_tonumber(L, 8);
					float _speed = (float)LuaAPI.lua_tonumber(L, 9);
					int _duration = LuaAPI.xlua_tointeger(L, 10);
					int _hp = LuaAPI.xlua_tointeger(L, 11);
					bool _justHitTarget = LuaAPI.lua_toboolean(L, 12);
					MBF.BulletTargeting _targeting = translator.GetDelegate<MBF.BulletTargeting>(L, 13);
					MBF.BulletTween _tween = translator.GetDelegate<MBF.BulletTween>(L, 14);
					MBF.ChaProp _propWhileCast;translator.Get(L, 15, out _propWhileCast);
					System.Collections.Generic.Dictionary<string, object> _parameters = (System.Collections.Generic.Dictionary<string, object>)translator.GetObject(L, 16, typeof(System.Collections.Generic.Dictionary<string, object>));
					
					var gen_ret = new MBF.BulletLauncher(_model, _caster, _side, _launchPos, _targetPos, _launchDir, _radius, _speed, _duration, _hp, _justHitTarget, _targeting, _tween, _propWhileCast, _parameters);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 15 && translator.Assignable<MBF.BulletModel>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<UnityEngine.Vector3>(L, 5) && translator.Assignable<UnityEngine.Vector3>(L, 6) && translator.Assignable<UnityEngine.Vector3>(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 10) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 11) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 12) && translator.Assignable<MBF.BulletTargeting>(L, 13) && translator.Assignable<MBF.BulletTween>(L, 14) && translator.Assignable<MBF.ChaProp>(L, 15))
				{
					MBF.BulletModel _model;translator.Get(L, 2, out _model);
					UnityEngine.GameObject _caster = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					int _side = LuaAPI.xlua_tointeger(L, 4);
					UnityEngine.Vector3 _launchPos;translator.Get(L, 5, out _launchPos);
					UnityEngine.Vector3 _targetPos;translator.Get(L, 6, out _targetPos);
					UnityEngine.Vector3 _launchDir;translator.Get(L, 7, out _launchDir);
					float _radius = (float)LuaAPI.lua_tonumber(L, 8);
					float _speed = (float)LuaAPI.lua_tonumber(L, 9);
					int _duration = LuaAPI.xlua_tointeger(L, 10);
					int _hp = LuaAPI.xlua_tointeger(L, 11);
					bool _justHitTarget = LuaAPI.lua_toboolean(L, 12);
					MBF.BulletTargeting _targeting = translator.GetDelegate<MBF.BulletTargeting>(L, 13);
					MBF.BulletTween _tween = translator.GetDelegate<MBF.BulletTween>(L, 14);
					MBF.ChaProp _propWhileCast;translator.Get(L, 15, out _propWhileCast);
					
					var gen_ret = new MBF.BulletLauncher(_model, _caster, _side, _launchPos, _targetPos, _launchDir, _radius, _speed, _duration, _hp, _justHitTarget, _targeting, _tween, _propWhileCast);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MBF.BulletLauncher constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_model(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.side);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_launchPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.launchPos);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.targetPos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_launchDir(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, gen_to_be_invoked.launchDir);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.radius);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.speed);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.duration);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.hp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_justHitTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.justHitTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targeting(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.targeting);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tween(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.tween);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                MBF.BulletModel gen_value;translator.Get(L, 2, out gen_value);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.side = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_launchPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.launchPos = gen_value;
            
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.targetPos = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_launchDir(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.launchDir = gen_value;
            
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.radius = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_speed(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.speed = (float)LuaAPI.lua_tonumber(L, 2);
            
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.duration = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hp = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_justHitTarget(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.justHitTarget = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targeting(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targeting = translator.GetDelegate<MBF.BulletTargeting>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tween(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tween = translator.GetDelegate<MBF.BulletTween>(L, 2);
            
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
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
			
                MBF.BulletLauncher gen_to_be_invoked = (MBF.BulletLauncher)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.parameters = (System.Collections.Generic.Dictionary<string, object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, object>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
