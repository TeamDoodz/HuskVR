using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace HuskVR.Patches {
	[HarmonyPatch]
	public static class VRCameraCorrectionPatch {

		public static float Offset { get; set; } = 0f;

		static GameObject container; // container? i hardly even know her!

		[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Start))]
		[HarmonyPrefix]
		static void Setup() {
			container = new GameObject("Main Camera Container");
			container.transform.parent = UKUtils.MainCam.transform.parent;
			container.transform.localPosition = Vector3.zero;
			container.transform.rotation = UKUtils.MainCam.transform.rotation;

			UKUtils.MainCam.transform.parent = container.transform;
		}

		[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Update))]
		[HarmonyPrefix]
		static void Update(NewMovement __instance) {
			if(__instance.dead) return;
			__instance.transform.rotation = Quaternion.Euler(__instance.transform.rotation.eulerAngles.x, UKUtils.MainCam.transform.rotation.eulerAngles.y, __instance.transform.rotation.eulerAngles.z);
			container.transform.rotation = Quaternion.Euler(0f, Offset, 0f);
		}
	}
}
