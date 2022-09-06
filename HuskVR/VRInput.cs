using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace HuskVR {
	public static class VRInput {
		private static InputDevice? headset;
		private static InputDevice? leftHand;
		private static InputDevice? rightHand;

		public static InputDevice Headset => headset ?? throw new Exception("Headset is null.");
		public static InputDevice LeftHand => leftHand ?? throw new Exception("Left Hand is null.");
		public static InputDevice RightHand => rightHand ?? throw new Exception("Right Hand is null.");

		static List<InputDevice> inputDevices = null;
		internal static void InitInput() {
			MainPlugin.logger.LogMessage("Initializing input");

			InputDevices.GetDevices(inputDevices);

			foreach(var device in inputDevices) {
				MainPlugin.logger.LogInfo(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics));
				if(device.characteristics.HasFlag(InputDeviceCharacteristics.HeadMounted)) {
					headset = device;
				} else if(device.characteristics.HasFlag(InputDeviceCharacteristics.Controller)) {
					if(device.characteristics.HasFlag(InputDeviceCharacteristics.Left)) {
						leftHand = device;
					}
					if(device.characteristics.HasFlag(InputDeviceCharacteristics.Right)) {
						rightHand = device;
					}
				}
			}

			if(headset == null) MainPlugin.logger.LogError("Could not find headset. Make sure it exists and is connected to the system.");
			if(leftHand == null) MainPlugin.logger.LogError("Could not find left hand. Make sure it exists and is connected to the system.");
			if(rightHand == null) MainPlugin.logger.LogError("Could not find right hand. Make sure it exists and is connected to the system.");
		}
	}
}
