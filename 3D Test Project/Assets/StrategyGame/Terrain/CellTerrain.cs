using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CellTerrain : MonoBehaviour {

	[SerializeField] private CellTerrainTemplate CellTerrainTemplate;
	[SerializeField] private GameObject cellHighlight;
	string tName;
	int moveCost;
	int viewCost;
	Mesh mesh;
	Material material;

	public int G;
	public int H;
	public int F { get => G + H; }
	public CellTerrain previous;

	public int MoveCost { get => moveCost; }
	public int ViewCost { get => viewCost; }
	public string TName { get => tName; }
	private List<GameObject> content;

	void Awake() {

		SetDefaultData();
		content = new List<GameObject>();
	}

	void SetDefaultData() {

		// Name
		tName = CellTerrainTemplate.tName;

		// Properties
		moveCost = CellTerrainTemplate.moveCost;
		viewCost = CellTerrainTemplate.viewCost;

		// Mesh
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		mesh = CellTerrainTemplate.mesh;
		meshFilter.mesh = mesh;

		// Material
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		material = CellTerrainTemplate.material;
		meshRenderer.material = material;
	}

	public void SetHighlight(bool state) {

		cellHighlight.SetActive(state);
	}

	public bool AddContent(GameObject gameObject) {

		if (gameObject == null || content.Count >= 1)
			return false;

		content.Add(gameObject);

		return true;
	}

	public bool RemoveContent(GameObject gameObject) {

		int cuantity = content.Count;
		content = new List<GameObject>();

		return true;
	}

	public List<GameObject> GetContent() {

		return content;
	}

	public Unit GetUnit() {

		foreach (GameObject gameObj in content) {

			Unit unit = gameObj.GetComponent<Unit>();

			if (unit != null)
				return unit;
		}

		return null;
	}
}
