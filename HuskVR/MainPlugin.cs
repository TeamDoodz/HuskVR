using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace HuskVR {
	[BepInPlugin(GUID,Name,Version)]
	public class MainPlugin : BaseUnityPlugin {

		public const string GUID = "io.github.TeamDoodz.HuskVR";
		public const string Name = "HuskVR";
		public const string Version = "0.0.1";

		internal static ManualLogSource logger { get; private set; }

		private void Awake() {
			logger = Logger;

			new Harmony(GUID).PatchAll();

			VRInput.InitInput();

			logger.LogMessage($"{Name} version {Version} loaded!");
		}

	}
}
