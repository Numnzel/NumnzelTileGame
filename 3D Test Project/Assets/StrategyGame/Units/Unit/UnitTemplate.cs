using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Unit")]
public class UnitTemplate : ScriptableObject {

	public Action action;
	public int health;
	public string cName;
	public Mesh mesh;
	public Material meshMaterial;
	public int movement;
}
