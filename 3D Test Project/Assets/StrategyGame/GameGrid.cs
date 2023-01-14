using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

[System.Flags]
public enum readCellsAreaFlags {

	BlockedByUnits = 1,
	BlockedByWater = 2
}

public class GameGrid : MonoBehaviour {

	const string TagTerrain = "TerrainCube";
	const int LayerTerrain = 6;
	const int GridCellSize = 1;
	const float offsy = 0.001f;

	[SerializeField] int _width, _height;

	//Dictionary<Vector2, OverlayTile> tiles;
	//LineRenderer lineRenderer;
	public Material gridLineMaterial;
	public SelectionCell selectionCellPrefab;
	[SerializeField] private CellHighlight cellHighlightPrefab;
	private static CellHighlight cellHighlight;
	public GameObject SphereTest;
	public GameObject gridContainer;
	public PathFinder pathFinder;

	public readCellsAreaFlags flags;

	static GameGrid _instance;
	public static GameGrid Instance { get => _instance; }

	void Awake() {
		
		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;

		cellHighlight = cellHighlightPrefab;
	}

	void Start() {

		//GenerateGrid();
		pathFinder = new PathFinder();
		pathFinder.gameGrid = this;

		// On scenario load, links all pre-placed units to their cell.
		LinkUnitsToCells(GetAllUnits());
	}

	void GenerateGrid() {

		//tiles = new Dictionary<Vector2, OverlayTile>();
		//float offsxz = 0.5f;
		float offsy = 0.03f;
		float linew = 0.02f;

		for (int x = 0; x <= _width; x++) {

			Vector3 srt = new Vector3(x - (_width / 2), offsy, -(_height / 2));
			Vector3 end = new Vector3(x - (_width / 2), offsy, (_height / 2));

			GameObject lines = new GameObject("Lines");
			lines.transform.parent = gridContainer.transform;
			LineRenderer lineRenderer = lines.AddComponent<LineRenderer>();
			lineRenderer.startColor = Color.blue;
			lineRenderer.endColor = Color.blue;
			lineRenderer.startWidth = linew;
			lineRenderer.endWidth = linew;
			lineRenderer.SetPosition(0, srt);
			lineRenderer.SetPosition(1, end);
			lineRenderer.material = gridLineMaterial;
		}
		for (int z = 0; z <= _height; z++) {

			Vector3 srt = new Vector3(-(_width / 2), offsy, z - (_height / 2));
			Vector3 end = new Vector3((_width / 2), offsy, z - (_height / 2));

			GameObject lines = new GameObject("Lines");
			lines.transform.parent = gridContainer.transform;
			LineRenderer lineRenderer = lines.AddComponent<LineRenderer>();
			lineRenderer.startColor = Color.green;
			lineRenderer.endColor = Color.green;
			lineRenderer.startWidth = linew;
			lineRenderer.endWidth = linew;
			lineRenderer.SetPosition(0, srt);
			lineRenderer.SetPosition(1, end);
			lineRenderer.material = gridLineMaterial;
		}
		/*
		for (int x = 0; x < _width; x++) {
			for (int z = 0; z < _height; z++) {

				Vector3 pos = new Vector3(x + offsxz - (_width / 2), offsy-0.001f, z + offsxz - (_height / 2));
				var spawnedTile = Instantiate(_tilePrefab, pos, Quaternion.identity);
				spawnedTile.name = $"Tile X{x} Z{z}";
			}
		}*/

		//_cam.transform.position = new Vector3
	}

	///<summary> Method: Casts a ray that only interacts with terrain. </summary>
	static bool RayCastTerrain(Ray ray, out RaycastHit hit, float maxDistance = Mathf.Infinity) {

		bool temp = Physics.Raycast(
			ray: ray,
			hitInfo: out hit,
			maxDistance: maxDistance,
			layerMask: 1 << LayerTerrain);

		temp = temp && hit.collider.CompareTag(TagTerrain);
		return temp;
	}

	///<summary> Method: Spawn a GameObject on a terrain cell. Returns the spawned instance. </summary>
	public static GameObject SpawnOnCell(GameObject gObject, CellTerrain targetCell, GameObject parent = null) {

		if (gObject == null || targetCell == null || !targetCell.CompareTag(TagTerrain))
			return null;

		Transform parentObj = (parent == null) ? Instance.transform : parent.transform;
		GameObject instance = Instantiate(gObject, parentObj);
		MoveToCell(instance, targetCell);

		return instance;
	}

	///<summary> Method: Move a GameObject to a terrain cell. </summary>
	public static void MoveToCell(GameObject gObject, CellTerrain targetCell, bool setParent = false) {

		if (gObject == null || targetCell == null || !targetCell.CompareTag(TagTerrain))
			return;

		Vector3 targetCellPos = targetCell.transform.position;

		if (setParent)
			gObject.transform.SetParent(targetCell.gameObject.transform);

		targetCellPos.y += offsy;
		gObject.transform.position = targetCellPos;
	}

