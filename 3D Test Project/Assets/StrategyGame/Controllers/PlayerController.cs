using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : Controller {

	// TODO: Make an interface for controlling selector/menuPlayer
	// TODO: Instead of getting a reference for selector, create a selector
	public GameObject selectorObject;
	public GameObject cameraSystemObject;

	[SerializeField] float cameraSensitivity = 0.15f;

	Selector selector;
	CameraSystem cameraSystem;
	float dTime;
	Vector2 cameraNavigation;
	Vector2 mousePosScreen;
	bool isPointerOverGameObject;

	public Vector2 MousePosition { get => mousePosScreen; set => mousePosScreen = value; }

	void Start() {
		
		selector = selectorObject.GetComponent<Selector>();
		cameraSystem = cameraSystemObject.GetComponent<CameraSystem>();
	}

	void Update() {

		dTime = Time.deltaTime;
		TickInputs(dTime);

		HandleInputs();
		CameraNavigation();

		// if (selector == null) // make singleton
	}

	public void TickInputs(float dTime) {

		HandleMouseScreen();
	}

	/// <summary>
	/// Store mouse position and check if it is over a game object
	/// </summary>
	void HandleMouseScreen() {

		mousePosScreen = Mouse.current.position.ReadValue();
		isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
	}

	/// <summary>
	/// Update the selector position to the cell at mouse position, updates the UI if so
	/// </summary>
	void HandleInputs() {

		if (selector.TryMoveToMouseCell(MousePosition)) {

			selector.HoveredUnitCircleExit();
			selector.HoveredUnitIndicatorDeselect();
			selector.Hover();
			selector.HoveredUnitCircleEnter();
			selector.HoveredUnitIndicatorHighlight();
			selector.UpdateUIHoveredCell();
		}
	}

	/// <summary>
	/// The selector tries to select an unit at his position and updates the UI acordingly
	/// </summary>
	void SelectorSelection() {

		// Check that the mouse cursor is not hovering an UI element
		if (!isPointerOverGameObject) {

			selector.SelectedUnitCircleDeselect();
			selector.SelectedUnitIndicatorDeselect();
			selector.Select();
			selector.SelectedUnitCircleSelect();
			selector.SelectedUnitIndicatorSelect();
			selector.UpdateUISelectedUnit();
		}
		//else Debug.Log(EventSystem.current.currentSelectedGameObject); // only returns buttons, others are null
	}

	void CameraNavigation() {
		
		if (cameraNavigation != Vector2.zero) {

			Transform cameraTargetTransform = cameraSystem.GetCameraTransform();
			Vector3 cameraTargetPosition = cameraTargetTransform.position;
			cameraTargetPosition += cameraNavigation.x * cameraSensitivity * cameraTargetTransform.right;
			cameraTargetPosition += cameraNavigation.y * cameraSensitivity * cameraTargetTransform.up;
			cameraSystem.MoveCameraNormal(cameraTargetPosition);
		}
	}

	public void OnHandleSelection(InputAction.CallbackContext context) {

		if (context.performed)
			SelectorSelection();
	}

	public void OnCameraNavigation(InputAction.CallbackContext context) {

		if (context.performed)
			cameraNavigation = context.ReadValue<Vector2>();
	}
}