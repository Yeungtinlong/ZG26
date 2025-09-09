using System;

namespace SupportUtils
{
    [Serializable]
    public struct ReferenceInfo
    {
        public string Guid;
        public string FileId;
        public string Type;

        public ReferenceInfo(string guid, string fileId, string type)
        {
            Guid = guid;
            FileId = fileId;
            Type = type;
        }
    }
}