using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SupportUtils;
using SupportUtils;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class AssetReferenceHelper
    {
        private const string GUID_PATTERN =
            @"(?<={fileID: )(?<fileID>-?\d*)(?:, guid: )(?<guid>[0-9a-zA-Z]*)(?:, type: )(?<type>[0123])(?=})";

        public List<ReferenceResult> SearchReferences(string assetPath, string targetTypeName,
            List<Object> guidProviders)
        {
            ReferenceInfo origin = default;
            var result = new List<ReferenceResult>();
            foreach (var guidProvider in guidProviders)
            {
                result.AddRange(SearchFileIdAndGuidReference(assetPath, ref origin, targetTypeName, guidProvider));
            }

            return result;
        }

        public List<ReferenceResult> SearchFileIdAndGuidReference(string assetPath,
            ref ReferenceInfo originRef, string targetFileTypeName, Object guidProvider)
        {
            var result = new List<ReferenceResult>();

            assetPath = AssetDatabaseUtils.AssetPathToAbsolutePath(assetPath);

            if (string.IsNullOrEmpty(originRef.Guid) && guidProvider == null)
            {
                Debug.LogError($"GUID cannot be null.");
                return result;
            }

            if (guidProvider != null)
            {
                originRef.Guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(guidProvider));
            }

            if (string.IsNullOrEmpty(targetFileTypeName))
            {
                Debug.LogError($"Extension name cannot be null.");
                return result;
            }

            string[] targetTypeNamesSplit = targetFileTypeName.Split(',');

            targetTypeNamesSplit.ForEach(Debug.Log);

            string pattern = GUID_PATTERN;
            if (!string.IsNullOrEmpty(originRef.FileId))
                pattern = pattern.Replace(@"(?<fileID>-?\d*)", $"(?<fileID>{originRef.FileId})");
            if (!string.IsNullOrEmpty(originRef.Guid))
                pattern = pattern.Replace(@"(?<guid>[0-9a-zA-Z]*)", $"(?<guid>{originRef.Guid})");
            if (!string.IsNullOrEmpty(originRef.Type))
                pattern = pattern.Replace(@"(?<type>[123]{1})", $"(?<type>{originRef.Type})");

            Regex lineMatcher = new Regex(pattern);

            AssetDatabaseUtils.OperateAllFilesFromDirectory(assetPath, true, fileInfo =>
            {
                string[] fileNameSplits = fileInfo.FullName.Split('.');
                string typeName = fileNameSplits[fileNameSplits.Length - 1];

                if (!targetTypeNamesSplit.Contains(typeName))
                    return;

                string content = File.ReadAllText(fileInfo.FullName);
                Match match = lineMatcher.Match(content);

                // Debug.Log($"Found extension match file {fileInfo.FullName}");

                if (match.Success)
                {
                    int idx = fileInfo.FullName.IndexOf("Assets");
                    string assetRelativePath = fileInfo.FullName.Substring(idx, fileInfo.FullName.Length - idx);

                    Object objectToLoad = AssetDatabase.LoadAssetAtPath<Object>(assetRelativePath);
                    if (objectToLoad == null)
                    {
                        Debug.LogWarning($"{assetRelativePath} is exists, but load Object failed.");
                    }

                    result.Add(new ReferenceResult
                    {
                        FromPath = AssetDatabase.GetAssetPath(guidProvider),
                        Object = objectToLoad,
                        Guid = AssetDatabase.AssetPathToGUID(assetRelativePath),
                    });

                    Debug.Log(
                        $"Found file {fileInfo.Name}, ---- fileID: {match.Groups[1].Value} ---- guid: {match.Groups[2].Value} ---- type: {match.Groups[3].Value} ----");
                }
            });

            return result;
        }

        public void AlternateScriptLocation(string assetPath,
            ref ReferenceInfo originRef, ref ReferenceInfo targetRef,
            List<ReferenceResult> result)
        {
            foreach (var searchResult in result)
            {
                assetPath =
                    AssetDatabaseUtils.AssetPathToAbsolutePath(AssetDatabase.GUIDToAssetPath(searchResult.Guid));

                string content = File.ReadAllText(assetPath);

                string pattern = GUID_PATTERN;

                if (!string.IsNullOrEmpty(originRef.FileId))
                    pattern = pattern.Replace(@"(?<fileID>-?\d*)", $"(?<fileID>{originRef.FileId})");
                if (!string.IsNullOrEmpty(originRef.Guid))
                    pattern = pattern.Replace(@"(?<guid>[0-9a-zA-Z]*)", $"(?<guid>{originRef.Guid})");
                if (!string.IsNullOrEmpty(originRef.Type))
                    pattern = pattern.Replace(@"(?<type>[123]{1})", $"(?<type>{originRef.Type})");

                List<RegexUtils.GroupReplaceInfo> groupReplaceInfos = new List<RegexUtils.GroupReplaceInfo>();
                if (!string.IsNullOrEmpty(targetRef.FileId))
                    groupReplaceInfos.Add(new RegexUtils.GroupReplaceInfo
                        { GroupName = "fileID", NewValue = targetRef.FileId });
                if (!string.IsNullOrEmpty(targetRef.Guid))
                {
                    groupReplaceInfos.Add(
                        new RegexUtils.GroupReplaceInfo
                        {
                            GroupName = "guid",
                            NewValue = targetRef.Guid
                        }
                    );
                }

                if (!string.IsNullOrEmpty(targetRef.Type))
                    groupReplaceInfos.Add(new RegexUtils.GroupReplaceInfo
                        { GroupName = "type", NewValue = targetRef.Type });

                // string newContent = RegexUtils.MatchGroupReplace(matchCollection, groupReplaceInfos, content);
                string newContent = RegexUtils.MatchGroupReplace(content, pattern, groupReplaceInfos);

                File.WriteAllText(assetPath, newContent);
            }

            AssetDatabase.Refresh();
        }
    }
}