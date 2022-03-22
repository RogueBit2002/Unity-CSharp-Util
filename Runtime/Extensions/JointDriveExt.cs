using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSUtil
{
    public static class JointDriveExt
    {
        public static JointDrive Set(this JointDrive drive, float spring, float damping, float max)
		{
			drive.positionSpring = spring;
			drive.positionDamper = damping;
			drive.maximumForce = max;

			return drive;
		}
    }
}
