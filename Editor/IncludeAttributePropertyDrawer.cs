using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

namespace LaurensKruis.CSharpUtil.Editor
{
    [CustomPropertyDrawer(typeof(IncludeAttribute))]
    public class IncludeAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type != "Enum")
                return;

            IncludeAttribute attr = attribute as IncludeAttribute;

            object[] values = Enum.GetValues(attr.type).Cast<object>().ToArray();
            string[] allOptions = Enum.GetValues(attr.type).Cast<object>().Select(o => o.ToString()).ToArray();
            string[] options = Enum.GetValues(attr.type).Cast<object>().Where(x => attr.values.Contains(x)).Select(o => o.ToString()).ToArray();

            if(options.Length == 0)
            {
                EditorGUI.LabelField(position, "No selection available");
                return;
            }

            int index = options.ToList().IndexOf(allOptions[property.enumValueIndex]);

            if (index == -1)
                index = 0;

            
            index = EditorGUI.Popup(position, label, index, options.Select(o => new GUIContent(o)).ToArray());
            property.enumValueIndex = allOptions.ToList().IndexOf(options[index]);
        }
    }
}
