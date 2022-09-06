using System;
using System.Collections.Generic;
using HarmonyLib;

namespace HuskVR.Patches {
	/*
	// force player hitscan attacks to point where the hud camera is pointing
	[HarmonyPatch(typeof(RevolverBeam), nameof(RevolverBeam.Start))]
	static class FixHitscanAttacksPatch {
		static void Prefix(RevolverBeam __instance) {
			MainPlugin.logger.LogInfo(__instance.beamType);
			if(__instance.beamType == BeamType.Enemy || __instance.beamType == BeamType.MaliciousFace) return; // only player hitscans are affected
			MainPlugin.logger.LogInfo("Shooting");
			__instance.transform.position = UKUtils.HUDCam.transform.position;
			__instance.transform.rotation = UKUtils.HUDCam.transform.rotation;
		}
	}
	*/
}
