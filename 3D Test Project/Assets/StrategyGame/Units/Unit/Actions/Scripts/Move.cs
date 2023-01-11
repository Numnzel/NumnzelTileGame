using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

[CreateAssetMenu(menuName = "Game Data/Action/Move")]
public class Move : Action {

	public Unit unit;

	public override void Execute(GameObject parent) {

		if (parent == null)
			return;

		//Movement movement = parent.GetComponent<Movement>();

		unit = parent.GetComponent<Unit>();

		// Action to be done:
		this.Log("The unit " + unit.uName + " is trying to move.");

		TweenUtils.TweenMoveUnit(unit, unit.transform.position + Vector3.forward * 3f, TweenScaleFunctions.Linear);

		GameGrid gameGrid = parent.GetComponentInParent<GameGrid>();
	}
}
