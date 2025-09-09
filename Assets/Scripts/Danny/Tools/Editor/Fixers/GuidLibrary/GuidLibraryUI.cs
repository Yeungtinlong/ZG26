using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class PluginItem
    {
        public string TypeName;
        public string Guid;
    }

    public class Plugin
    {
        public string PluginName;
        public Dictionary<string, PluginItem> Items = new Dictionary<string, PluginItem>();
    }

    public class Library
    {
        public List<Plugin> Plugins = new List<Plugin>();
    }

    public class LibraryScriptableObject : SerializedScriptableObject
    {
        public List<Plugin> Plugins = new List<Plugin>();
    }

    public class GuidLibraryUI : OdinEditorWindow
    {
        [MenuItem("Danny/Tools/Guid Library")]
        static void OpenWindow()
        {
            GetWindow<GuidLibraryUI>().Show();
        }

        [SerializeField] private TextAsset _oldFile;

        [Button("Convert to Json")]
        void ConvertToJson()
        {
            string oldText = _oldFile.text;

            using StringReader stringReader = new StringReader(oldText);

            Library library = new Library();
            Plugin plugin = null;
            string line = stringReader.ReadLine();
            ;

            while (line != null)
            {
                if (line.Contains("<<<<"))
                {
                    plugin = new Plugin();
                    library.Plugins.Add(plugin);
                    plugin.PluginName = line.Split(' ')[1];
                }

                if (plugin != null && line.Contains(':'))
                {
                    string[] splits = line.Split(':');
                    plugin.Items.Add(splits[0], new PluginItem { Guid = splits[1].TrimStart() });
                }

                line = stringReader.ReadLine();
            }

            string json = JsonConvert.SerializeObject(library);
            string path =
                $"{Path.GetDirectoryName(Path.GetFullPath(AssetDatabase.GetAssetPath(_oldFile)))}/library.json";

            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
        }

        [SerializeField] private TextAsset _jsonFile;

        [Button("Convert to ScriptableObject")]
        void ConvertToScriptableObject()
        {
            Library library = JsonConvert.DeserializeObject<Library>(_jsonFile.text);
            LibraryScriptableObject libraryScriptableObject = CreateInstance<LibraryScriptableObject>();
            libraryScriptableObject.Plugins = library.Plugins;

            string path =
                $"{Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(_jsonFile)), "LibrarySO.asset")}";
            AssetDatabase.CreateAsset(libraryScriptableObject, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}