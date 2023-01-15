using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using System.Linq;

public class Selector : MonoBehaviour {

	static Selector _instance;
	public static Selector Instance { get => _instance; }
	public static UnityEvent<CellTerrain> OnClick { get => Instance._onClick; set => Instance._onClick = value; }

	GameGrid gameGrid;
	CellTerrain currentCell;
	public Unit selectedUnit;
	public Unit hoveredUnit;

	public UnityEvent<CellTerrain> _onClick;

	void Awake() {

		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}

	void Start() {

		gameGrid = GetComponentInParent<GameGrid>();
		currentCell = GameGrid.ReadCell(transform.position);
		UpdateUIHoveredCell();
	}

	UnitIndicator GetUnitIndicator(Unit unit) {

		if (unit != null)
			return unit.indicator.GetComponent<UnitIndicator>();

		return null;
	}

	UnitCircle GetUnitCircle(Unit unit) {

		if (unit != null)
			return unit.circle.GetComponent<UnitCircle>();

		return null;
	}

	public void HoveredUnitIndicatorHighlight() {

		UnitIndicator unitIndicator = GetUnitIndicator(hoveredUnit);

		if (unitIndicator != null)
			unitIndicator.SetSpriteHighlight();
	}

	public void HoveredUnitIndicatorDeselect() {

		UnitIndicator unitIndicator = GetUnitIndicator(hoveredUnit);

		if (unitIndicator != null)
			unitIndicator.SetSpriteAvailable();
	}

	public void SelectedUnitIndicatorSelect() {

		UnitIndicator unitIndicator = GetUnitIndicator(selectedUnit);

		if (unitIndicator != null)
			unitIndicator.SetSpriteSelected();
	}

	public void SelectedUnitIndicatorDeselect() {

		UnitIndicator unitIndicator = GetUnitIndicator(selectedUnit);

		if (unitIndicator != null)
			unitIndicator.SetSpriteAvailable();
	}

	public void HoveredUnitCircleEnter() {

		UnitCircle unitCircle = GetUnitCircle(hoveredUnit);

		if (unitCircle != null)
			unitCircle.SetVisibility(true);
	}

	public void HoveredUnitCircleExit() {

		UnitCircle unitCircle = GetUnitCircle(hoveredUnit);

		if (unitCircle != null && !unitCircle.Selected)
			unitCircle.SetVisibility(false);
	}

	public void SelectedUnitCircleDeselect() {

		UnitCircle unitCircle = GetUnitCircle(selectedUnit);

		if (unitCircle != null) {

			unitCircle.SetVisibility(false);
			unitCircle.SetSelected(false);
		}
	}

	public void SelectedUnitCircleSelect() {

		UnitCircle unitCircle = GetUnitCircle(selectedUnit);

		if (unitCircle != null) {

			unitCircle.SetVisibility(true);
			unitCircle.SetSelected(true);
		}
	}

	public void Select() {

		selectedUnit = currentCell.GetUnit();

		if (OnClick != null)
			OnClick.Invoke(currentCell);
	}

	public void Hover() {

		hoveredUnit = currentCell.GetUnit();
	}

	public bool TryMoveToMouseCell(Vector2 mousePosScreen) {

		CellTerrain targetCell = gameGrid.ReadCellAtMouse(mousePosScreen);

		if (targetCell == null || targetCell == currentCell)
			return false;

		currentCell = targetCell;
		GameGrid.MoveToCell(gameObject, currentCell);
		return true;
	}

	public void UpdateUIHoveredCell() {

		if (currentCell == null)
			return;

		UIManager.Instance.UpdateTerrainUI(currentCell);

		//if (targetTerrain != null)
		//Debug.Log("VIEW COST: " + targetTerrain.ViewCost);
	}

	public void UpdateUISelectedUnit(int playerFactionNumber = 0) {

		Faction unitFaction = null;

		/*
		if (hoveredUnit == null && maintainSelection)
			return;
		*/

		UIManager.Instance.UpdateUnitSelectionUI(hoveredUnit);

		if (hoveredUnit != null)
			unitFaction = hoveredUnit.GetComponent<Faction>();

		if (unitFaction != null && unitFaction.factionNumber == playerFactionNumber)
			UIManager.Instance.UpdateUnitCommandUI(hoveredUnit);
		else
			UIManager.Instance.UpdateUnitCommandUI(null);
	}
}