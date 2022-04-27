using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace LaurensKruis.CSUtil.Editor.Extensions
{
    public static class SerializedPropertyExt
    {
		public static IEnumerable<SerializedProperty> GetArrayEnumerable(this SerializedProperty property)
		{
			if (!property.isArray)
				throw new System.InvalidOperationException("Serialized property is not an array");

			for(int i = 0; i < property.arraySize; i ++)
			{
				yield return property.GetArrayElementAtIndex(i);
			}
		}
    }
}
