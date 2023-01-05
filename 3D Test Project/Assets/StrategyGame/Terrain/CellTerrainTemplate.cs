using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Cell Terrain")]
public class CellTerrainTemplate : ScriptableObject {

	public string tName;
	public int moveCost;
	public int viewCost;
	public Mesh mesh;
	public Material material;
}
