using System;
using System.Collections.Generic;
using HarmonyLib;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(PostProcessV2_Handler), nameof(PostProcessV2_Handler.SetupRTs))]
	static class DontUpdateRTsIfCamNullPatch {
		static bool Prefix(PostProcessV2_Handler __instance) {
			return __instance.mainCam != null && __instance.hudCam != null;
		}
	}
}
