using System;
using System.Collections.Generic;
using  HarmonyLib;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(HUDPos), nameof(HUDPos.Start))]
	static class MoveHUDPatch {
		static void Prefix(HUDPos __instance) {
			if(__instance.gameObject.name == "GunCanvas") {
				__instance.transform.localScale = new UnityEngine.Vector3(0.001f, 0.001f, 0.001f);
				__instance.defaultPos = new UnityEngine.Vector3(-0.75f, -0.4f, 1.6f);
			} else if(__instance.gameObject.name == "StyleCanvas") {
				__instance.transform.localScale = new UnityEngine.Vector3(0.002f, 0.002f, 0.001f);
				__instance.defaultPos = new UnityEngine.Vector3(1.3f, 0.2f, 1.6f);
			}
		}
	}
}
