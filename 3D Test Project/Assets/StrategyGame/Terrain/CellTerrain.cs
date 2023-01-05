using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CellTerrain : MonoBehaviour {

	public CellTerrainTemplate CellTerrainTemplate;
	Unit unit;
	string tName;
	int moveCost;
	int viewCost;
	Mesh mesh;
	Material material;

	public int MoveCost { get => moveCost; }
	public int ViewCost { get => viewCost; }
	public string TName { get => tName; }
	public Unit Unit { get => unit; set => unit = value; }

	void Awake() {

		SetDefaultData();
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
}
