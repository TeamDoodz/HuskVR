using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace HuskVR {
	public static class VersionChecker {
		const string VERSION_URI = "https://api.github.com/repos/TeamDoodz/HuskVR/tags";

		public static Version LatestVersion { get; private set; }

		private static string Get(string uri) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using(Stream stream = response.GetResponseStream())
			using(StreamReader reader = new StreamReader(stream)) {
				string responseString = reader.ReadToEnd();
				MainPlugin.logger.LogDebug($"Request: {uri}");
				MainPlugin.logger.LogDebug($"Response:\n{responseString}");
				return responseString;
			}
		}

		internal static void CheckVersion() {
			VersionTag[] versions = JsonConvert.DeserializeObject<VersionTag[]>(Get(VERSION_URI));
			if(versions.Length == 0) {
				MainPlugin.logger.LogDebug("HuskVR has no public releases");
			}
			if(versions[0].Name > MainPlugin.Version) {
				MainPlugin.logger.LogWarning($"HuskVR outdated. Current version: {MainPlugin.Version}; Latest version: {LatestVersion}");
			}
		}

		private struct VersionTag {
			[JsonProperty("name", ItemConverterType = typeof(Newtonsoft.Json.Converters.VersionConverter))]
			public Version Name;
		}
	}
}
