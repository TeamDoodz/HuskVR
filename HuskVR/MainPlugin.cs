using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace HuskVR {
	[BepInPlugin(GUID,Name,VersionString)]
	public class MainPlugin : BaseUnityPlugin {

		public const string GUID = "io.github.TeamDoodz.HuskVR";
		public const string Name = "HuskVR";
		private const string VersionString = "0.0.1";

		public static Version Version { get; } = new Version(VersionString);
		public static string BuildConfiguration { get; } = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration;
		public static string FullVersionString => $"{Version} {BuildConfiguration} {(VersionChecker.IsOutdated ? "(outdated)" : "")}";
		public static string RootPath;

		internal static ManualLogSource logger { get; private set; }

		private void Awake() {
			logger = Logger;
			RootPath = Path.GetDirectoryName(Info.Location);

			VersionChecker.CheckVersion();

			new Harmony(GUID).PatchAll();

			VRInput.InitInput();
			InfoPanelInjector.Init();
			VRUI.InitUI();

			logger.LogMessage($"{Name} version {FullVersionString} loaded!");
		}

		private void Update() {
			if(Input.GetKeyDown(KeyCode.F1)) {
				UKUtils.HUDCam.enabled = !UKUtils.HUDCam.enabled;
			}
		}

    }
}
