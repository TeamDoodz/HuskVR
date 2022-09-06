using System;
using System.Collections.Generic;
using HarmonyLib;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(PlayerInput))]
	static class InputPatches {
		[HarmonyPatch(nameof(PlayerInput.Fire1), MethodType.Getter)]
		[HarmonyPostfix]
		static void Fire1(ref InputActionState __result) {
			__result.IsPressed = VRInput.IsRightHandTriggerDown;
		}
		[HarmonyPatch(nameof(PlayerInput.Fire2), MethodType.Getter)]
		[HarmonyPostfix]
		static void Fire2(ref InputActionState __result) {
			__result.IsPressed = VRInput.IsRightHandBDown;
		}
	}
}
