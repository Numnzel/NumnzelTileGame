using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : CameraPan {

	[SerializeField] float speed = 5f;

	public override void Pan(GameObject camera, Vector3 target) {
		
		StartCoroutine(CalculateSmooth(camera, target));
	}

	IEnumerator CalculateSmooth(GameObject camera, Vector3 target) {
		
		while (camera.transform.position != target) {
			camera.transform.position = Vector3.MoveTowards(camera.transform.position, target, speed * Time.deltaTime);
			yield return null;
		}
	}
}
