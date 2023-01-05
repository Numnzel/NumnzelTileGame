using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour {

    PlayerInputs playerInputs;
	InputDevice inputDevices;
	InputDevice currentDevice;

	Vector2 mousePosScreen;
    Vector2 cameraNavigation;
    bool selectionFlag;

	public Vector2 MousePosition { get => mousePosScreen; set => mousePosScreen = value; }
	public Vector2 CameraNavigation { get => cameraNavigation; }
	public bool SelectionFlag { get => selectionFlag; }

	public void OnEnable() {
		
		if (playerInputs == null)
            playerInputs = new PlayerInputs();

		// Cada vez que pulsas WASD se hace lo que hay a la derecha de la expresión lambda (=>).
		playerInputs.Game.CameraNavigation.performed += playerInputs => cameraNavigation = playerInputs.ReadValue<Vector2>();
		playerInputs.Enable();
	}

	public void TickInputs(float dTime) {

		HandleSelection();
		HandleMouseScreen();
		CheckDevices();
	}

    void HandleSelection() {
		
        selectionFlag = playerInputs.Game.ContentSelection.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
	}

	void HandleMouseScreen() {
		
		mousePosScreen = Mouse.current.position.ReadValue();
	}

	void CheckDevices() {

		if (inputDevices == null)
			inputDevices = InputSystem.GetDevice<InputDevice>();
		
		InputSystem.onDeviceChange += (device, change) => {
			switch (change) {
				case InputDeviceChange.Added:
					this.Log("New device added: " + device);
					SetCurrentDevice();
					break;

				case InputDeviceChange.Removed:
					this.Log("Device removed: " + device);
					SetCurrentDevice();
					break;
			}
		};
	}

	void SetCurrentDevice() {
		
		var gamepad = Gamepad.current;
		var mouse = Mouse.current;

		if (gamepad != null)
			currentDevice = gamepad;
		else if (mouse != null)
			currentDevice = mouse;
		else
			currentDevice = null;
	}
}
