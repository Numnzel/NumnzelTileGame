using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ArrowTranslator;

[CreateAssetMenu(menuName = "Game Data/Action/SkillMove")]
public class SkillMove : Action {

	Movement movement;
	List<CellTerrain> cells;
	List<CellTerrain> path;
	UnityAction clickAction;
	UnityAction hoverAction;

	public override ActionState Execute(GameObject parent) {

		if (parent == null)
			return ActionState.Error;

		movement = parent.GetComponent<Movement>();
		Faction faction = parent.GetComponent<Faction>();

		if (movement == null || faction == null)
			return ActionState.Error;

		Controller controller = GameManager.Instance.Controllers[faction.factionNumber];
		CellTerrain parentCell = GameGrid.ReadCell(parent.transform.position);

		if (controller == null || parentCell == null)
			return ActionState.Error;

		// get available cells to move
		cells = GameGrid.GetMovementCells(parentCell, movement.movement);

		// add listeners
		clickAction = delegate { Clicked(controller); };
		hoverAction = delegate { Hover(controller, parentCell); };

		controller.OnClick.AddListener(clickAction);
		controller.OnHover.AddListener(hoverAction);

		// add a highlight to each cell
		foreach (CellTerrain cell in cells) {
			
			if (cell == null || cell.GetUnit() != null)
				continue;

			cell.SetHighlight(true);
		}

		return ActionState.Success;
	}

	private void Clicked(Controller controller) {

		ClearPath();

		CellTerrain selectedCell = controller.CurrentCell;

		// move to selectedCell
		if (cells.Contains(selectedCell))
			movement.Move(selectedCell, 0.4f);

		// remove listeners
		controller.OnClick.RemoveListener(clickAction);
		controller.OnHover.RemoveListener(hoverAction);

		// remove the highlight from each cell
		foreach (CellTerrain cell in cells) {

			if (cell == null)
				continue;

			cell.SetHighlight(false);
		}
	}

	private void Hover(Controller controller, CellTerrain parentCell) {

		ClearPath();

		if (controller.HoverCell == null || !cells.Contains(controller.HoverCell))
			return;

		path = GameGrid.AStar(parentCell, controller.HoverCell);

		// Add cell at unit position
		path.Insert(0, parentCell);

		// Generate arrow sprite with path
		for (int i = 0; i < path.Count; i++) {

			CellTerrain previousTile = i > 0 ? path[i - 1] : null;
			CellTerrain futureTile = i < path.Count - 1 ? path[i + 1] : null;

			ArrowInfo arrowDir = TranslateDirection(previousTile, path[i], futureTile);

			path[i].SetArrowSprite(arrowDir, Color.green);
		}
	}

	private void ClearPath() {

		if (path == null)
			return;

		foreach (CellTerrain cell in path)
			if (cell != null)
				cell.SetArrowSprite(new ArrowInfo());
	}
}
