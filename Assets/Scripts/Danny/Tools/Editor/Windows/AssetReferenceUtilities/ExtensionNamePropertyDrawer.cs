using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    [Flags]
    public enum ExtensionNames
    {
        Null = 0,
        Prefab = 1,
        Asset = 2,
        Unity = 4,
        Mat = 8,
    }

    [CustomPropertyDrawer(typeof(ExtensionNameAttribute))]
    public class ExtensionNamePropertyDrawer : PropertyDrawer
    {
        private ExtensionNames selectedValue = ExtensionNames.Null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            selectedValue = selectedValue == ExtensionNames.Null ? StringToEnum(property.stringValue) : selectedValue;
            selectedValue = (ExtensionNames) EditorGUI.EnumFlagsField(position,"Extension Names:", selectedValue);
            property.stringValue = EnumToString(selectedValue);
        }

        private string EnumToString(Enum enumFlags)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ExtensionNames value in Enum.GetValues(typeof(ExtensionNames)))
            {
                if (enumFlags.HasFlag(value) && value != ExtensionNames.Null)
                {
                    sb.Append($"{value},");
                }
            }

            if (sb.Length == 0)
                return string.Empty;
            
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString().ToLower();
        }

        private ExtensionNames StringToEnum(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return ExtensionNames.Null;

            string[] splits = stringValue.Split(',');
            if (splits == null || splits.Length == 0)
                return ExtensionNames.Null;

            ExtensionNames resultValue = ExtensionNames.Null;
            foreach (var value in splits)
            {
                if (Enum.TryParse(typeof(ExtensionNames), value, true, out object rst))
                {
                    resultValue |= (ExtensionNames) rst;
                }
            }

            return resultValue;
        }
    }
}