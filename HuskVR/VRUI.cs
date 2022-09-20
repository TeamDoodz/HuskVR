using HuskVR.MonoBehaviours;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuskVR {
	public static class VRUI {
		public static void InitUI() {
			SceneManager.activeSceneChanged += (x,y) => SceneChanged();
		}

		private static void SceneChanged() {
			foreach(var canvas in Object.FindObjectsOfType<Canvas>()) {
				if(canvas.renderMode != RenderMode.ScreenSpaceOverlay) continue;
				canvas.renderMode = RenderMode.WorldSpace;
				canvas.gameObject.layer = 13; // always on top
				canvas.gameObject.AddComponent<VRUICanvas>();
			}
			var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			Object.Destroy(obj.GetComponent<Collider>());
			obj.transform.localScale = Vector3.one * 0.25f;
			obj.AddComponent<ControllerTracked>();
		}
	}
}
