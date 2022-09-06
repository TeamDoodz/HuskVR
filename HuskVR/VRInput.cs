using System;
using System.Collections.Generic;
using System.Text;
using HuskVR.MonoBehaviours;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace HuskVR {
	public static class VRInput {
		private static UnityEngine.XR.InputDevice? headset;
		private static UnityEngine.XR.InputDevice? leftHand;
		private static UnityEngine.XR.InputDevice? rightHand;

		public static UnityEngine.XR.InputDevice Headset => headset ?? throw new Exception("Headset is null.");
		public static UnityEngine.XR.InputDevice LeftHand => leftHand ?? throw new Exception("Left Hand is null.");
		public static UnityEngine.XR.InputDevice RightHand => rightHand ?? throw new Exception("Right Hand is null.");

		public static bool IsRightHandTriggerDown {
			get {
				if(RightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool outp)) {
					return outp;
				} else {
					MainPlugin.logger.LogWarning("Could not read triggerButton usage of Right Hand");
					return false;
				}
			}
		}
		public static bool IsRightHandBDown {
			get {
				if(RightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool outp)) {
					return outp;
				} else {
					MainPlugin.logger.LogWarning("Could not read secondaryButton usage of Right Hand");
					return false;
				}
			}
		}

		static List<UnityEngine.XR.InputDevice> inputDevices = new List<UnityEngine.XR.InputDevice>();
		internal static void InitInput() {
			MainPlugin.logger.LogMessage("Initializing input");

			inputDevices = new List<UnityEngine.XR.InputDevice>();
			InputDevices.GetDevices(inputDevices);

			if(inputDevices == null) {
				MainPlugin.logger.LogError("Failed to get input devices.");
				return;
			}

			foreach(var device in inputDevices) {
				InitDevice(device);
			}

			InputDevices.deviceConnected += InitDevice;

			if(headset == null) MainPlugin.logger.LogWarning("Could not find headset. Make sure it exists and is connected to the system.");
			if(leftHand == null) MainPlugin.logger.LogWarning("Could not find left hand. Make sure it exists and is connected to the system.");
			if(rightHand == null) MainPlugin.logger.LogWarning("Could not find right hand. Make sure it exists and is connected to the system.");
		}

		private static void InitDevice(UnityEngine.XR.InputDevice device) {
			MainPlugin.logger.LogInfo(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics));
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
		}
	}
}
