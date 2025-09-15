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
    public class TheGameGMGameLuaInterfaceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TheGame.GM.GameLuaInterface);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 20, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateBullet", _m_CreateBullet_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateAoe", _m_CreateAoe_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveAoe", _m_RemoveAoe_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateTimeline", _m_CreateTimeline_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDamage", _m_CreateDamage_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateSightEffect", _m_CreateSightEffect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PopText", _m_PopText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNearestTarget", _m_GetNearestTarget_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateCharacter", _m_CreateCharacter_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetItem", _m_GetItem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveItem", _m_RemoveItem_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnlockCharacter", _m_UnlockCharacter_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAllTargets", _m_GetAllTargets_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTargetsInRange", _m_GetTargetsInRange_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMouseWorldPosition", _m_GetMouseWorldPosition_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RandomGetCharacterOfType", _m_RandomGetCharacterOfType_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RandomGetCharacter", _m_RandomGetCharacter_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RandomGetCharactersOfType", _m_RandomGetCharactersOfType_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "MeleeFindSingleFoe", _m_MeleeFindSingleFoe_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "game", _g_get_game);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "input", _g_get_input);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "game", _s_set_game);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "input", _s_set_input);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "TheGame.GM.GameLuaInterface does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateBullet_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    MBF.BulletLauncher _bulletLauncher = (MBF.BulletLauncher)translator.GetObject(L, 1, typeof(MBF.BulletLauncher));
                    
                    TheGame.GM.GameLuaInterface.CreateBullet( _bulletLauncher );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateAoe_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    MBF.AoeLauncher _aoeLauncher = (MBF.AoeLauncher)translator.GetObject(L, 1, typeof(MBF.AoeLauncher));
                    
                    TheGame.GM.GameLuaInterface.CreateAoe( _aoeLauncher );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAoe_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<MBF.AoeState>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    MBF.AoeState _aoeState = (MBF.AoeState)translator.GetObject(L, 1, typeof(MBF.AoeState));
                    bool _immediate = LuaAPI.lua_toboolean(L, 2);
                    
                    TheGame.GM.GameLuaInterface.RemoveAoe( _aoeState, _immediate );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<MBF.AoeState>(L, 1)) 
                {
                    MBF.AoeState _aoeState = (MBF.AoeState)translator.GetObject(L, 1, typeof(MBF.AoeState));
                    
                    TheGame.GM.GameLuaInterface.RemoveAoe( _aoeState );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TheGame.GM.GameLuaInterface.RemoveAoe!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateTimeline_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    MBF.TimelineObj _timelineObj = (MBF.TimelineObj)translator.GetObject(L, 1, typeof(MBF.TimelineObj));
                    
                    TheGame.GM.GameLuaInterface.CreateTimeline( _timelineObj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDamage_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _attacker = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    UnityEngine.GameObject _defender = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    MBF.Damage _damage;translator.Get(L, 3, out _damage);
                    MBF.DamageInfoTag[] _tags = (MBF.DamageInfoTag[])translator.GetObject(L, 4, typeof(MBF.DamageInfoTag[]));
                    
                    TheGame.GM.GameLuaInterface.CreateDamage( _attacker, _defender, _damage, _tags );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateSightEffect_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _effectName = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.CreateSightEffect( _effectName, _position );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PopText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    float _scale = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Color _color;translator.Get(L, 3, out _color);
                    UnityEngine.Vector3 _position;translator.Get(L, 4, out _position);
                    
                    TheGame.GM.GameLuaInterface.PopText( _text, _scale, _color, _position );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNearestTarget_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _finder = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    int _side = LuaAPI.xlua_tointeger(L, 2);
                    bool _includeFoe = LuaAPI.lua_toboolean(L, 3);
                    bool _includeAlly = LuaAPI.lua_toboolean(L, 4);
                    float _radius = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.GetNearestTarget( _finder, _side, _includeFoe, _includeAlly, _radius );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateCharacter_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _id = LuaAPI.lua_tostring(L, 1);
                    int _side = LuaAPI.xlua_tointeger(L, 2);
                    int _grade = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.CreateCharacter( _id, _side, _grade );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetItem_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _itemId = LuaAPI.lua_tostring(L, 1);
                    int _count = LuaAPI.xlua_tointeger(L, 2);
                    
                    TheGame.GM.GameLuaInterface.GetItem( _itemId, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveItem_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _itemId = LuaAPI.lua_tostring(L, 1);
                    int _count = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.RemoveItem( _itemId, _count );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnlockCharacter_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _chaId = LuaAPI.lua_tostring(L, 1);
                    
                    TheGame.GM.GameLuaInterface.UnlockCharacter( _chaId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAllTargets_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.GameObject _finder = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));
                    int _side = LuaAPI.xlua_tointeger(L, 2);
                    bool _includeFoe = LuaAPI.lua_toboolean(L, 3);
                    bool _includeAlly = LuaAPI.lua_toboolean(L, 4);
                    float _radius = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.GetAllTargets( _finder, _side, _includeFoe, _includeAlly, _radius );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTargetsInRange_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 _center;translator.Get(L, 1, out _center);
                    int _side = LuaAPI.xlua_tointeger(L, 2);
                    bool _includeFoe = LuaAPI.lua_toboolean(L, 3);
                    bool _includeAlly = LuaAPI.lua_toboolean(L, 4);
                    float _radius = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.GetTargetsInRange( _center, _side, _includeFoe, _includeAlly, _radius );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMouseWorldPosition_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.GetMouseWorldPosition(  );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RandomGetCharacterOfType_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TheGame.GM.CharacterType _characterType;translator.Get(L, 1, out _characterType);
                    int _side = LuaAPI.xlua_tointeger(L, 2);
                    bool _includeFoe = LuaAPI.lua_toboolean(L, 3);
                    bool _includeAlly = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.RandomGetCharacterOfType( _characterType, _side, _includeFoe, _includeAlly );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RandomGetCharacter_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _side = LuaAPI.xlua_tointeger(L, 1);
                    bool _includeFoe = LuaAPI.lua_toboolean(L, 2);
                    bool _includeAlly = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.RandomGetCharacter( _side, _includeFoe, _includeAlly );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RandomGetCharactersOfType_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TheGame.GM.CharacterType _characterType;translator.Get(L, 1, out _characterType);
                    int _count = LuaAPI.xlua_tointeger(L, 2);
                    int _side = LuaAPI.xlua_tointeger(L, 3);
                    bool _includeFoe = LuaAPI.lua_toboolean(L, 4);
                    bool _includeAlly = LuaAPI.lua_toboolean(L, 5);
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.RandomGetCharactersOfType( _characterType, _count, _side, _includeFoe, _includeAlly );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MeleeFindSingleFoe_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    MBF.CharacterState _caster = (MBF.CharacterState)translator.GetObject(L, 1, typeof(MBF.CharacterState));
                    
                        var gen_ret = TheGame.GM.GameLuaInterface.MeleeFindSingleFoe( _caster );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_game(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TheGame.GM.GameLuaInterface.game);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_input(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TheGame.GM.GameLuaInterface.input);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_game(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    TheGame.GM.GameLuaInterface.game = (TheGame.GM.GameManager)translator.GetObject(L, 1, typeof(TheGame.GM.GameManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_input(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    TheGame.GM.GameLuaInterface.input = (TheGame.InputSystem.InputManager)translator.GetObject(L, 1, typeof(TheGame.InputSystem.InputManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
