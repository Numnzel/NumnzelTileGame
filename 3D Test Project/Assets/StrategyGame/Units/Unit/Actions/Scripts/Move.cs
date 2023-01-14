using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game Data/Action/Move")]
public class Move : Action {

	Movement movement;
	List<CellTerrain> cells;

	public override void Execute(GameObject parent) {

		if (parent == null)
			return;

		movement = parent.GetComponent<Movement>();

		if (movement == null)
			return;

		// get available cells to move
		cells = Search(parent, movement.movement);

		// add a listener to each cell
		foreach (CellTerrain cell in cells) {
			
			if (cell == null || cell.Unit != null)
				continue;

			GameGrid.HighlightCell(cell);
			Selector.OnClick.AddListener(Clicked);
		}
	}

	private List<CellTerrain> Search(GameObject parent, int movement) {

		return GameGrid.ReadCellNeighbour(GameGrid.ReadCell(parent.transform.position));
	}

	private void Clicked(CellTerrain cellTerrain) {

		if (cells.Contains(cellTerrain))
			movement.Move(cellTerrain);

		foreach (CellTerrain cell in cells) {

			if (cell == null)
				continue;

			CellHighlight cellHighlight = cell.GetComponentInChildren<CellHighlight>();
			if (cellHighlight != null)
				GameGrid.RemoveOnCell(cellHighlight.gameObject, cell);

			Selector.OnClick.RemoveAllListeners();
		}
	}
}
