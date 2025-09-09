using System.IO;
using System.Text;
using TheGame.ResourceManagement;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class EncryptAssetbundle
    {
        [MenuItem("Danny/Tools/BuildAB")]
        static void BuildAB()
        {
            FileUtil.DeleteFileOrDirectory(Application.streamingAssetsPath);
            Directory.CreateDirectory(Application.streamingAssetsPath);
            var manifest = BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle,
                BuildTarget.iOS);
            foreach (var name in manifest.GetAllAssetBundles())
            {
                var uniqueSalt = Encoding.UTF8.GetBytes(name);
                var data = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, name));
                using (var myStream = new EncryptStream(Path.Combine(Application.streamingAssetsPath, $"{AssetBundleResLoader.BUNDLE_PREFIX}_" + name),
                           FileMode.Create))
                {
                    myStream.Write(data, 0, data.Length);
                }
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Danny/Tools/EncryptAB")]
        static void EncryptAB()
        {
            var baseAB = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "base"));
            using (var myStream = new EncryptStream(Path.Combine(Application.streamingAssetsPath, $"{AssetBundleResLoader.BUNDLE_PREFIX}_base"),
                       FileMode.Create))
            {
                myStream.Write(baseAB, 0, baseAB.Length);
            }

            var scenesAB = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "scenes"));
            using (var myStream = new EncryptStream(Path.Combine(Application.streamingAssetsPath, $"{AssetBundleResLoader.BUNDLE_PREFIX}_scenes"),
                       FileMode.Create))
            {
                myStream.Write(scenesAB, 0, scenesAB.Length);
            }

            AssetDatabase.Refresh();
        }
    }
}