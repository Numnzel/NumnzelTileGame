using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraPan : MonoBehaviour {

	public abstract void Pan(GameObject camera, Vector3 target);
}
