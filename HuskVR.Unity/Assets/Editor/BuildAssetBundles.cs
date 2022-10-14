using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HuskVR.Unity.Editor {
	public sealed class BuildAssetBundles {
		const string RESULT_PATH = "Built Bundles";

		[MenuItem("Assets/Build AssetBundles")]
		static void DoTheThing() {
			if(!Directory.Exists(RESULT_PATH)) {      
				Directory.CreateDirectory(RESULT_PATH);
			}

			BuildPipeline.BuildAssetBundles(RESULT_PATH,
				BuildAssetBundleOptions.None,
				BuildTarget.StandaloneWindows64);
		}
	}
}
