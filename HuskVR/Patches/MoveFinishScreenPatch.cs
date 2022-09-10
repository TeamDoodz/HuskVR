using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(FinalRank), nameof(FinalRank.Start))]
	static class MoveFinishScreenPatch {
		static void Prefix(FinalRank __instance) {
			__instance.transform.parent.localScale = new UnityEngine.Vector3(0.003f, 0.002f, 0.001f);
			__instance.transform.parent.localPosition = new UnityEngine.Vector3(0f, -0.2f, 2.2f);
		}
	}
}
