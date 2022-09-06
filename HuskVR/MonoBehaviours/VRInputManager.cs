using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HuskVR.MonoBehaviours {
	/*
	[ConfigureSingleton(SingletonFlags.PersistAutoInstance)]
	public class VRInputManager : MonoSingleton<VRInputManager> {
		// there is a much more elegant way to do this but whatevs
		private void Update() {
			if(VRInput.MockInput == null) {
				VRInput.MockInput = VRInput.SetUpMockInputForActions(InputManager.Instance.bindings[0].Action.actionMap.asset);
			}
			if(VRInput.MockInput == null) {
				MainPlugin.logger.LogError("MockInput is null.");
				return;
			}
			if(VRInput.IsRightHandTriggerDown) {
				Fire();
			}
		}
		private void Fire() {
			
			if(GunControl.Instance == null) return;
			{
				RocketLauncher rocketLauncher = GunControl.Instance.currentWeapon.GetComponent<RocketLauncher>();
				if(rocketLauncher != null) {
					if(rocketLauncher.cooldown > 0f) return;
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
*/
}
