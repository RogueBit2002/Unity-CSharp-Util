using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Extensions
{
    public static class ConfigurableJointExt
	{

        public static ConfigurableJoint SetJointDrives(this ConfigurableJoint joint, 
			JointDrive? xDrive, JointDrive? yDrive, JointDrive? zDrive,
			JointDrive? angularXDrive, JointDrive? angularYZDrive, JointDrive? slerpDrive)
		{
			if (xDrive.HasValue) joint.xDrive = xDrive.Value;
			if (yDrive.HasValue) joint.yDrive = yDrive.Value;
			if (zDrive.HasValue) joint.zDrive = zDrive.Value;
			if (angularXDrive.HasValue) joint.angularXDrive = angularXDrive.Value;
			if (angularYZDrive.HasValue) joint.angularYZDrive = angularYZDrive.Value;
			if (slerpDrive.HasValue) joint.slerpDrive = slerpDrive.Value;

			return joint;
		}


		public static ConfigurableJoint SetMotionConstraints(this ConfigurableJoint joint,
			ConfigurableJointMotion? xMotion, ConfigurableJointMotion? yMotion, ConfigurableJointMotion? zMotion,
			ConfigurableJointMotion? angularXMotion, ConfigurableJointMotion? angularYMotion, ConfigurableJointMotion? angularZMotion)
		{
			if (xMotion.HasValue) joint.xMotion = xMotion.Value;
			if (yMotion.HasValue) joint.yMotion = yMotion.Value;
			if (zMotion.HasValue) joint.zMotion = zMotion.Value;
			if (angularXMotion.HasValue) joint.angularXMotion = angularXMotion.Value;
			if (angularYMotion.HasValue) joint.angularYMotion = angularYMotion.Value;
			if (angularZMotion.HasValue) joint.angularZMotion = angularZMotion.Value;
			
			return joint;
		}

		/// <summary>
		/// Sets the joints target rotation based on world space
		/// </summary>
		/// <param name="joint"></param>
		/// <param name="targetRotation">The target rotation, in world space</param>
		/// <param name="startRotation">The rotation the joint started with</param>
		/// <returns></returns>
		public static ConfigurableJoint SetTargetRotation(this ConfigurableJoint joint, Quaternion targetRotation, 
			Quaternion startRotation)
		{

			if (joint.configuredInWorldSpace)
				SetTargetRotationInternal(joint, targetRotation, startRotation, Space.World);
			else
				SetTargetRotationInternal(joint, joint.transform.InverseTransformRotation(targetRotation), startRotation, Space.Self);

			return joint;
		}

		/// <summary>
		/// Sets the joint's target rotation
		/// </summary>
		/// <param name="joint">The joint being modified</param>
		/// <param name="targetRotation">The target rotation</param>
		/// <param name="startRotation">The initial rotation of the joint</param>
		/// <param name="space">Specifies wether targetRotation is in world space, or local space</param>
		/// <returns></returns>
		public static ConfigurableJoint SetTargetRotation(this ConfigurableJoint joint, Quaternion targetRotation,
			Space space, Quaternion startRotation)
		{

			if (space == Space.Self)
				targetRotation = joint.transform.TransformRotation(targetRotation);

			SetTargetRotationInternal(joint, targetRotation, startRotation, Space.World);

			return joint;
		}


		private static void SetTargetRotationInternal(ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation, Space space)
		{
			// Calculate the rotation expressed by the joint's axis and secondary axis
			var right = joint.axis;
			var forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
			var up = Vector3.Cross(forward, right).normalized;
			Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

			// Transform into world space
			Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

			// Counter-rotate and apply the new local rotation.
			// Joint space is the inverse of world space, so we need to invert our value
			if (space == Space.World)
				resultRotation *= startRotation * Quaternion.Inverse(targetRotation);
			else
				resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;

			// Transform back into joint space
			resultRotation *= worldToJointSpace;

			// Set target rotation to our newly calculated rotation
			joint.targetRotation = resultRotation;
		}
	}
}
