﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HuskVR.Patches;
using UnityEngine;
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
		private bool nextVariant = false;
		private bool jump = false;
		private bool slide = false;
		private bool dash = false;

		private float turnSpeed = 300f;

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
				InputManager.Instance.InputSource.PrevWeapon.Trigger(prevWeapon, !prevWeapon);
			}
			if(VRInput.IsSwitchVariantDown != nextVariant) {
				nextVariant = VRInput.IsSwitchVariantDown;
				InputManager.Instance.InputSource.ChangeVariation.Trigger(nextVariant, !nextVariant);
			}
			if(VRInput.IsJumpDown != jump) {
				jump = VRInput.IsJumpDown;
				InputManager.Instance.InputSource.Jump.Trigger(jump, !jump);
			}
			if(VRInput.IsSlideDown != slide) {
				slide = VRInput.IsSlideDown;
				InputManager.Instance.InputSource.Slide.Trigger(slide, !slide);
			}
			if(VRInput.IsDashDown != dash) {
				dash = VRInput.IsDashDown;
				InputManager.Instance.InputSource.Dodge.Trigger(dash, !dash);
			}
			if(VRInput.IsTurnLeftDown) {
				VRCameraCorrectionPatch.Offset += -turnSpeed * Time.deltaTime;
			} else if(VRInput.IsTurnRightDown) {
				VRCameraCorrectionPatch.Offset += turnSpeed * Time.deltaTime;
			}
		}
	}
}
