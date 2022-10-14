using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

namespace HuskVR.MonoBehaviours {
	// Based on https://assetstore.unity.com/packages/tools/gui/gaze-ui-for-canvas-70881
	public sealed class GazeUIInteraction : MonoBehaviour {
		// Prevents loop over the same selectable
		Selectable excludedSelectable;
		Selectable currentSelectable;
		RaycastResult currentRaycastResult;

		IPointerClickHandler clickHandler;
		IDragHandler dragHandler;

		PointerEventData pointerEvent;

		private void Start() {
			pointerEvent = new PointerEventData(EventSystem.current) {
				button = PointerEventData.InputButton.Left
			};
		}

		void Update() {
			// Set pointer position
			pointerEvent.position = new Vector2(XRSettings.eyeTextureWidth / 2, XRSettings.eyeTextureHeight / 2);

			List<RaycastResult> raycastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEvent, raycastResults);

			// Detect selectable
			if(raycastResults.Count > 0) {
				foreach(var result in raycastResults) {
					var newSelectable = result.gameObject.GetComponentInParent<Selectable>();

					if(newSelectable) {
						if(newSelectable != excludedSelectable && newSelectable != currentSelectable) {
							Select(newSelectable);
							currentRaycastResult = result;
						}
						break;
					}
				}
			} else {
				if(currentSelectable || excludedSelectable) {
					Select(null, null);
				}
			}

			// Target is being activating
			if(currentSelectable != null && (InputManager.Instance?.InputSource.Fire1.WasPerformedThisFrame ?? false)) {
				if(clickHandler != null) {
					clickHandler.OnPointerClick(pointerEvent);
					Select(null, currentSelectable);
				} else if(dragHandler != null) {
					pointerEvent.pointerPressRaycast = currentRaycastResult;
					dragHandler.OnDrag(pointerEvent);
				}
			}
		}

		void Select(Selectable s, Selectable exclude = null) {
			excludedSelectable = exclude;

			if(currentSelectable)
				currentSelectable.OnPointerExit(pointerEvent);

			currentSelectable = s;

			if(currentSelectable) {
				currentSelectable.OnPointerEnter(pointerEvent);
				clickHandler = currentSelectable.GetComponent<IPointerClickHandler>();
				dragHandler = currentSelectable.GetComponent<IDragHandler>();
			}
		}
	}
}
