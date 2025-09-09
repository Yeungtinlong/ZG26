using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SupportUtils
{
    public static class RegexExtensions
    {
        public static string ReplaceGroupByName(this Match match, string groupName, string replaceText)
        {
            string matchValue = match.Value;
            var group = match.Groups[groupName];
            return matchValue.Remove(group.Index - match.Index, group.Length)
                .Insert(group.Index - match.Index, replaceText);
        }

        public static string ReplaceGroupByNameOrigin(this Match match, string originInput, string groupName,
            string replaceText)
        {
            var group = match.Groups[groupName];
            if (!group.Success)
            {
                Debug.LogError($"Group Name: {groupName} hasn't matched.");
                return null;
            }
            return originInput.Remove(group.Index, group.Length).Insert(group.Index, replaceText);
        }

        /// <summary>
        /// Replace text of named groups.
        /// </summary>
        /// <param name="regex">Match pattern</param>
        /// <param name="input">Origin input content</param>
        /// <param name="replacementByGroup">Pass a key-value list. Item1: group name; Item2: value to replace.</param>
        /// <returns></returns>
        public static string ReplaceByGroups(this Regex regex, string input, List<(string, string)> replacementByGroup)
        {
            Match match = regex.Match(input);
            if (replacementByGroup.Count != match.Groups.Count)
            {
                Debug.LogError($"Matched {match.Groups.Count}, is not equal to param group {replacementByGroup.Count}.");
                return null;
            }
            foreach (var groupTuple in replacementByGroup)
            {
                input = match.ReplaceGroupByNameOrigin(input, groupTuple.Item1, groupTuple.Item2);
                if (input == null) return null;
            }
            return input;
        }
    }
}