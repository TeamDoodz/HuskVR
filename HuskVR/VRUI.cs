using HuskVR.MonoBehaviours;
using HuskVR.Patches;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuskVR {
	public static class VRUI {
		public static Camera UICam { get; private set; }

		public static void InitUI() {
			SceneManager.activeSceneChanged += (x,y) => SceneChanged(y);
		}

		private static void SceneChanged(Scene after) {
			CreateUICam();
			foreach(var canvas in Object.FindObjectsOfType<Canvas>()) {
				ConvertCanvas(canvas);
			}
		}

		private static void ConvertCanvas(Canvas canvas) {
			if(canvas.renderMode != RenderMode.ScreenSpaceOverlay) return;
			canvas.renderMode = RenderMode.WorldSpace;
			canvas.gameObject.layer = 5; // ui
			canvas.gameObject.AddComponent<VRUICanvas>();
		}

		private static void CreateUICam() {
			UICam = new GameObject("UI Camera").AddComponent<Camera>();
			UICam.cullingMask = LayerMask.GetMask(new string[] { "UI" });
			UICam.clearFlags = CameraClearFlags.Depth;
			UICam.depth = 1f;
		}
	}
}
