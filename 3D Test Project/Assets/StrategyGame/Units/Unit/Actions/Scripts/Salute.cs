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
		
		this.Log(salute + unit.cName);
		GameGrid gameGrid = unit.GetComponentInParent<GameGrid>();
		
		if (gameGrid != null)
			gameGrid.ReadCell(unit.transform.position);
	}
}
