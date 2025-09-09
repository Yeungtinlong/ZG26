using Newtonsoft.Json.Linq;

namespace SupportUtils
{
    public interface IRootObjectVisitor
    {
        JObject VisitRoot(JObject rootObject);
    }
}