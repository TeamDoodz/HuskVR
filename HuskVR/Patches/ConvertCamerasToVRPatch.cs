using System;
using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace HuskVR.Patches {
	[HarmonyPatch(typeof(CameraController), nameof(CameraController.Start))]
	static class ConvertCamerasToVRPatch {
		static IEnumerator ConvertCamera(Camera cam, Action<Camera> callback, Action<Camera> ppFound) {
			// there is no way to change a normal camera to a vr camera
			// so instead, we create a new camera with similar properties to the original one and make that a vr cam

			GameObject obj = cam.gameObject;
			MainPlugin.logger.LogMessage($"Converting camera {obj.name} to VR");

			// the autoaim system prevents us from deleting the main camera due to unity jank
			// but we cant get rid of autoaim because other objects depend on it due to ultrakill jank
			// so we have to create a new autoaim and hope it doesn't break anything
			CameraFrustumTargeter oldFT = obj.GetComponent<CameraFrustumTargeter>();

			RectTransform crosshair = null;
			LayerMask mask = -1;
			LayerMask occlusionMask = -1;
			float maximumRange = 0f;
			float maxHorAim = 0f;
			bool replaceFT = false;
			if(oldFT != null) {
				MainPlugin.logger.LogMessage($"Saving autoaim values");
				replaceFT = true;
				crosshair = oldFT.crosshair;
				mask = oldFT.mask;
				occlusionMask = oldFT.occlusionMask;
				maximumRange = oldFT.maximumRange;
				maxHorAim = oldFT.maxHorAim;
				GameObject.Destroy(oldFT);
			}

			MainPlugin.logger.LogMessage($"Saving camera values");
			float depth = cam.depth;
			CameraType cameraType = cam.cameraType;
			CameraClearFlags clearFlags = cam.clearFlags;
			int cullingMask = cam.cullingMask;
			RenderTexture targetTexture = cam.targetTexture;

			GameObject.Destroy(cam);

			yield return new WaitForEndOfFrame(); // camera isn't destroyed until after this frame is over

			MainPlugin.logger.LogMessage($"Replacing camera");

			var newCam = obj.AddComponent<Camera>();
			newCam.depth = depth;
			newCam.cameraType = cameraType | CameraType.VR; // i have no idea if this is necessary, but whatever
			newCam.clearFlags = obj.name == "Main Camera" ? clearFlags : CameraClearFlags.Depth; // dirty hack
			newCam.cullingMask = cullingMask;
			newCam.targetTexture = targetTexture;
			newCam.backgroundColor = Color.black; // this fixes weird jank in the main menu

			if(replaceFT) {
				MainPlugin.logger.LogMessage($"Replacing autoaim");
				var newFT = obj.AddComponent<CameraFrustumTargeter>();
				newFT.crosshair = crosshair;
				newFT.mask = mask;
				newFT.occlusionMask = occlusionMask;
				newFT.maximumRange = maximumRange;
				newFT.maxHorAim = maxHorAim;
				newFT.camera = newCam;
			}

			MainPlugin.logger.LogMessage($"Invoking callback");
			callback(newCam);

			yield return new WaitUntil(() => PostProcessV2_Handler.Instance != null);

			MainPlugin.logger.LogMessage($"Invoking PP callback");
			ppFound(newCam);

			newCam.depth++; // we have to do this at the end for some reason
		}
		static void Prefix(CameraController __instance) {
			__instance.StartCoroutine(ConvertCamera(__instance.cam, (Camera cam) => { 
				__instance.cam = cam; 
			}, (Camera cam) => {
				if(PostProcessV2_Handler.Instance != null) {
					PostProcessV2_Handler.Instance.mainCam = cam;
				}
			}));
			__instance.StartCoroutine(ConvertCamera(__instance.hudCamera, (Camera cam) => { 
				__instance.hudCamera = cam;
			}, (Camera cam) => {
				if(PostProcessV2_Handler.Instance != null) {
					PostProcessV2_Handler.Instance.hudCam = cam;
				}
			}));

			GameObject.Find("Virtual Camera").SetActive(false); // i have no idea what the "virtual camera" is so this might be a bad idea
		}
	}
}
