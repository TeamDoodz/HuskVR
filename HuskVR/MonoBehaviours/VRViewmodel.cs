using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace HuskVR.MonoBehaviours {
	public class VRViewmodel : MonoBehaviour {
		private const float ANTISHAKE_DELTA = 0.005f;

		public ViewmodelType Type = ViewmodelType.Weapon;

		private Vector3 offset;

		private Vector3 viewmodelScale = new Vector3(0.5f, 0.5f, 0.5f);
		private float viewmodelFwd = 0.35f;
		private float viewmodelShift = 0.1f;
		private float viewmodelHeight = -0.1f;

		private Vector3 targetPos = Vector3.zero;

		private void Start() {
			if(Type == ViewmodelType.Fist) {
				viewmodelScale = new Vector3(0.3f, 0.3f, 0.3f);

			}

			transform.localScale = viewmodelScale;
			offset = transform.localPosition;
			targetPos = transform.position;
			GetComponent<WalkingBob>().enabled = false; // disable weapon bobbing when moving
		}

		private void Update() {
			targetPos = UKUtils.HUDCam.transform.TransformPoint(offset) + (UKUtils.HUDCam.transform.forward * viewmodelFwd) + (UKUtils.HUDCam.transform.right * viewmodelShift) + (UKUtils.HUDCam.transform.up * viewmodelHeight);
			transform.rotation = UKUtils.HUDCam.transform.rotation;

			// Only move the object if it is a certain distance away from target
			// this is a hack to prevent weird shaking
			float dist = MathUtils.FastDistance(transform.position, targetPos);
			if(dist > ANTISHAKE_DELTA) {
				transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.unscaledDeltaTime * dist * 5f);
			} else {
				transform.position = targetPos;
			}
		}
	}
}
