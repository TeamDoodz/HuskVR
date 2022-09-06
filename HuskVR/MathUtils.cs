using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HuskVR {
	public static class MathUtils {
		public static Vector3 Multiply(this Vector3 v1, Vector3 v2) { // no way
			return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}
		public static Vector3 Divide(this Vector3 v1, Vector3 v2) {
			return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
		}

		/// <summary>
		/// Calculates the distance between two points. Slightly faster than <see cref="Vector3.Distance(Vector3, Vector3)"/>.
		/// </summary>
		public static float FastDistance(Vector3 A, Vector3 B) {
			return (A - B).sqrMagnitude;
		}

		public static Matrix4x4 ConstructTransform(Vector3 scale, Quaternion rotation, Vector3 translation) {
			// Order matters
			return Matrix4x4.Scale(scale) * Matrix4x4.Rotate(rotation) * Matrix4x4.Translate(translation);
		}
	}
}
