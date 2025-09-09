using TheGame.ResourceManagement;
using UnityEngine;
using XLua;

namespace TheGame.Common
{
    public static class LuaManager
    {
        private static LuaEnv _luaEnv;
        public static LuaEnv LuaEnv => _luaEnv;

        public static void Init()
        {
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader((ref string scriptPath) =>
            {
                var data = ResLoader.LoadAsset<TextAsset>($"LuaScripts/{scriptPath}.lua")?.bytes;
                // if (data == null)
                //     Debug.Log($"[ResLoader] Loader1 {scriptPath} failed.");
                return data;
            });
            _luaEnv.AddLoader((ref string scriptPath) =>
            {
                var data = ResLoader.LoadAsset<TextAsset>($"{scriptPath}.lua")?.bytes;
                // if (data == null)
                //     Debug.Log($"[ResLoader] Loader2 {scriptPath} failed.");
                return data;
            });
            _luaEnv.AddLoader((ref string scriptPath) =>
            {
                string path = $"{scriptPath.Replace('.', '/')}.lua";
                var data = Resources.Load<TextAsset>($"{path}")?.bytes;
                // if (data == null)
                //     Debug.Log($"[ResLoader] Loader3 {scriptPath} -> {path} failed."); 
                return data;
            });
            _luaEnv.DoString(
                "_G.Game = require(\"Game\")"
            );
        }
    }
}