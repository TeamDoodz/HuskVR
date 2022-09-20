using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuskVR.MonoBehaviours {
	public class VRUICanvas : MonoBehaviour {
		/*
		public static bool ShouldUpdatePos => 
			!SceneManager.GetActiveScene().name.StartsWith("Main Menu") && 
			!(OptionsManager.Instance != null && OptionsManager.Instance.paused); // this makes so much sense
		*/

		private const float dist = 200f;
		private static float scale = 0.25f;

		public void UpdatePos() {
			transform.position = UKUtils.HUDCam.transform.position + UKUtils.HUDCam.transform.forward * dist;
			transform.localScale = Vector3.one * scale;
			transform.rotation = UKUtils.HUDCam.transform.rotation;
		}

		private void Start() {
			UpdatePos();
		}

		private void Update() {
			/*if(ShouldUpdatePos)*/ UpdatePos();
		}
	}
}
