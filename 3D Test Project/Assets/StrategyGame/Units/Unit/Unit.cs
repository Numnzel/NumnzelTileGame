using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Unit : MonoBehaviour {

	public UnitTemplate unitTemplate;
	public int currentHP = 0;
	public string cName;
	public List<Action> actions;
	public GameObject circle;
	public UnitIndicator indicator;
	public int movement;
	int maxHP;
	Mesh mesh;
	Material meshMaterial;
	bool available = true;

	void Awake() {

		SetDefaultData();
	}

	void Update() {

		indicator.SetVisibility(available);
	}

	void SetDefaultData() {

		if (unitTemplate == null)
			return;

		// Name
		cName = (cName == "") ? unitTemplate.cName : cName;

		// Health
		maxHP = unitTemplate.health;
		currentHP = (currentHP == 0) ? maxHP : currentHP;

		// Movement
		movement = unitTemplate.movement;

		// Mesh
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		mesh = unitTemplate.mesh;
		meshFilter.mesh = mesh;

		// Mesh Material
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		meshMaterial = unitTemplate.meshMaterial;
		meshRenderer.material = meshMaterial;
	}

	public void DoAction(Action action) {

		action.Execute(gameObject);
	}
}