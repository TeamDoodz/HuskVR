using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using Mono.Cecil;

namespace HuskVR.Patcher {
	public static class MainPatcher {
		private static ManualLogSource logger = new ManualLogSource("HuskVR.Patcher");

		public static IEnumerable<string> TargetDLLs { get; } = new string[0];

		public static void Patch(AssemblyDefinition ad) {
		}
	}
}
