using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HuskVR.MonoBehaviours {
	public class VRUICanvas : MonoBehaviour {
		public static bool ShouldUpdatePos => 
			!SceneManager.GetActiveScene().name.StartsWith("Main Menu") && 
			!(OptionsManager.Instance != null && OptionsManager.Instance.paused) &&  // this makes so much sense
			!(SpawnMenu.Instance != null && SpawnMenu.Instance.gameObject.activeInHierarchy);

		private Vector3 lastCamFwd = Vector3.zero;

		private const float dist = 50f;
		private static float scale = 0.0625f;

		public void UpdatePos() {
			lastCamFwd = VRUI.UICam.transform.forward * dist;
			transform.rotation = VRUI.UICam.transform.rotation;
		}
		public void ResetPos() {
			lastCamFwd = lastCamFwd.XZ();
			transform.LookAt(VRUI.UICam.transform);
			transform.forward = -transform.forward.XZ();
		}

		private void Start() {
			transform.localScale = Vector3.one * scale;
			lastCamFwd = Vector3.back * dist;
		}

		private void Update() {
			if(ShouldUpdatePos) UpdatePos(); else ResetPos();
			transform.position = VRUI.UICam.transform.position + lastCamFwd;
		}

		public static void ConvertCanvas(Canvas canvas) {
			if(canvas.renderMode != RenderMode.ScreenSpaceOverlay) return;
			canvas.worldCamera = VRUI.UICam;
			canvas.renderMode = RenderMode.WorldSpace;
			canvas.gameObject.layer = 5; // ui
			canvas.gameObject.AddComponent<VRUICanvas>();

			foreach(Transform child in canvas.transform) {
				ConvertElement(child);
			}
		}

		private static void ConvertElement(Transform element) {
			if(element.GetComponent<Selectable>() is Selectable button) {
				// ColorBlock is a value type which means in order to modify it we need this jank
				var block = button.colors;
				block.highlightedColor = Color.green;
				button.colors = block;
			}

			foreach(Transform child in element) {
				ConvertElement(child);
			}
		}
	}
}
