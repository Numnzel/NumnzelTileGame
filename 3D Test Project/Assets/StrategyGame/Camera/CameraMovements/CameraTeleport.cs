using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTeleport : CameraPan {
    public override void Pan(GameObject camera, Vector3 target) {

		camera.transform.position = target;
	}
}
