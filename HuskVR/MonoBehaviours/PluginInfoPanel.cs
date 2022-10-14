using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HuskVR.MonoBehaviours {
	public sealed class PluginInfoPanel : MonoBehaviour {
		public const string WEB_GITHUB = "https://github.com/TeamDoodz/HuskVR";
		public const string WEB_YOUTUBE = "https://www.youtube.com/channel/UC0pXKQKBGPzS7QRMCQojWug";

		[SerializeField] private TMP_Text versionLabel;

		private void Start() {
			versionLabel.text = MainPlugin.FullVersionString;
		}

		public void SocialsGithub() {
			Application.OpenURL(WEB_GITHUB);
		}
		public void SocialsYoutube() {
			Application.OpenURL(WEB_YOUTUBE);
		}
	}
}
