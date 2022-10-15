using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Management;

namespace HuskVR {
	public static class VREnabler {
		// Thank you to Raicuparta (creator of the outer wilds/firewatch VR mods) for publishing this code
		public static void EnableVR() {
			var generalSettings = ScriptableObject.CreateInstance<XRGeneralSettings>();
			var managerSettings = ScriptableObject.CreateInstance<XRManagerSettings>();

		}
	}
}
