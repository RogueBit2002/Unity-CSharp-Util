using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LaurensKruis.CSharpUtil.Editor
{
	[CustomPropertyDrawer(typeof(LayerAttribute))]
	class LayerAttributePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			property.intValue = EditorGUI.LayerField(position, label, property.intValue);
		}
	}
}
