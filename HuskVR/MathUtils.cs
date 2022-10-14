using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HuskVR {
	public static class MathUtils {
		/// <summary>
		/// Calculates the distance between two points. Slightly faster than <see cref="Vector3.Distance(Vector3, Vector3)"/>.
		/// </summary>
		public static float FastDistance(Vector3 A, Vector3 B) {
			return (A - B).sqrMagnitude;
		}

		public static Vector3 XZ(this Vector3 vec3) {
			return new Vector3(vec3.x, 0f, vec3.z);
		}
	}
}
