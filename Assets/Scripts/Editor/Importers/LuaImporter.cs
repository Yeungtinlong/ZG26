using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace TheGame.Editor
{
    [ScriptedImporter(0, "lua")]
    public class LuaImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            TextAsset textAsset = new TextAsset(File.ReadAllText(ctx.assetPath));
            ctx.AddObjectToAsset("TextAsset", textAsset);
            ctx.SetMainObject(textAsset);
        }
    }
}