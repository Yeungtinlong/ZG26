using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;

namespace SupportUtils
{
    public class UnderlinePatternRootObjectVisitor : IRootObjectVisitor
    {
        public JObject VisitRoot(JObject rootObject)
        {
            JToken internalIDToNameTable = rootObject.SelectToken("$.TextureImporter.internalIDToNameTable");

            Dictionary<string, int> nameIDTable = new Dictionary<string, int>();

            // 1 修改SpriteName，使其唯一。
            foreach (var jToken in internalIDToNameTable)
            {
                int internalID = jToken.SelectToken(".first.213").Value<int>();
                string spriteName = jToken.SelectToken(".second").Value<string>();

                if (!nameIDTable.ContainsKey(spriteName))
                {
                    nameIDTable.Add(spriteName, internalID);
                    continue;
                }

                string existingName = spriteName;
                string[] splits = existingName.Split('_');

                string numberString = splits[splits.Length - 1];
                int number = int.Parse(numberString);
                int lastIndexOfUnderline = existingName.LastIndexOf('_');
                int numberIndex = lastIndexOfUnderline + 1;
                string existingNameWithoutNumber =
                    existingName.Remove(numberIndex, existingName.Length - numberIndex);

                string newSpriteName = $"{existingNameWithoutNumber}{(++number)}";
                while (nameIDTable.ContainsKey(newSpriteName))
                {
                    newSpriteName = $"{existingNameWithoutNumber}{(++number)}";
                }

                nameIDTable.Add(newSpriteName, internalID);
                jToken["second"] = newSpriteName;
            }

            JToken spriteSheetSprites = rootObject.SelectToken("$.TextureImporter.spriteSheet.sprites");

            Dictionary<string, List<int>> prefixUsedNumbers = new Dictionary<string, List<int>>();

            // 2 修改 spriteSheet.sprites ，使重名的 sprite 唯一，并使 internalID 与 internalIDToNameTable 对应。
            foreach (var spriteSheetSprite in spriteSheetSprites)
            {
                string spriteName = spriteSheetSprite.SelectToken(".name").Value<string>();
                // string internalID = spriteSheetSprite.SelectToken("@.internalID").Value<string>();

                int underlineIndex = spriteName.LastIndexOf('_');
                string prefix = spriteName.Substring(0, underlineIndex);
                int numberIndex = underlineIndex + 1;
                int number = int.Parse(spriteName.Substring(numberIndex, spriteName.Length - numberIndex));

                if (!prefixUsedNumbers.ContainsKey(prefix))
                    prefixUsedNumbers.Add(prefix, new List<int>());

                if (!prefixUsedNumbers[prefix].Contains(number))
                {
                    prefixUsedNumbers[prefix].Add(number);
                    int newID = nameIDTable[spriteName];
                    spriteSheetSprite["internalID"] = newID;
                    continue;
                }

                // 生成新Number
                List<int> usedNumbers = prefixUsedNumbers[prefix];
                number = usedNumbers[usedNumbers.Count - 1] + 1;
                usedNumbers.Add(number);

                if (!nameIDTable.TryGetValue($"{prefix}_{number}", out int id))
                {
                    throw new Exception("spriteSheet.sprites is more than internalIDToNameTable, that's not allowed.");
                }

                spriteSheetSprite["name"] = $"{prefix}_{number}";
                spriteSheetSprite["internalID"] = id;
                spriteSheetSprite["spriteID"] = GUID.Generate().ToString();
            }

            // 3 重建 nameFileIdTable
            JToken spriteSheet = rootObject.SelectToken("$.TextureImporter.spriteSheet");
            spriteSheet["nameFileIdTable"] = JToken.Parse(JsonConvert.SerializeObject(nameIDTable));

            return rootObject;
        }
    }
}