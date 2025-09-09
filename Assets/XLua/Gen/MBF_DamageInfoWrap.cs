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
    public class MBFDamageInfoWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(MBF.DamageInfo);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 4, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "attacker", _g_get_attacker);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "defender", _g_get_defender);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "damage", _g_get_damage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tags", _g_get_tags);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "attacker", _s_set_attacker);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "defender", _s_set_defender);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "damage", _s_set_damage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tags", _s_set_tags);
            
			
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
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<UnityEngine.GameObject>(L, 2) && translator.Assignable<UnityEngine.GameObject>(L, 3) && translator.Assignable<MBF.Damage>(L, 4) && translator.Assignable<MBF.DamageInfoTag[]>(L, 5))
				{
					UnityEngine.GameObject _attacker = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
					UnityEngine.GameObject _defender = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					MBF.Damage _damage;translator.Get(L, 4, out _damage);
					MBF.DamageInfoTag[] _tags = (MBF.DamageInfoTag[])translator.GetObject(L, 5, typeof(MBF.DamageInfoTag[]));
					
					var gen_ret = new MBF.DamageInfo(_attacker, _defender, _damage, _tags);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to MBF.DamageInfo constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_attacker(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.attacker);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defender(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.defender);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_damage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.damage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.tags);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_attacker(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.attacker = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defender(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.defender = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_damage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                MBF.Damage gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.damage = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tags(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                MBF.DamageInfo gen_to_be_invoked = (MBF.DamageInfo)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tags = (MBF.DamageInfoTag[])translator.GetObject(L, 2, typeof(MBF.DamageInfoTag[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
