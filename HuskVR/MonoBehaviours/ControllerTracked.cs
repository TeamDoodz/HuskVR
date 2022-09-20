using UnityEngine;
using UnityEngine.XR;

namespace HuskVR.MonoBehaviours {
	public class ControllerTracked : MonoBehaviour {

		private void Update() {
			transform.position = VRInput.RightHandPosition - VRInput.HeadPosition + UKUtils.HUDCam.transform.position;
		}
	}
}
