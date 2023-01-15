using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DigitalRuby.Tween;

public class Movement : MonoBehaviour {

	public int movement;
	private Coroutine tweenCoroutine;

	public void Move(CellTerrain targetCell, float duration = 0) {

		List<CellTerrain> pathCells;

		pathCells = GameGrid.AStar(GameGrid.ReadCell(transform.position), targetCell);

		//foreach (CellTerrain cell in pathCells) {
		//	cell.transform.localPosition = new Vector3 (cell.transform.localPosition.x, cell.transform.localPosition.y+0.1f, cell.transform.localPosition.z);
		//}

		tweenCoroutine = StartCoroutine(TweenMove(pathCells, duration));
	}

	public IEnumerator TweenMove(List<CellTerrain> pathCells, float duration) {

		foreach (CellTerrain cell in pathCells) {

			GameGrid.MoveLinkToCell(gameObject, cell, TweenScaleFunctions.Linear, duration);
			yield return new WaitForSeconds(duration);
		}
	}

	internal void OnDestroy() {

		if (tweenCoroutine != null)
			StopCoroutine(tweenCoroutine);
	}
}