	///<summary> Method: Move with tween a GameObject to a terrain cell. </summary>
	public static void MoveTweenToCell(GameObject gObject, CellTerrain targetCell, System.Func<float, float> tweenScale, bool setParent = false) {

		if (gObject == null || targetCell == null || !targetCell.CompareTag(TagTerrain))
			return;

		Vector3 targetCellPos = targetCell.transform.position;

		if (setParent)
			gObject.transform.SetParent(targetCell.gameObject.transform);

		targetCellPos.y += offsy;
		TweenUtils.TweenMove(gObject, targetCellPos, tweenScale);
	}

	///<summary> Method: Move a unit to a terrain cell. </summary>
	public static void MoveUnitToCell(Unit unit, CellTerrain targetCell, System.Func<float, float> tweenScale) {

		if (unit == null || targetCell == null || !targetCell.CompareTag(TagTerrain) || targetCell.Unit != null)
			return;

		// unlink unit from original cell
		CellTerrain originalCell = ReadCell(unit.transform.position);
		if (originalCell)
			Instance.UnlinkUnitFromCell(unit, originalCell);

		// link unit to target cell
		Instance.LinkUnitToCell(unit, targetCell);

		// check movement type
		if (tweenScale == null)
			MoveToCell(unit.gameObject, targetCell, true);
		else
			MoveTweenToCell(unit.gameObject, targetCell, tweenScale);
	}

	///<summary> Method: Links a unit to a terrain cell. </summary>
	public void LinkUnitToCell(Unit unit, CellTerrain targetCell) {

		if (targetCell == null || !targetCell.CompareTag(TagTerrain))
			return;

		targetCell.Unit = unit;
	}

	///<summary> Method: Unlinks a unit from a terrain cell. </summary>
	public void UnlinkUnitFromCell(Unit unit, CellTerrain targetCell) {

		if (targetCell == null || !targetCell.CompareTag(TagTerrain))
			return;

		targetCell.Unit = null;
	}

	///<summary> Method: Remove a GameObject that is a child of a terrain cell. </summary>
	public static bool RemoveOnCell(GameObject gObject, CellTerrain targetCell) {

		if (gObject == null || targetCell == null || !targetCell.CompareTag(TagTerrain) || !gObject.transform.IsChildOf(targetCell.transform))
			return false;

		Destroy(gObject);
		return true;
	}

	///<summary> Method: Gets the terrain cell at the specified coordinates. </summary>
	public static CellTerrain ReadCell(Vector3 origin, bool dev = false) {

		CellTerrain terrainObject = null;
		Ray ray = new Ray(origin + Vector3.up, Vector3.down * 2);
		RaycastHit hit;

		if (dev)
			Debug.DrawRay(origin + Vector3.up, Vector3.down * 2, Color.green, 60);

		if (RayCastTerrain(ray, out hit, 8f))
			terrainObject = hit.collider.gameObject.GetComponent<CellTerrain>();

		return terrainObject;
	}

	///<summary> Method: Gets the terrain cell at mouse screen position. </summary>
	public CellTerrain ReadCellAtMouse(Vector2 mousePosScreen) {

		return ReadCell(MouseHitOnScene(mousePosScreen).point);
	}

	///<summary> Method: Gets the scene hit info at mouse screen position. </summary>
	public RaycastHit MouseHitOnScene(Vector2 mousePosScreen) {

		Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);
		RaycastHit hit;

		Physics.Raycast(ray, out hit, Mathf.Infinity);

