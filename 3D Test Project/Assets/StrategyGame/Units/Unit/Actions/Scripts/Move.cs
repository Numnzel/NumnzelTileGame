using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Action/Move")]
public class Move : Action {

	public float A;
	public float B;

	public override void Execute(GameObject parent) {

		this.Log("Calculation: " + A + B);
	}
}
