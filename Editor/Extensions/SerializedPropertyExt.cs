using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace LaurensKruis.CSharpUtil.Editor.Extensions
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

		public static IEnumerable<SerializedProperty> GetDirectChildren(this SerializedProperty property)
        {
			List<SerializedProperty> children = new List<SerializedProperty>();
			if (!property.hasChildren)
				return children;

			string root = property.propertyPath;
			SerializedProperty iterator = property.Copy();

			iterator.NextVisible(true);
			do
			{
				if (!iterator.propertyPath.StartsWith(root))
					break;

				children.Add(iterator.Copy());
			} while (iterator.NextVisible(false));

			return children;
        }
    }
}
