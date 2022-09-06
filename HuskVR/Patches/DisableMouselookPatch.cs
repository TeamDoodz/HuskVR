using System;
using System.Collections.Generic;
using HarmonyLib;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(CameraController), nameof(CameraController.Update))]
	static class DisableMouselookPatch {
		static void Prefix(CameraController __instance) {
			__instance.activated = false; // this only disables mouselook
		}
	}
}
