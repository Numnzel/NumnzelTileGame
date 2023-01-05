using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Selector : MonoBehaviour {

	static Selector _instance;
	public static Selector Instance { get => _instance; }

	GameGrid gameGrid;
	CellTerrain currentCell;
	public Unit selectedUnit;
	public Unit hoveredUnit;

	void Awake() {

		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}

	void Start() {

		gameGrid = GetComponentInParent<GameGrid>();
		currentCell = gameGrid.ReadCell(transform.position, true);
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

		selectedUnit = currentCell.Unit;

		//path = gameGrid.pathFinder.FindPath(highlightedCell, highlightedCellend);

		//if (path.Count > 0)
		//	MoveAlongPath();
	}

	public void Hover() {

		hoveredUnit = currentCell.Unit;
	}

	/*
	void MoveAlongPath() {

		float step = selectedUnit.movement * Time.deltaTime;

		selectedUnit.transform.position = Vector2.MoveTowards(selectedUnit.transform.position, path[0].transform.position, step);

		if (Vector3.Distance(selectedUnit.transform.position, path[0].transform.position) < 0.0001f) {

			gameGrid.MoveToCell(gameObject, path[0].gameObject.GetComponent<CellTerrain>());
			path.RemoveAt(0);
		}
	}*/

	/*
public void Select() {

	GameObject terrainCell = gameGrid.ReadCell(transform.position, true);

	if (currentCell == terrainCell)
		return;

	Deselect();

	currentCell = terrainCell;
	selectedCube = currentCell.GetComponent<CellTerrain>();
	selectedCube.SetSelected(true);
	gameGrid.SpawnOnCell(gameGrid.selectionCellPrefab.gameObject, currentCell, currentCell);

	HashSet<GameObject> cells = new HashSet<GameObject>();
	cells.UnionWith(gameGrid.GetMovementCells(currentCell, 3));


	foreach (GameObject cell in cells) {

		if (cell == null)
			continue;

		//GameObject highlight = gameGrid.SpawnOnCell(gameGrid.cellHighlightPrefab.gameObject, cell, gameGrid.highlightContainer);
		GameObject highlight = gameGrid.SpawnOnCell(new GameObject(), cell, gameGrid.highlightContainer);

		TextMeshPro textMesh = highlight.AddComponent<TextMeshPro>();
		//MeshRenderer meshRenderer = highlight.AddComponent<MeshRenderer>();
		//Debug.Log(textMesh.ToString());
		textMesh.text = cell.GetComponent<CellTerrain>().MoveCost.ToString();
		textMesh.alignment = TextAlignmentOptions.Center;
		textMesh.fontSize = 4;
		highlight.transform.position += new Vector3(0, 0.15f, 0);
		//RectTransform rectTransform = highlight.GetComponent<RectTransform>();
		//rectTransform.rotation = new Quaternion(26,0,0,0);
		//rectTransform.localRotation = new Quaternion(26, 0, 0, 0);
		//rectTransform.transform.rotation = new Quaternion(26, 0, 0, 0);
		//highlight.GetComponent<MeshRenderer>().material = testMaterial;
	}
}*/

	public bool TryMoveToMouseCell(Vector2 mousePosScreen) {

		CellTerrain targetCell = gameGrid.ReadCellAtMouse(mousePosScreen);

		if (targetCell == null || targetCell == currentCell)
			return false;

		currentCell = targetCell;
		gameGrid.MoveToCell(gameObject, currentCell);
		return true;
	}


	public void UpdateUIHoveredCell() {

		UIManager.Instance.UpdateTerrainUI(currentCell);

		//if (targetTerrain != null)
		//Debug.Log("VIEW COST: " + targetTerrain.ViewCost);
	}

	public void UpdateUISelectedUnit() {

		UIManager.Instance.UpdateUnitCommandUI(hoveredUnit);
		UIManager.Instance.UpdateUnitSelectionUI(hoveredUnit);
	}
}