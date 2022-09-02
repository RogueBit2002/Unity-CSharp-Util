using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>), true)]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        private const float propertyPadding = 24f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty enabledProperty = property.FindPropertyRelative("enabled");
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            GUIContent cachedLabel = new GUIContent(label);

            Rect togglePosition = new Rect(
                position.x + position.width - EditorGUI.GetPropertyHeight(enabledProperty),
                position.y,
                EditorGUI.GetPropertyHeight(enabledProperty),
                EditorGUI.GetPropertyHeight(enabledProperty));

            position.width -= propertyPadding;

            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            EditorGUI.PropertyField(position, valueProperty, cachedLabel, true);
            EditorGUI.EndDisabledGroup();

            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            enabledProperty.boolValue = EditorGUI.Toggle(togglePosition, enabledProperty.boolValue);

            EditorGUI.indentLevel = indentLevel;
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"));// - EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }
    }
}
