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
            /*Rect childrenPosition = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight,
                position.width,
                position.height - EditorGUIUtility.singleLineHeight);*/

            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            EditorGUI.PropertyField(position, valueProperty, cachedLabel, true);
            //DrawValueChildren(childrenPosition, valueProperty);
            EditorGUI.EndDisabledGroup();

            EditorGUI.PropertyField(togglePosition, enabledProperty, GUIContent.none);
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return //property.FindPropertyRelative("value").isExpanded ?
                EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"));// - EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }

        /*
        private void DrawValueChildren(Rect position, SerializedProperty valueProperty)
        {
            EditorGUI.indentLevel++;
            float y = 0;
            foreach (SerializedProperty prop in GetChildProperties(valueProperty))
            {
                
                float height = EditorGUI.GetPropertyHeight(prop);
                Rect pos = EditorGUI.IndentedRect(new Rect(position.x, position.y + y, position.width, height));
                
                EditorGUI.PropertyField(new Rect(position.x, position.y + y, position.width, height), prop, true);
                
                y += height + 2;
            }
            EditorGUI.indentLevel--;
        }

        private IEnumerable<SerializedProperty> GetChildProperties(SerializedProperty property)
        {
            List<SerializedProperty> children = new List<SerializedProperty>();

            property = property.Copy();

            if (!property.hasVisibleChildren || !property.isExpanded)
                return children;

            string root = property.propertyPath;

            property.NextVisible(true);

            do
            {
                if (!property.propertyPath.StartsWith(root))
                    break;

                children.Add(property.Copy());
            } while (property.NextVisible(false));

            return children;
        }*/
    }
}
