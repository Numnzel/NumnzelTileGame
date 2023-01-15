using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
	public GameObject SphereTest;
	public GameObject gridContainer;

	public readCellsAreaFlags flags;

	static GameGrid _instance;
	public static GameGrid Instance { get => _instance; }

	void Awake() {
		
		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}

	void Start() {

		//GenerateGrid();

		// On scenario load, links all pre-placed units to their cell.
		List<Unit> units = GetAllUnits();
		foreach (Unit unit in units)
			if (!LinkToCell(unit.gameObject, ReadCell(unit.transform.position)))
				Destroy(unit);
	}

	private void Update() {

		List<CellTerrain> cells = GetAllTiles();

		int totalUnits = 0;

		foreach (CellTerrain cell in cells) {
			if (cell.GetUnit())
				totalUnits++;
		}

		Debug.Log(totalUnits);
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
	public static GameObject SpawnOnCell(GameObject gameObj, CellTerrain targetCell, GameObject parent = null) {

		if (gameObj == null || targetCell == null || !targetCell.CompareTag(TagTerrain))
			return null;

		Transform parentObj = (parent == null) ? Instance.transform : parent.transform;
		GameObject instance = Instantiate(gameObj, parentObj);
		MoveToCell(instance, targetCell);

		return instance;
	}

	///<summary> Method: Move a GameObject to a terrain cell. </summary>
	public static void MoveToCell(GameObject gameObj, CellTerrain targetCell, bool setParent = false) {

		if (gameObj == null || targetCell == null || !targetCell.CompareTag(TagTerrain))
			return;

		Vector3 targetCellPos = targetCell.transform.position;

		if (setParent)
			gameObj.transform.SetParent(targetCell.gameObject.transform);

		targetCellPos.y += offsy;
		gameObj.transform.position = targetCellPos;
	}

	///<summary> Method: Move with tween a GameObject to a terrain cell. </summary>
	public static void MoveTweenToCell(GameObject gameObj, CellTerrain targetCell, System.Func<float, float> tweenScale, float duration, bool setParent = false) {

		if (gameObj == null || targetCell == null || !targetCell.CompareTag(TagTerrain))
			return;

		Vector3 targetCellPos = targetCell.transform.position;

		if (setParent)
			gameObj.transform.SetParent(targetCell.gameObject.transform);

		targetCellPos.y += offsy;
		TweenUtils.TweenMove(gameObj, targetCellPos, tweenScale, duration);
	}

	///<summary> Method: Move a GameObject and link it to a terrain cell. </summary>
	public static void MoveLinkToCell(GameObject gameObj, CellTerrain targetCell, System.Func<float, float> tweenScale, float duration = 0) {

		if (gameObj == null || targetCell == null || !targetCell.CompareTag(TagTerrain) || targetCell.GetUnit() != null)
			return;

		// unlink unit from original cell
		CellTerrain originalCell = ReadCell(gameObj.transform.position);
		if (originalCell != null)
			Instance.UnlinkFromCell(gameObj, originalCell);

		// link unit to target cell
		if (!Instance.LinkToCell(gameObj, targetCell))
			return;

		// check movement type
		if (tweenScale == null || duration == 0)
			MoveToCell(gameObj.gameObject, targetCell, true);
		else
			MoveTweenToCell(gameObj.gameObject, targetCell, tweenScale, duration);
	}

	///<summary> Method: Links a GameObject to a terrain cell. </summary>
	public bool LinkToCell(GameObject gameObj, CellTerrain targetCell) {

		if (targetCell == null || !targetCell.CompareTag(TagTerrain))
			return false;

		return targetCell.AddContent(gameObj);
	}

	///<summary> Method: Unlinks a GameObject from a terrain cell. </summary>
	public bool UnlinkFromCell(GameObject gameObj, CellTerrain targetCell) {

		if (targetCell == null || !targetCell.CompareTag(TagTerrain))
			return false;

		return targetCell.RemoveContent(gameObj.gameObject);
	}

	///<summary> Method: Remove a GameObject that is a child of a terrain cell. </summary>
	public static bool RemoveOnCell(GameObject gameObj, CellTerrain targetCell) {

		if (gameObj == null || targetCell == null || !targetCell.CompareTag(TagTerrain) || !gameObj.transform.IsChildOf(targetCell.transform))
			return false;

		gameObj.SetActive(false);
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
	public static List<CellTerrain> GetMovementCells(CellTerrain originCell, int movement) {

		List<CellTerrain> inRangeCells = new List<CellTerrain>();
		List<CellTerrain> previousCells = new List<CellTerrain>();
		int stepCount = 0;

		inRangeCells.Add(originCell);
		previousCells.Add(originCell);

		while (stepCount < movement) {

			List<CellTerrain> surroundingCells = new List<CellTerrain>();

			foreach (CellTerrain cell in previousCells)
				surroundingCells.AddRange(ReadCellNeighbour(cell));

			inRangeCells.AddRange(surroundingCells);

			previousCells = surroundingCells.Distinct().ToList();

			++stepCount;
		}

		return inRangeCells.Distinct().ToList();
	}

	public static List<CellTerrain> AStar(CellTerrain originCell, CellTerrain targetCell) {

		List<CellTerrain> openList = new List<CellTerrain>();
		List<CellTerrain> closedList = new List<CellTerrain>();

		openList.Add(originCell);

		while (openList.Count > 0) {

			CellTerrain currentCell = openList.OrderBy(x => x.F).First();

			openList.Remove(currentCell);
			closedList.Add(currentCell);
			
			//currentCell.transform.localPosition = new Vector3(currentCell.transform.localPosition.x, currentCell.transform.localPosition.y + 0.5f, currentCell.transform.localPosition.z);

			if (currentCell == targetCell) {

				List<CellTerrain> endList = GetFinishedList(originCell, targetCell);
				/*
				int i = 0;
				foreach (CellTerrain cell in endList) {
					++i;
					Debug.DrawRay(cell.transform.position + (Vector3.up * 0.5f), Vector3.down * 0.5f, new Color(0.0f + (0.12f * i), 1.0f - (0.12f * i), 0), 6000);
				}*/

				return endList;
			}

			List<CellTerrain> neighbourCells = ReadCellNeighbour(currentCell);

			foreach (CellTerrain cell in neighbourCells) {

				if (cell.GetUnit() != null) {
					Debug.Log("There is a unit");
					continue;
				}

				if (closedList.Contains(cell)) {
					Debug.Log("Contained");
					continue;
				}

				cell.G = GetManhattanDistance(currentCell, cell);
				cell.H = GetManhattanDistance(targetCell, cell);

				cell.previous = currentCell;

				if (!openList.Contains(cell))
					openList.Add(cell);
			}
		}

		return new List<CellTerrain>();
	}

	private static List<CellTerrain> GetFinishedList(CellTerrain originCell, CellTerrain targetCell) {

		List<CellTerrain> finishedList = new List<CellTerrain>();

		CellTerrain currentCell = targetCell;

		while (currentCell != originCell) {

			finishedList.Add(currentCell);
			currentCell = currentCell.previous;
		}

		finishedList.Reverse();

		return finishedList;
	}

	private static int GetManhattanDistance(CellTerrain start, CellTerrain end) {

		Vector2 startPos = start.transform.position;
		Vector2 endPos = end.transform.position;

		return Mathf.RoundToInt(Mathf.Abs(startPos.x - endPos.x) + Mathf.Abs(startPos.y - endPos.y));
	}

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

		for (int i = 0; i < 4; i++) {
			
			CellTerrain cell = ReadCell(originCell.transform.position + directions[i]);
			if (cell != null)
				cellsArea.Add(cell);
		}
		
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
}
