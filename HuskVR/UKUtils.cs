using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HuskVR {
	public static class UKUtils {
		public static Camera MainCam {
			get {
				if(maincam == null) maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
				return maincam;
			}
		}
		private static Camera maincam;
		public static Camera HUDCam {
			get {
				if(hudcam == null) hudcam = GameObject.Find("HUD Camera").GetComponent<Camera>();
				return hudcam;
			}
		}
		private static Camera hudcam;
	}
}
