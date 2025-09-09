using UnityEditor;
using UnityEngine;

namespace TheGame.Editor
{
    public class ImageImporter : AssetPostprocessor
    {
        private void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
        {
            if (assetImporter is not TextureImporter textureImporter) return;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.mipmapEnabled = false;
        }
    }
}