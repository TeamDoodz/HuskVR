using System;
using System.Collections.Generic;
using HarmonyLib;
using HuskVR.MonoBehaviours;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(FistControl), nameof(FistControl.Start))]
	static class FixArmViewmodelPatch {
		static void Prefix(FistControl __instance) {
			var vm = __instance.gameObject.AddComponent<VRViewmodel>();
			vm.Type = ViewmodelType.Fist;
		}
	}
}
