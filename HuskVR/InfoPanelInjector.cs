using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuskVR {
	public static class InfoPanelInjector {
		public static void Init() {
			SceneManager.activeSceneChanged += (x, y) => SceneChanged(y);
		}

		private static AssetBundle infoPanelBundle;
		private static AssetBundle InitInfoPanel() {
			if(infoPanelBundle == null) {
				infoPanelBundle = AssetBundle.LoadFromFile(Path.Combine(MainPlugin.RootPath, "assets", "info_panel"));
			}
			return infoPanelBundle;
		}

		private static void SceneChanged(Scene after) {
			if(!after.name.StartsWith("Main Menu")) return;
			AssetBundle bundle = InitInfoPanel();
			GameObject.Instantiate(bundle.LoadAsset<GameObject>("HuskVR_Info"));
		}
	}
}
