using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    public GameObject cam;

    public CameraPan normal;
    public CameraPan cinematic;

	void Start() {

		//MoveCameraCinematic(new Vector3(1.0f, 3.0f, 10.0f));
	}

	public void MoveCameraNormal(Vector3 target) {
        normal.Pan(cam, target);
	}

    public void MoveCameraCinematic(Vector3 target) {
        cinematic.Pan(cam, target);
	}

	public Transform GetCameraTransform() {
		return cam.transform;
	}
}
