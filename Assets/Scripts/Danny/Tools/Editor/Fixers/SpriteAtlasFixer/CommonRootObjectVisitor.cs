using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;

namespace SupportUtils
{
    public class CommonRootObjectVisitor : IRootObjectVisitor
    {
        public JObject VisitRoot(JObject rootObject)
        {
            JToken internalIDToNameTable = rootObject.SelectToken("$.TextureImporter.internalIDToNameTable");

            Dictionary<string, List<int>> nameIDTable = new Dictionary<string, List<int>>();

            // 1 修改SpriteName，使其唯一。
            foreach (var jToken in internalIDToNameTable)
            {
                int internalID = jToken.SelectToken(".first.213").Value<int>();
                string spriteName = jToken.SelectToken(".second").Value<string>();

                string newSpriteName = $"{spriteName}_{internalID}";

                if (!nameIDTable.ContainsKey(spriteName))
                    nameIDTable.Add(spriteName, new List<int>());
                
                nameIDTable[spriteName].Add(internalID);
                jToken["second"] = newSpriteName;
            }

            JToken spriteSheetSprites = rootObject.SelectToken("$.TextureImporter.spriteSheet.sprites");

            HashSet<int> prefixUsedInternals = new HashSet<int>();

            // 2 修改 spriteSheet.sprites ，使重名的 sprite 唯一，并使 internalID 与 internalIDToNameTable 对应。
            foreach (var spriteSheetSprite in spriteSheetSprites)
            {
                string spriteName = spriteSheetSprite.SelectToken(".name").Value<string>();

                List<int> internalIDsToSelect = nameIDTable[spriteName];

                foreach (var internalID in internalIDsToSelect)
                {
                    if (!prefixUsedInternals.Contains(internalID))
                    {
                        spriteSheetSprite["name"] = $"{spriteName}_{internalID}";
                        spriteSheetSprite["internalID"] = internalID;
                        spriteSheetSprite["spriteID"] = GUID.Generate().ToString();
                        prefixUsedInternals.Add(internalID);
                        break;
                    }
                }
            }
            
            // 3 重建 nameFileIdTable
            JToken spriteSheet = rootObject.SelectToken("$.TextureImporter.spriteSheet");

            Dictionary<string, int> nameFileIdTable = new Dictionary<string, int>();

            foreach (var nameIDsPair in nameIDTable)
            {
                foreach (var id in nameIDsPair.Value)
                {
                    nameFileIdTable.Add($"{nameIDsPair.Key}_{id}", id);
                }
            }
            
            spriteSheet["nameFileIdTable"] = JToken.Parse(JsonConvert.SerializeObject(nameFileIdTable));

            return rootObject;
        }
    }
}