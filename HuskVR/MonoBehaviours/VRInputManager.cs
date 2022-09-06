using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine.InputSystem;

namespace HuskVR.MonoBehaviours {
	[ConfigureSingleton(SingletonFlags.PersistAutoInstance)]
	public class VRInputManager : MonoSingleton<VRInputManager> {
		private bool fire1 = false;
		private bool fire2 = false;

		private void Update() {
			if(VRInput.IsFireDown != fire1) {
				fire1 = VRInput.IsFireDown;
				InputManager.Instance.InputSource.Fire1.Trigger(fire1, !fire1);
				MainPlugin.logger.LogInfo("fire");
			}
			if(VRInput.IsAltFireDown != fire2) {
				fire2 = VRInput.IsAltFireDown;
				InputManager.Instance.InputSource.Fire2.Trigger(fire2, !fire2);
				MainPlugin.logger.LogInfo("altfire");
			}
		}
	}
}
