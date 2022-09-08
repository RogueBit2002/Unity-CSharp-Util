using LaurensKruis.CSharpUtil.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Editor.Collections
{
    [CustomPropertyDrawer(typeof(FixedDictionary<,>), true)]
    public class FixedDictionaryPropertyDrawer : PropertyDrawer
    {
        private readonly GUIStyle headerBackground = new GUIStyle("RL Header");
        public readonly GUIStyle boxBackground = new GUIStyle("RL Background");
        private float headerHeight => headerBackground.CalcSize(GUIContent.none).y;
        private readonly float emptyHeight = EditorGUIUtility.singleLineHeight;
        public const float divider = 0.3f;

        private float GetSafePropertyHeight(SerializedProperty property) => GetSafePropertyHeight(property, EditorGUIUtility.singleLineHeight);
        private float GetSafePropertyHeight(SerializedProperty property, float fallback) => property == null ? fallback : EditorGUI.GetPropertyHeight(property);


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawHeader(position, property, label);

            position.y += headerHeight;
            position.height -= headerHeight;

            
            DrawContent(position, property);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty arrayProperty = property.FindPropertyRelative("items");

            if (arrayProperty.arraySize == 0 || !property.isExpanded)
                return headerHeight + emptyHeight;

            float height = headerHeight;

            
            for (int i = 0; i < arrayProperty.arraySize; i++)
                height += CalculateEntryHeight(arrayProperty.GetArrayElementAtIndex(i));

            return height;
        }

        private float CalculateEntryHeight(SerializedProperty property)
        {
            SerializedProperty keyProperty = property.FindPropertyRelative("key");
            SerializedProperty valueProperty = property.FindPropertyRelative("value");

            float keyHeight = GetSafePropertyHeight(keyProperty);
            float valueHeight = GetSafePropertyHeight(valueProperty);

            float v = Mathf.Max(
                EditorGUIUtility.singleLineHeight,
                Mathf.Max(
                    keyHeight,
                    valueHeight)
                );

            return v;
        }

        
        private void DrawHeader(Rect rect, SerializedProperty property ,GUIContent label)
        {
            rect.height = headerHeight;
            void DrawBackground(Rect rect)
            {
                if (Event.current.type == EventType.Repaint)
                    headerBackground.Draw(rect, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);
            }

            void DrawLabel(Rect rect)
            {
                rect.x += 14;
                rect.width -= 10;
                rect.height -= 2f;
                rect.y += 1f;

                property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label, true);
            }

            DrawBackground(rect);
            DrawLabel(rect);
        }
        private void DrawContent(Rect rect, SerializedProperty property)
        {
            SerializedProperty arrayProperty = property.FindPropertyRelative("items");
            if (Event.current.type == EventType.Repaint)
                boxBackground.Draw(rect, isHover: false, isActive: false, on: false, hasKeyboardFocus: false);

            if (!property.isExpanded)
            {
                rect.x += 4;
                rect.width -= 4;
                EditorGUI.LabelField(rect, new GUIContent($"{arrayProperty.arraySize} Item(s)"));
                return;
            }

            for (int i = 0; i < arrayProperty.arraySize; i++)
            {
                SerializedProperty prop = arrayProperty.GetArrayElementAtIndex(i);
                float height = CalculateEntryHeight(prop);

                rect.height = height;

                if (i == arrayProperty.arraySize - 1)
                    rect.height -= 0.5f;

                DrawElement(rect, prop, divider);
                rect.y += height;
            }
        }

        
        private void DrawElement(Rect rect, SerializedProperty property, float divider)
        {
            float padding = 4f;
            float error = 0.5f;

            SerializedProperty key = property.FindPropertyRelative("key");
            SerializedProperty value = property.FindPropertyRelative("value");

            EditorGUI.BeginDisabledGroup(true);

            if(key != null)
                EditorGUI.PropertyField(
                    new Rect(
                        rect.x, 
                        rect.y, 
                        rect.width * divider - padding/2f - error, 
                        GetSafePropertyHeight(key)), key, GUIContent.none);

            EditorGUI.EndDisabledGroup();

            if(value != null)
            {
                float additionalWidthOffset = value.hasVisibleChildren ? 6 : 0;
                GUIContent label = value.hasVisibleChildren ? new GUIContent("Value") : GUIContent.none;
                EditorGUI.PropertyField(
                   new Rect(
                       rect.x + rect.width * divider + padding / 2f + additionalWidthOffset,
                       rect.y,
                       rect.width - rect.width * divider - padding / 2f - error - additionalWidthOffset,
                       GetSafePropertyHeight(value)), value, label, true);
            }
               
        }
    }
}
