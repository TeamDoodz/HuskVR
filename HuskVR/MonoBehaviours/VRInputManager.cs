using System;
using System.Collections.Generic;
using System.Text;

namespace HuskVR.MonoBehaviours {
	[ConfigureSingleton(SingletonFlags.PersistAutoInstance)]
	public class VRInputManager : MonoSingleton<VRInputManager> {
		private void Update() {
			if(VRInput.IsRightHandTriggerDown) {
				Fire();
			}
		}
		private void Fire() {
			if(GunControl.Instance == null) return;
			{
				RocketLauncher rocketLauncher = GunControl.Instance.currentWeapon.GetComponent<RocketLauncher>();
				if(rocketLauncher != null) {
					if(!rocketLauncher.wid || rocketLauncher.wid.delay == 0f) {
						rocketLauncher.Shoot();
						return;
					}
					rocketLauncher.Invoke("Shoot", rocketLauncher.wid.delay);
					rocketLauncher.cooldown = 1f;
				}
			}
		}
	}
}
