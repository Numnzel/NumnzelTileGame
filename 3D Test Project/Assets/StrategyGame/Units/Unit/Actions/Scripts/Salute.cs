using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Action/Salute")]
public class Salute : Action {

	public string salute;

	public override void Execute(GameObject parent) {

		Unit unit = parent.GetComponent<Unit>();

		if (unit == null)
			return;
		
		// Action to be done:
		this.Log(salute + unit.uName);

		/*
		GameGrid gameGrid = unit.transform.parent.GetComponent<GameGrid>();
		
		if (gameGrid != null)
			gameGrid.ReadCell(unit.transform.position);*/
	}
}
