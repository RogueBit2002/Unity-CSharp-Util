using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.CSharpUtil.Extensions
{
    public static class DebugExt
    {
        private static Color defaultColor = Color.white;

        #region Marker
        public static void DrawMarker(Vector3 position)
        {
            DrawMarker(position, Vector3.one, Quaternion.identity, defaultColor, 0, true);
        }

        public static void DrawMarker(Vector3 position, Vector3 scale)
        {
            DrawMarker(position, scale, Quaternion.identity, defaultColor, 0, true);
        }

        public static void DrawMarker(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            DrawMarker(position, scale, rotation, defaultColor, 0, true);
        }

        public static void DrawMarker(Vector3 position, Vector3 scale, Quaternion rotation, Color color)
        {
            DrawMarker(position, scale, rotation, color, 0, true);
        }

        public static void DrawMarker(Vector3 position, Vector3 scale, Quaternion rotation, Color color, float duration)
        {
            DrawMarker(position, scale, rotation, color, duration, true);
        }

        public static void DrawMarker(Vector3 position, Vector3 scale, Quaternion rotation, Color color, float duration, bool depthTest)
        {
            Vector3 right = rotation * Vector3.right;
            Vector3 up = rotation * Vector3.up;
            Vector3 forward = rotation * Vector3.forward;

            Vector3 r = 0.5f * right * scale.x;
            Vector3 u = 0.5f * up * scale.y;
            Vector3 f = 0.5f * forward * scale.z;

            Debug.DrawLine(position - r, position + r, color, duration, true);
            Debug.DrawLine(position - u, position + u, color, duration, true);
            Debug.DrawLine(position - f, position + f, color, duration, true);
        }

        #endregion

        #region Triangle
        public static void DrawTriangle(Vector3 positionA, Vector3 positionB, Vector3 positionC)
        {
            DrawTriangle(positionA, positionB, positionC, defaultColor, 0, true);
        }

        public static void DrawTriangle(Vector3 positionA, Vector3 positionB, Vector3 positionC, Color color)
        {
            DrawTriangle(positionA, positionB, positionC, color, 0, true);
        }

        public static void DrawTriangle(Vector3 positionA, Vector3 positionB, Vector3 positionC, Color color, float duration)
        {
            DrawTriangle(positionA, positionB, positionC, color, duration, true);
        }

        public static void DrawTriangle(Vector3 positionA, Vector3 positionB, Vector3 positionC, Color color, float duration, bool depthTest)
        {
            Debug.DrawLine(positionA, positionB, color, duration, depthTest);
            Debug.DrawLine(positionA, positionC, color, duration, depthTest);
            Debug.DrawLine(positionB, positionC, color, duration, depthTest);
        }

        #endregion
    }
}
