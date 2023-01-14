using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DigitalRuby.Tween;

public class Movement : MonoBehaviour {

	public int movement;

	public void Move(CellTerrain cell) {

		Unit unit = gameObject.GetComponent<Unit>();

		if (unit == null)
			return;

		GameGrid.MoveUnitToCell(unit, cell, TweenScaleFunctions.Linear);
	}
}
