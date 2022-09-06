using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace HuskVR.Patches {
	// prevents NewMovement.Update from changing the position of the hud, this stops weapon viewmodels from jittering
	[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Update))]
	static class PlayerDontMoveHUDPatch {
		static Vector3 savedPos = Vector3.zero;
		static void Prefix(NewMovement __instance) {
			savedPos = __instance.hudCam.transform.localPosition;
		}
		static void Postfix(NewMovement __instance) {
			__instance.hudCam.transform.localPosition = savedPos;
		}
	}
}
