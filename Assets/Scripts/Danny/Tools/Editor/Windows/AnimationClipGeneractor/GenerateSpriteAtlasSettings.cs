using UnityEditor;

namespace SupportUtils
{
    public struct GenerateSpriteAtlasSettings
    {
        public bool IsChangeTexture2DSize;
        public string Platform;
        public int TextureSize;

        public int SpriteAtlasSize;
        public TextureImporterFormat SpriteAtlasFormat;
    }
}