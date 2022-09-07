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
		private bool punch = false;
		private bool changeFist = false;
		private bool nextWeapon = false;
		private bool prevWeapon = false;

		private void Update() {
			if(VRInput.IsFireDown != fire1) {
				fire1 = VRInput.IsFireDown;
				InputManager.Instance.InputSource.Fire1.Trigger(fire1, !fire1);
			}
			if(VRInput.IsAltFireDown != fire2) {
				fire2 = VRInput.IsAltFireDown;
				InputManager.Instance.InputSource.Fire2.Trigger(fire2, !fire2);
			}
			if(VRInput.IsPunchDown != punch) {
				punch = VRInput.IsPunchDown;
				InputManager.Instance.InputSource.Punch.Trigger(punch, !punch);
			}
			if(VRInput.IsSwitchFistDown != changeFist) {
				changeFist = VRInput.IsSwitchFistDown;
				InputManager.Instance.InputSource.ChangeFist.Trigger(changeFist, !changeFist);
			}
			if(VRInput.IsNextWeaponDown != nextWeapon) {
				nextWeapon = VRInput.IsNextWeaponDown;
				InputManager.Instance.InputSource.NextWeapon.Trigger(nextWeapon, !nextWeapon);
			}
			if(VRInput.IsPrevWeaponDown != prevWeapon) {
				prevWeapon = VRInput.IsPrevWeaponDown;
				InputManager.Instance.InputSource.NextWeapon.Trigger(prevWeapon, !prevWeapon);
			}
		}
	}
}
