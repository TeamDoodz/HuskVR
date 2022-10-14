using HuskVR.MonoBehaviours;
using HuskVR.Patches;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HuskVR {
	public static class VRUI {
		public static Camera UICam { get; private set; }

		public static void InitUI() {
			SceneManager.activeSceneChanged += (x,y) => SceneChanged(y);
		}

		private static void SceneChanged(Scene after) {
			CreateUICam();
			foreach(var canvas in Object.FindObjectsOfType<Canvas>()) {
				VRUICanvas.ConvertCanvas(canvas);
			}
		}

		private static void CreateUICam() {
			UICam = new GameObject("UI Camera").AddComponent<Camera>();
			UICam.cullingMask = LayerMask.GetMask(new string[] { "UI" });
			UICam.clearFlags = CameraClearFlags.Depth;
			UICam.depth = 1f;
			UICam.gameObject.AddComponent<GazeUIInteraction>();
		}
	}
}
