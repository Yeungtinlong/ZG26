using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SupportUtils
{
    public static class RegexUtils
    {
        [Serializable]
        public struct GroupReplaceInfo
        {
            public string GroupName;
            public string NewValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="groupReplaceInfos"></param>
        /// <returns></returns>
        public static string MatchGroupReplace(
            string input,
            string pattern,
            List<GroupReplaceInfo> groupReplaceInfos)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matchCollection = regex.Matches(input);

            if (matchCollection.Count == 0)
                return input;

            int offset = 0;

            foreach (Match match in matchCollection)
            {
                foreach (Group group in match.Groups)
                {
                    GroupReplaceInfo groupReplaceInfo =
                        groupReplaceInfos.FirstOrDefault(pair => pair.GroupName == group.Name);
                    GroupReplace(group, groupReplaceInfo, ref input, ref offset);
                }
            }

            return input;
        }

        private static void GroupReplace(
            Group group,
            GroupReplaceInfo groupReplaceInfo,
            ref string input,
            ref int offset)
        {
            if (string.IsNullOrEmpty(groupReplaceInfo.NewValue))
                return;
            
            input = input
                .Remove(group.Index + offset, group.Length)
                .Insert(group.Index + offset, groupReplaceInfo.NewValue);
            offset += groupReplaceInfo.NewValue.Length - group.Length;
        }
    }
}