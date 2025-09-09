// #define DANNY_SPINE_SUPPORT
#if DANNY_SPINE_SUPPORT
namespace SupportUtils
{
    public static class FixNameHelper
    {
        public static string Remove_d(string nameContains_d)
        {
            if (nameContains_d.Contains("_d"))
            {
                return nameContains_d =
                    nameContains_d.Substring(0, nameContains_d.LastIndexOf("_d"));
            }

            return nameContains_d;
        }
    }
}
#endif