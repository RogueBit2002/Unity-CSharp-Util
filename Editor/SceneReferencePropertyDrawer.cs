using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Editor
{
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferencePropertyDrawer : PropertyDrawer
    {
        private const float widthPadding = 24f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty isDirtyProperty = property.FindPropertyRelative("isDirty");
            SerializedProperty assetProperty = property.FindPropertyRelative("asset");

            position.height = EditorGUIUtility.singleLineHeight;

            //Trick to mark property as dirty, and make it saveable
            if (isDirtyProperty.boolValue)
                isDirtyProperty.boolValue = false;

            if (!IsIncludedInBuild(assetProperty.objectReferenceValue))
            {
                GUIContent icon = EditorGUIUtility.IconContent("Warning@2X", "|This scene is not included in the final build.");

                Rect rect = new Rect(
                    position.x + position.width - EditorGUIUtility.singleLineHeight,
                    position.y,
                    EditorGUIUtility.singleLineHeight,
                    EditorGUIUtility.singleLineHeight);

                //Having an EditorGUI.indentLevel that's not zero messes with the icon transform???
                int indent = EditorGUI.indentLevel;

                EditorGUI.indentLevel = 0;
                EditorGUI.LabelField(rect,icon, GUIStyle.none);
                EditorGUI.indentLevel = indent;

                position.width -= widthPadding;
            }

            EditorGUI.PropertyField(position, assetProperty, label, false);


        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }


        private bool IsIncludedInBuild(UnityEngine.Object asset)
        {
            if (asset)
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long localId))
                    return Array.FindIndex(EditorBuildSettings.scenes, s => s.guid.ToString() == guid) != -1;
            
            return false;
        }
    }
}
