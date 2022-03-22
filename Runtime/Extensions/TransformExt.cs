using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSUtil.Extensions
{
    public static class TransformExt
    {
		public static Quaternion TransformRotation(this Transform transform, Quaternion quaternion)
		{
			return transform.rotation * quaternion;
		}

		public static Quaternion InverseTransformRotation(this Transform transform, Quaternion rotation)
		{
			return Quaternion.Inverse(transform.rotation) * rotation;
		}
	}
}
