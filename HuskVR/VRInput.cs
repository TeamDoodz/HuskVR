using System;
using System.Collections.Generic;
using System.Text;
using HuskVR.MonoBehaviours;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace HuskVR {
	public static class VRInput {
		private static InputDevice? headset;
		private static InputDevice? leftHand;
		private static InputDevice? rightHand;

		public static InputDevice Headset => headset ?? throw new Exception("Headset is null.");
		public static InputDevice LeftHand => leftHand ?? throw new Exception("Left Hand is null.");
		public static InputDevice RightHand => rightHand ?? throw new Exception("Right Hand is null.");

		public static bool IsFireDown {
			get {
				if(!RightHand.isValid) {
					MainPlugin.logger.LogError("Right Hand is invalid.");
					return false;
				}
				if(RightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool outp)) {
					return outp;
				} else {
					MainPlugin.logger.LogWarning("Could not read triggerButton usage of Right Hand");
					return false;
				}
			}
		}
		public static bool IsAltFireDown {
			get {
				if(!RightHand.isValid) {
					MainPlugin.logger.LogError("Right Hand is invalid.");
					return false;
				}
				if(RightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool outp)) {
					return outp;
				} else {
					MainPlugin.logger.LogWarning("Could not read gripButton usage of Right Hand");
					return false;
				}
			} 
		}

		static List<InputDevice> inputDevices = new List<InputDevice>();
		internal static void InitInput() {
			MainPlugin.logger.LogMessage("Initializing input");

			inputDevices = new List<InputDevice>();
			InputDevices.GetDevices(inputDevices);

			if(inputDevices == null) {
				MainPlugin.logger.LogError("Failed to get input devices.");
				return;
			}

			SceneManager.activeSceneChanged += (Scene a, Scene b) => {
				if(VRInputManager.Instance == null) {
					new GameObject("VR Input Manager").AddComponent<VRInputManager>();
				}
			};

			foreach(var device in inputDevices) {
				InitDevice(device);
			}

			InputDevices.deviceConnected += InitDevice;

			if(headset == null) MainPlugin.logger.LogWarning("Could not find headset. Make sure it exists and is connected to the system.");
			if(leftHand == null) MainPlugin.logger.LogWarning("Could not find left hand. Make sure it exists and is connected to the system.");
			if(rightHand == null) MainPlugin.logger.LogWarning("Could not find right hand. Make sure it exists and is connected to the system.");
		}

		private static void InitDevice(InputDevice device) {
			MainPlugin.logger.LogMessage(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics));
			if(device.characteristics.HasFlag(InputDeviceCharacteristics.HeadMounted)) {
				headset = device;
			} else if(device.characteristics.HasFlag(InputDeviceCharacteristics.Controller)) {
				if(device.characteristics.HasFlag(InputDeviceCharacteristics.Right)) {
					rightHand = device;
				}
				if(device.characteristics.HasFlag(InputDeviceCharacteristics.Left)) {
					leftHand = device;
				}
			}
			List<InputFeatureUsage> usages = new List<InputFeatureUsage>();
			device.TryGetFeatureUsages(usages);
			foreach(InputFeatureUsage usage in usages) {
				MainPlugin.logger.LogInfo(usage.name);
			}
		}

		public static void Trigger(this InputActionState state, bool started, bool canceled) {
			if(started) {
				state.IsPressed = true;
				state.PerformedFrame = Time.frameCount;
				state.PerformedTime = Time.time;
			} else if(canceled) {
				state.IsPressed = false;
				state.CanceledFrame = Time.frameCount;
			}
		}
	}
}
