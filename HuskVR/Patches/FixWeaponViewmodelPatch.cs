using System;
using System.Collections.Generic;
using HarmonyLib;
using HuskVR.MonoBehaviours;

namespace HuskVR.Patches {
	// autoaim messes with viewmodel rotation, cant have that
	[HarmonyPatch(typeof(RotateToFaceFrustumTarget), nameof(RotateToFaceFrustumTarget.Update))]
	static class FixWeaponViewmodelPatch {
		static bool Prefix(RotateToFaceFrustumTarget __instance) {
			__instance.enabled = false; // destroying it might cause errors
			__instance.gameObject.AddComponent<VRViewmodel>();
			return false;
		}
	}
}
