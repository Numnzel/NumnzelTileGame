using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : Controller {

	// TODO: Make an interface for controlling selector/menuPlayer
	// TODO: Instead of getting a reference for selector, create a selector
	public GameObject selectorObject;
	public GameObject cameraSystemObject;

	[SerializeField] float cameraSensitivity = 0.15f;

	Selector selector;
	CameraSystem cameraSystem;
	InputHandler inputHandler;
	float dTime;

	bool selectionTrigger;

	void Start() {
		
        inputHandler = GetComponent<InputHandler>();
		selector = selectorObject.GetComponent<Selector>();
		cameraSystem = cameraSystemObject.GetComponent<CameraSystem>();
	}

	void Update() {

		dTime = Time.deltaTime;
		inputHandler.TickInputs(dTime);

		HandleInputs();

		// if (selector == null) // make singleton
	}

	void HandleInputs() {

		SelectorSelection();
		CameraNavigation();

		if (selector.TryMoveToMouseCell(inputHandler.MousePosition)) {

			selector.HoveredUnitCircleExit();
			selector.HoveredUnitIndicatorDeselect();
			selector.Hover();
			selector.HoveredUnitCircleEnter();
			selector.HoveredUnitIndicatorHighlight();
			selector.UpdateUIHoveredCell();
		}
	}

	void SelectorSelection() {
		
		if (inputHandler.SelectionFlag && !selectionTrigger) {

			selectionTrigger = true;

			if (!EventSystem.current.IsPointerOverGameObject()) {

				selector.SelectedUnitCircleDeselect();
				selector.SelectedUnitIndicatorDeselect();
				selector.Select();
				selector.SelectedUnitCircleSelect();
				selector.SelectedUnitIndicatorSelect();
				selector.UpdateUISelectedUnit();
			}
			//else Debug.Log(EventSystem.current.currentSelectedGameObject); // only returns buttons, others are null
		}

		if (!inputHandler.SelectionFlag && selectionTrigger) {

			selectionTrigger = false;
		}
	}

	void CameraNavigation() {
		
		Vector2 cameraNavigation = inputHandler.CameraNavigation;

		if (cameraNavigation != Vector2.zero) {

			Transform cameraTargetTransform = cameraSystem.GetCameraTransform();
			Vector3 cameraTargetPosition = cameraTargetTransform.position;
			cameraTargetPosition += cameraNavigation.x * cameraSensitivity * cameraTargetTransform.right;
			cameraTargetPosition += cameraNavigation.y * cameraSensitivity * cameraTargetTransform.up;
			cameraSystem.MoveCameraNormal(cameraTargetPosition);
		}
	}
}