using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace HuskVR.Patches {
	public class RotPlayer
	{
		//im sorry this is a hack

		static GameObject camera;
		static GameObject holder;

		[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Update))]
		static class RotPlayer_Update
		{
			static void Postfix(NewMovement __instance)
			{
				if (camera != null)
				{
					__instance.transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);
					holder.transform.rotation = Quaternion.Euler(0, 0, 0);
					camera.transform.position = new Vector3(0, camera.transform.position.y, 0);
				}
			}
		}

		[HarmonyPatch(typeof(NewMovement), nameof(NewMovement.Start))]
		static class RotPlayer_Start
		{
			static void Postfix(NewMovement __instance)
			{
				__instance.StartCoroutine(WaitAndParent(__instance));
			}
		}

		static IEnumerator WaitAndParent(NewMovement __instance)
        {
			yield return new WaitForSeconds(1);

			holder = new GameObject("Camera Holder");
			holder.transform.parent = __instance.gameObject.transform;

			camera = __instance.gameObject.ChildByName("Main Camera");
			holder.transform.localPosition = Vector3.zero;
			camera.transform.parent = holder.transform;
			camera.transform.localPosition = Vector3.zero;
		}
	}
}
