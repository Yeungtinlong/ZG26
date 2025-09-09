using System;

namespace SupportUtils
{
    [Serializable]
    public struct ReferenceResult
    {
        public string FromPath;
        public Object Object;
        public string Guid;
    }
}