		return hit;
	}

	///<summary> Method: Gets the scene terrain hit info at mouse screen position. </summary>
	public bool MouseHitOnTerrain(Vector2 mousePosScreen, out RaycastHit hit) {

		Ray ray = Camera.main.ScreenPointToRay(mousePosScreen);

		return RayCastTerrain(ray, out hit);
	}

	///<summary> Method: Gets the cells in an area considering movement costs. </summary>
	public List<GameObject> GetMovementCells(GameObject originCell, int movement, readCellsAreaFlags flags = 0) {

		return new List<GameObject>();
	}

	/*
	///<summary> Method: Gets the cells in an area considering movement costs. </summary>
	public HashSet<GameObject> GetMovementCells(GameObject originCell, int movement, readCellsAreaFlags flags = 0) {

		if (originCell == null || movement < 0)
			return null;

		// if movement == 0 return originCell ???


		Dictionary<GameObject, int> cellsMPoints = new Dictionary<GameObject, int>();
		HashSet<GameObject> cellsRead = new HashSet<GameObject>();
		HashSet<GameObject> cellsBuffer = new HashSet<GameObject>();

		cellsBuffer.Add(originCell);
		cellsMPoints.Add(originCell, movement);

		do {

			foreach (GameObject bCell in cellsBuffer) {

				List<GameObject> cellsNeightbour = ReadCellNeighbour(bCell);

				foreach (GameObject nCell in cellsNeightbour) {

					if (nCell == null)
						continue;

					int moveCost = nCell.GetComponent<CellTerrain>().MoveCost;
					int movePoints = 0;

					if (cellsMPoints.TryGetValue(bCell, out movePoints))
						if (movePoints >= moveCost) {
							movePoints -= moveCost;
							cellsMPoints.TryAdd(nCell, movePoints);
							cellsRead.Add(nCell);
						}
				}
			}
		} while (cellsMPoints.ContainsValue(1));

		//cellsMPoints.

		//cellsSet.UnionWith(cellsBuffer);
		return cellsRead;
	}*/

	///<summary> Method: Check if the movement points are enough to move to a cell. Gets the difference in an out parameter.
	///Returns false with a difference of -1 if there's an error.
	///</summary>
	/*
	bool CheckMovement(GameObject objectiveCell, int movement, out int difference) {

		if (objectiveCell == null || movement <= 0) {
			
			difference = -1;
			return false;
		}

		CellTerrain oCell = objectiveCell.GetComponent<CellTerrain>();

		if (oCell == null) {

			difference = -1;
			return false;
		}

		int diff = oCell.MoveCost - movement;
		difference = diff;

		return (diff > 0);
	}
	*/

	///<summary> Method: Gets the cells around a cell in an area defined by distance. </summary>
	/*
	public GameObject[] ReadCellsArea(GameObject originCell, int distance, readCellsAreaFlags flags = 0) {

		if (originCell == null || distance < 0)
			return null;

		HashSet<GameObject> cellsArea = new HashSet<GameObject>();
		HashSet<GameObject> cellsRead = new HashSet<GameObject>();
		HashSet<GameObject> cellsLast = new HashSet<GameObject>();

		cellsArea.Add(originCell);
		cellsLast.Add(originCell);

		//Debug.DrawLine(aCell.transform.position + Vector3.up, cell.transform.position + Vector3.up, new Color(255 * Math.Abs(directions[j].x), 255 * Math.Abs(directions[j].y), 255 * Math.Abs(directions[j].z)), 99);

		for (int i = distance; i > 0; i--) {

			foreach (GameObject lCell in cellsLast) {

				GameObject[] neighbours = ReadCellNeighbour(lCell);

				foreach (GameObject nCell in neighbours) {
					Debug.DrawLine(nCell.transform.position, nCell.transform.position + (Vector3.up * 10), new Color((255 / distance) * i, 0, 255 - (255 / distance) * i), 99);
					if (nCell != null && cellsRead != null && cellsArea != null && !cellsArea.Contains(nCell)) {
						cellsRead.Add(nCell);
					}
				}
			}

			if (cellsRead != null) {
				cellsArea.UnionWith(cellsRead);
				cellsLast = cellsRead;
			}

			cellsRead = null;
		}

		GameObject[] objArray = new GameObject[cellsArea.Count];
		cellsArea.CopyTo(objArray);
		return objArray;
	}
	*/

	///<summary> Method: Gets the cells adjacent to a cell. </summary>
	public static List<CellTerrain> ReadCellNeighbour(CellTerrain originCell) {

		if (originCell == null)
			return null;

		List<CellTerrain> cellsArea = new List<CellTerrain>();
		Vector3[] directions = new Vector3[4];
		directions[0] = new Vector3(GridCellSize, 0, 0);
		directions[1] = new Vector3(-GridCellSize, 0, 0);
		directions[2] = new Vector3(0, 0, GridCellSize);
		directions[3] = new Vector3(0, 0, -GridCellSize);

		for (int i = 0; i < 4; i++)
			cellsArea.Insert(i, ReadCell(originCell.transform.position + directions[i], true));

		return cellsArea;
	}

	///<summary> Method: Gets all cells. </summary>
	List<CellTerrain> GetAllTiles() {

		List<CellTerrain> list = new List<CellTerrain>();

		foreach (CellTerrain cell in gameObject.GetComponentsInChildren<CellTerrain>())
			list.Add(cell);

		return list;
	}

	///<summary> Method: Gets all units. </summary>
	List<Unit> GetAllUnits() {

		List<Unit> list = new List<Unit>();

		foreach (Unit unit in gameObject.GetComponentsInChildren<Unit>())
			list.Add(unit);

		return list;
	}

	///<summary> Method: Spawns a highlight over passed cells. </summary>
	public static void HighlightCells(List<CellTerrain> cells) {

		if (cells == null)
			return;

		foreach (CellTerrain cell in cells)
			HighlightCell(cell);
	}

	///<summary> Method: Spawns a highlight over passed cell. </summary>
	public static void HighlightCell(CellTerrain cell) {

		if (cell == null || !cell.CompareTag(TagTerrain) || cell.GetComponentInChildren<CellHighlight>() != null)
			return;

		SpawnOnCell(cellHighlight.gameObject, cell, cell.gameObject);
	}

	///<summary> Method: Links units to their current cell. </summary>
	void LinkUnitsToCells(List<Unit> units) {

		if (units == null)
			return;

		foreach (Unit unit in units)
			LinkUnitToCell(unit, ReadCell(unit.transform.position));
	}
}
